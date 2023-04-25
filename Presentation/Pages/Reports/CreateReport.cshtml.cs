using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.RegularExpressions;

namespace Presentation.Pages.Reports
{
    public class CreateReportModel : PageModel
    {
        public List<Report> Reports = new();
        public List<Teacher> Teachers = new();
        private SchoolContext context = new();
        public static GroupStudents Group = new();
        public static string ErrorMessage = "";
        public static Report PutReport = new();

        public CreateReportModel()
        {
            Teachers = context.Teachers.ToList();
            Reports = context.Reports
                .Where(x => x.GroupStudentId.Equals(Group.Id)).ToList();
        }

        public static void SetViewPutReport(GroupStudents group, Report report, HttpResponse response)
        {
            Group = group;
            PutReport = report;
            response.Redirect("/Reports/CreateReport");
        }

        public static void AddReport(GroupStudents group, HttpResponse response)
        {
            Group = group;
            response.Redirect("/Reports/CreateReport");
        }

        public void OnPostSetPutReport(string ReportId)
        {
            PutReport = context.Reports
                .FirstOrDefault(x => x.Id.ToString().Equals(ReportId));
        }
        public void OnPostPutReport(string NameReport,string TeacherId, 
            string[] CurrentScore, string[] AllScore, string[] FullName,
            string PutReportId)
        {
            if (NameReport.IsNullOrEmpty() || TeacherId.Equals("Выберите учителя")
                || CurrentScore.Any(x => x.IsNullOrEmpty()) ||
                AllScore.Any(x => x.IsNullOrEmpty()))
            {
                ErrorMessage = "При попытке создания отчета произошла ошибка" +
                    ", попробуйте снова заполнить все поля";
                return;
            }
            ErrorMessage = "";

            var report = context.Reports
                .FirstOrDefault(x => x.Id.ToString().Equals(PutReportId));

            var teacherOld = context.Teachers.
                FirstOrDefault(x => x.Id.Equals(report.Teacher.Id));

            var teacherNew = context.Teachers
                .FirstOrDefault(x => x.Id.ToString().Equals(TeacherId));

            StringBuilder description = new();

            int index = 0;
            foreach (var student in FullName)
            {
                string[] line = student.Split(' ');
                description.AppendLine($"{line[0]} " +
                    $"{line[1]} {line[2]} {CurrentScore[index]} " +
                    $"{AllScore[index]}");
                index++;
            }

            report.Name = NameReport;
            report.Description = description.ToString();
            report.Teacher = teacherNew;
            report.TeacherId = teacherNew.Id;
            teacherNew.Reports.Add(report);
            teacherOld.Reports.Remove(PutReport);

            context.SaveChanges();
            Response.Redirect(Request.Path);
        }
        public void OnPostRemoveReport(string ReportId)
        {
            var report = context.Reports
                .FirstOrDefault(x => x.Id.ToString().Equals(ReportId));

            var teacher = context.Teachers
                .FirstOrDefault(x => x.Id.Equals(report.TeacherId));

            var group = context.GroupsStudents
                .FirstOrDefault(x => x.Id.Equals(Group.Id));
            
            if(teacher != null) teacher.Reports.Remove(report);
            if(group != null) group.Reports.Remove(report);

            context.Reports.Remove(report);

            context.SaveChanges();
            Response.Redirect(Request.Path);
        }

        public void OnPostGetGroup(string GroupId) 
        {
            if (GroupId.IsNullOrEmpty())
            {
                ErrorMessage = "Выберите группу";
                return;
            }
            ErrorMessage = "";

            Group = context.GroupsStudents
                .FirstOrDefault(x => x.Id.ToString().Equals(GroupId));
            
            Reports = context.Reports
                .Where(x => x.GroupStudents.Id.Equals(Group.Id)).ToList();

            Response.Redirect(Request.Path);
        }

        public void OnPostCreateReport(string NameReport, string[] StudentId,
            string TeacherId, string[] CurrentScore, string[] AllScore)
        {
            if (NameReport.IsNullOrEmpty() || TeacherId.Equals("Выберите учителя")
                || CurrentScore.Any(x => x.IsNullOrEmpty()) ||
                AllScore.Any(x => x.IsNullOrEmpty()))
            {
                ErrorMessage = "При попытке создания отчета произошла ошибка" +
                    ", попробуйте снова заполнить все поля";
                return;
            }
            ErrorMessage = "";

            var students = context.Students
                .Where(x => x.GroupStudentsId.Equals(Group.Id)).ToList();
            var teacher = context.Teachers
                .FirstOrDefault(x => x.Id.ToString().Equals(TeacherId));

            StringBuilder desciption = new();

            int index = 0;
            foreach(var id in StudentId)
            {
                var student = students
                    .FirstOrDefault(x => x.Id.ToString().Equals(id));

                desciption.AppendLine($"{student.FirstName} {student.LastName} " +
                    $"{student.MiddleName} {CurrentScore[index]} {AllScore[index]}");
                index++;
            }

            var report = new Report(NameReport, desciption.ToString(), DateTime.Now);
            var group = context.GroupsStudents
                .FirstOrDefault(x => x.Id.Equals(Group.Id));

            report.Teacher = teacher;
            report.GroupStudents = group;
            report.GroupStudentId = group.Id;
            teacher.Reports.Add(report);
            group.Reports.Add(report);
            context.Reports.Add(report);

            var k = context.Reports.ToList();

            context.SaveChanges();
            Response.Redirect(Request.Path);
        }
    }
}

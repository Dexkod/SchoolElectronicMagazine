using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Pages.Reports
{
    public class EditGroupStudentsModel : PageModel
    {
        public static GroupStudents Group = new();
        public  List<Student> Students = new();
        public static Student PutStudent = new();
        public string ErrorMessage = string.Empty;
        public static string ValidationStudent = "";
        public SchoolContext context = new();

        public EditGroupStudentsModel()
        {
            Students = context.Students
                .Where(x => x.GroupStudent.Equals(Group)).ToList();
            
        }

        public static void GetView(GroupStudents group, HttpResponse response)
        {
            Group = group;
            response.Redirect("/Students/EditGroupStudents");
        }
        public void OnPostPutLastReport(string PutReportId)
        {
            var report = context.Reports
                .FirstOrDefault(x => x.Id.ToString().Equals(PutReportId));

            CreateReportModel.SetViewPutReport(Group, report, Response);
        }

        public void OnPostAddReport()
        {
            CreateReportModel.AddReport(Group, Response);
        }

        public void OnPostRemoveReport(string RemoveReportId)
        {
            var report = context.Reports
                .FirstOrDefault(x => x.Id.ToString().Equals(RemoveReportId));

            var teacher = context.Teachers
                .FirstOrDefault(x => x.Id.Equals(report.TeacherId));

            var group = context.GroupsStudents
                .FirstOrDefault(x => x.Id.Equals(Group.Id));

            if (teacher != null) teacher.Reports.Remove(report);
            if (group != null) group.Reports.Remove(report);

            context.Reports.Remove(report);

            context.SaveChanges();
            Response.Redirect(Request.Path);
        }
        public void OnPostSearchStudents(string ValidationString)
        {
            
            ValidationStudent = ValidationString.IsNullOrEmpty() ?
                "" : string.Join("", ValidationString.Split(' '));
        }
        public void OnPostBindingStudent(string[] StudentId)
        {
            foreach(var id in StudentId)
            {
                var student = context.Students
                    .FirstOrDefault(x => x.Id.ToString().Equals(id));

                if (student.GroupStudent != null) 
                { 
                    var oldGroup = context.GroupsStudents
                    .FirstOrDefault(x => student.GroupStudent.Id.Equals(x.Id));

                    oldGroup.Students.Remove(student);
                    Group.Students.Add(student);
                }

                student.GroupStudent = Group;
                student.GroupStudentsId = Group.Id;
                
            }

            context.SaveChanges();
            Response.Redirect(Request.Path);
        }

        public void OnPostCreateNewStudent(string StudentFirstName,
            string StudentLastName, string StudentMiddleName)
        {
            if (StudentFirstName.IsNullOrEmpty() || StudentLastName.IsNullOrEmpty()
                || StudentMiddleName.IsNullOrEmpty())
            {
                ErrorMessage = "При добавление студента произошла ошибка, " +
                    "попробуйте снова ввести все поля";
                return;
            }
            ErrorMessage = string.Empty;

            Student student = new Student(StudentFirstName
                , StudentLastName, StudentMiddleName);

            context.Students.Add(student);

            student.GroupStudent = Group;
            student.GroupStudentsId = Group.Id;
            Group.Students.Add(student);

            context.SaveChanges();
            
            Response.Redirect(Request.Path);
        }

        public void OnPostGetPutStudent(string StudentId)
        {
            PutStudent = Students
                .FirstOrDefault(x => x.Id.ToString().Equals(StudentId));
        }

        public void OnPostPutStudent(string StudentFirstName,
            string StudentLastName, string StudentMiddleName)
        {
            if(StudentFirstName.IsNullOrEmpty() || StudentLastName.IsNullOrEmpty() 
                || StudentMiddleName.IsNullOrEmpty())
            {
                ErrorMessage = "При изменения произошла ошибка, " +
                    "попробуйте снова ввести все поля";
                return;
            }
            ErrorMessage = string.Empty;

            var student = Students.FirstOrDefault(x => x.Id.Equals(PutStudent.Id));

            student.FirstName = StudentFirstName;
            student.LastName = StudentLastName;
            student.MiddleName = StudentMiddleName;

            context.SaveChanges();
        }

        public void OnPostRemoveStudent(string StudentId)
        {
            Student student = Students
                .FirstOrDefault(x => x.Id.ToString().Equals(StudentId));
            Group.Students.Remove(student);

            context.Students.FirstOrDefault(x => x.Equals(student))
                .Lessons.Select(x => x.StudentsOnLesson = null);
            context.Students.Remove(student);
            context.SaveChanges();

            Response.Redirect(Request.Path);
        }
        

    }
}

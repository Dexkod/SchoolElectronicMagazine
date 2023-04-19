using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Pages.CreateTeacher
{
    public class TeachersModel : PageModel
    {
        public string ErrorMessage = string.Empty;
        public List<Teacher> Teachers = new();
        public static Teacher PutTeacher = new();
        private SchoolContext context = new();

        public TeachersModel()
        {
            Teachers = context.Teachers.ToList();
        }
        public void OnPostCreateTeacher(string TeacherFirstName,
            string TeacherLastName, string TeacherMiddleName, string Description)
        {
            if (TeacherFirstName.IsNullOrEmpty() || TeacherLastName.IsNullOrEmpty()
                || TeacherMiddleName.IsNullOrEmpty() || Description.IsNullOrEmpty())
            {
                ErrorMessage = "При создание произошла ошибка, введите все поля";
                return;
            }
            ErrorMessage = "";

            context.Teachers.Add(new Teacher(TeacherFirstName, TeacherLastName,
                TeacherMiddleName, Description));
            context.SaveChanges();

            Response.Redirect(Request.Path);
        }

        public void OnPostGetTeacher(string TeacherId)
        {
            PutTeacher = context.Teachers
                .FirstOrDefault(x => x.Id.ToString().Equals(TeacherId));
        }

        public void OnPostPutTeacher(string TeacherFirstName,
            string TeacherLastName, string TeacherMiddleName, string Description)
        {
            if (TeacherFirstName.IsNullOrEmpty() || TeacherLastName.IsNullOrEmpty()
                || TeacherMiddleName.IsNullOrEmpty() || Description.IsNullOrEmpty())
            {
                ErrorMessage = "При создание произошла ошибка, введите все поля";
                return;
            }
            ErrorMessage = "";

            var teacher = Teachers
                .FirstOrDefault(x => x.Id.Equals(PutTeacher.Id));


            teacher.FirstName = TeacherFirstName;
            teacher.LastName = TeacherLastName;
            teacher.MiddleName = TeacherMiddleName;
            teacher.Desciption = Description;

            context.SaveChanges();
            PutTeacher = new();
        }

        public void OnPostRemoveTeacher(string TeacherId)
        {
            var teacher = Teachers
                .FirstOrDefault(x => x.Id.ToString().Equals(TeacherId));
            context.Teachers.Remove(teacher);

            List<Lesson> lessons = context.Lessons.ToList();
            lessons.Where(x => x.Teacher.Equals(teacher)).Select(x => x.Teacher == null);
            context.Reports.Where(x => x.Teacher.Equals(teacher)).Select(x => x.Teacher == null);

            context.SaveChanges();
            Response.Redirect(Request.Path);
        }
    }
}

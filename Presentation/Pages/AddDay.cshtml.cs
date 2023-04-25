using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace Presentation
{
    public class AddDayModel : PageModel
    { 
        public static List<Lesson> Lessons = new List<Lesson>();
        public string ErrorMessageAddDay = "";
        public string ErrorMessageLesson = "";
        public AddDayModel()
        {
            
        }
        public void OnGet()
        {
        }

        public void OnPostAddDay(string DateDay)
        {
            if(DateDay == null)
            {
                ErrorMessageAddDay = "Введите дату";
                return;
            }
            ErrorMessageAddDay = "";
            
            SchoolContext context = new();

            int[] dayMouthYear = DateDay.Split('-').Select(x => Convert.ToInt32(x)).ToArray();
            var date = new DateTime(dayMouthYear[0], dayMouthYear[1], dayMouthYear[2]); 
            var day = new Day(date.DayOfWeek.ToString(), date);

            Lessons = Lessons.OrderBy(x => x.HoursAndMinutes.Hour).ToList();

            foreach(var lesson in Lessons)
            {
                day.Lessons.Add(lesson);
                lesson.Day = day;
                lesson.DayId = day.Id;
                context.Add(lesson);
            }

            context.Days.Add(day);
            context.SaveChangesAsync();

            Lessons = new();
            Response.Redirect("/Index");
        }

        public void OnPostAddLesson(string TimeNewLesson, string NameNewLesson, string PlaceNewLesson)
        {
            if(TimeNewLesson == null || NameNewLesson == null || PlaceNewLesson == null)
            {
                ErrorMessageLesson = "При создании пары произошли ошибка, заполните все поля";
                return;
            }
            ErrorMessageLesson = "";

            DateTime time = new();
            time = time.AddHours(Convert.ToInt32(TimeNewLesson.Split(':', ' ')[0]));
            time = time.AddMinutes(Convert.ToInt32(TimeNewLesson.Split(':', ' ')[1]));

            Lesson lesson = new(NameNewLesson, PlaceNewLesson, time);
            Lessons.Add(lesson);
        }

        public void OnPostPutLessons(string[] NameLessons, string[] Times,
            string[] Places, string[] IsRemove)
        {
            if (NameLessons.Any(word => word.IsNullOrEmpty()) ||
                Places.Any(word => word.IsNullOrEmpty()) ||
                Times.Any(word => word.IsNullOrEmpty()))
            {
                ErrorMessageLesson = "При изменении пар произошла ошибка,введите все поля";
                return;
            }
            ErrorMessageLesson = "";

            char[] separators = { ' ', ':', '-' };

            for(int i = 0; i < NameLessons.Length; i++)
            {
                try
                {
                    string[] HoursAndMinute = Times[i].Split(separators)
                        .Select(number => string.Join("",number.Where(x => char.IsNumber(x))))
                        .Where(number => !number.IsNullOrEmpty()).ToArray();
                    DateTime time = new();
                    time = time.AddHours(Convert.ToInt32(HoursAndMinute[0]));
                    time = time.AddMinutes(Convert.ToInt32(HoursAndMinute[1]));

                    Lessons[i].Name = NameLessons[i];
                    Lessons[i].HoursAndMinutes = time;
                    Lessons[i].Place = Places[i]; 
                }
                catch
                {
                    ErrorMessageLesson = "Не все изменения прошли успешно, попытайтесь снова";
                }
            }

            for(int i = 0; i < IsRemove.Length; i++)
            {
                try
                {
                    Lessons.Remove(Lessons.FirstOrDefault(x => x.Id.ToString().Equals(IsRemove[i])));
                }
                catch
                {
                    ErrorMessageLesson = "Не все изменения прошли успешно, попытайтесь снова";
                }
            }

        }
    }
}

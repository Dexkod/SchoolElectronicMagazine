using Domain.Entities;
using Infrastructure.DbContext;
using Infrastructure.FirstData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace SchoolElectronicMagazine.Pages
{
    public class IndexModel : PageModel
    {
        public static Day PutDay = new Day();
        public string ErrorMessagePostLesson = string.Empty;
        public string ErrorMessageLesson = "";
        private readonly ILogger<IndexModel> _logger;
        public List<Day> Days;
        private SchoolContext schoolContext = new();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            SearchWeek();
        }

        public void OnGet()
        {
            
        }

        public void OnPostAddLesson(string DayId, string TimeNewLesson,
            string PlaceNewLesson, string NameNewLesson)
        {
            if(DayId == null || TimeNewLesson == null || PlaceNewLesson== null ||
               NameNewLesson == null)
            {
                ErrorMessagePostLesson = "При создании произошла ошибка, введите все поля";
                return;
            }
            ErrorMessagePostLesson = "";

            Day day = Days.FirstOrDefault(x => x.Id.ToString().Equals(DayId));

            DateTime time = new();
            time = time.AddHours(Convert.ToInt32(TimeNewLesson.Split(':',' ')[0]));
            time = time.AddMinutes(Convert.ToInt32(TimeNewLesson.Split(':', ' ')[1]));

            Lesson lesson = new(NameNewLesson,PlaceNewLesson, time);
            day.Lessons.Add(lesson);
            lesson.DayId = day.Id;
            lesson.Day = day;

            day.Lessons = day.Lessons.OrderBy(x => x.HoursAndMinutes.Hour).ToList();
            schoolContext.Add(lesson);
            schoolContext.SaveChanges();

            Response.Redirect(Request.Path);
        }

        public void OnPostPutLessons(string[] NameLessons, string[] Times,
            string[] Places, string[] IsRemove)
        {
            Day day = schoolContext.Days.FirstOrDefault(x => x.Id.Equals(PutDay.Id));

            if (NameLessons.Any(word => word.IsNullOrEmpty()) ||
                Places.Any(word => word.IsNullOrEmpty()) ||
                Times.Any(word => word.IsNullOrEmpty()))
            {
                ErrorMessageLesson = "При изменении пар произошла ошибка,введите все поля";
                return;
            }
            ErrorMessageLesson = "";

            char[] separators = { ' ', ':', '-' };
            int index = 0;

            foreach (var lesson in day.Lessons)
            {
                try
                {
                    string[] HoursAndMinute = Times[index].Split(separators)
                        .Select(number => string.Join("", number.Where(x => char.IsNumber(x))))
                        .Where(number => !number.IsNullOrEmpty()).ToArray();
                    DateTime time = new();
                    time = time.AddHours(Convert.ToInt32(HoursAndMinute[0]));
                    time = time.AddMinutes(Convert.ToInt32(HoursAndMinute[1]));

                    lesson.Name = NameLessons[index];
                    lesson.HoursAndMinutes = time;
                    lesson.Place = Places[index];
                }
                catch
                {
                    ErrorMessageLesson = "Не все изменения прошли успешно, попытайтесь снова";
                }
                
                index++;
            }

            for (int i = 0; i < IsRemove.Length; i++)
            {
                try
                {
                    Lesson lesson = day.Lessons
                        .FirstOrDefault(x => x.Id.ToString().Equals(IsRemove[i]));

                    day.Lessons.Remove(lesson);
                    schoolContext.Lessons.Remove(lesson);
                }
                catch(Exception error)
                {
                    ErrorMessageLesson = "Не все изменения прошли успешно, попытайтесь снова";
                }
            }
            PutDay = new Day();

            schoolContext.SaveChanges();
        }

        public void OnPostGetDay(string DayId)
        {
            PutDay = Days.FirstOrDefault(x => x.Id.ToString().Equals(DayId));
        }

        private void SearchWeek()
        {
            // Last day in schedule
            DateTime dayLast = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dayLast = dayLast.AddDays(4);
            List<Day> allDays = schoolContext.Days.ToList();
            List<Lesson> lessons = schoolContext.Lessons.ToList();

            allDays = allDays.Where(x => (dayLast - x.Time).Days >= 0 && 
                (dayLast - x.Time).Days < 7).ToList();
            allDays = allDays.OrderBy(x => x.Time).ToList();

            Days = allDays;
        }
    }
}
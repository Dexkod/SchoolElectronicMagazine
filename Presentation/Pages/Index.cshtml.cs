using Domain.Entities;
using Infrastructure.DbContext;
using Infrastructure.FirstData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolElectronicMagazine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Day> Days = new List<Day>();
        private SchoolContext schoolContext = new SchoolContext();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
        }

        public void OnGet()
        {
            //AddData.Add();
            SearchWeek();
        }

        public void OnPost(string DayId, string TimeNewLesson,string PlaceNewLesson, string NameNewLesson)
        {
            //new L
            Console.WriteLine();
        }

        private void SearchWeek()
        {
            // Last day in schedule
            DateTime dayLast = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dayLast = dayLast.AddDays(4);
            List<Day> allDays = schoolContext.Days.ToList();
            List<Lesson> lessons = schoolContext.Lessons.ToList();

            allDays = allDays.Where(x => (dayLast - x.Time).Days >= 0 && 
                (dayLast - x.Time).Days < 7).ToList();
            
            for(int i = 0; i < allDays.Count - 1; i++)
            {
				for (int j = i + 1; j < allDays.Count - 1; j++)
				{
                    if(allDays[i].Time > allDays[j].Time)
                    {
                        var swap = allDays[i];
                        allDays[i] = allDays[j];
                        allDays[j] = swap;
                    }
				}
			}

            Days = allDays;
        }
    }
}
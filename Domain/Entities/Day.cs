using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Day : IEntity
    {
        public Guid Id { get; set; }
        public string DayOfTheWeek { get; set; }
        public DateTime Time { get; set; }

        public Day()
        {
            Lessons = new List<Lesson>();
        }
        public ICollection<Lesson> Lessons { get; set; }
    }
}

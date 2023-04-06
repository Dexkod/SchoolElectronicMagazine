using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lesson : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime HoursAndMinutes { get; set; }

        public Lesson() 
        {
            GroupStudents = new List<GroupStudents>();
            StudentsOnLesson = new List<Student>();
        }

        public Lesson(string name, string place, DateTime hoursAndMinutes) : this()
        {
            Id = Guid.NewGuid(); 
            Name = name;
            Place = place;
            HoursAndMinutes = hoursAndMinutes;
        }
        public Guid? DayId { get; set; }
        public Day Day { get; set; }

        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<GroupStudents> GroupStudents { get; set; }
        public ICollection<Student> StudentsOnLesson { get; set; }
    }
}

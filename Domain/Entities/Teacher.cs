using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Desciption { get; set; }

        public Teacher()
        {
            Lessons = new List<Lesson>();
            GroupStudents = new List<GroupStudents>();
            Reports = new List<Report>();
        }

        public Teacher(string firstName, string lastName, string middleName, string Description) 
            : this()
        {
            Id = Guid.NewGuid(); 
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Desciption = Description;
        }

        public ICollection <Report> Reports { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<GroupStudents> GroupStudents { get; set; }
    }
}

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
        }

        public Guid? ReportId { get; set; }
        public Report Report { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<GroupStudents> GroupStudents { get; set; }
    }
}

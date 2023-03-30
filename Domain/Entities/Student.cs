using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Student() { }

        public Guid? GroupStudentsId { get; set; }
        public GroupStudents GroupStudent { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}

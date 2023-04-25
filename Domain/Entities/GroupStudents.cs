using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GroupStudents : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public GroupStudents()
        {
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Reports = new List<Report>();
        }
        
        public GroupStudents(string name) : this()
        {
            Id = Guid.NewGuid();
            Name = name;
        }


        public ICollection<Teacher> Teachers { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}

﻿using Domain.Interface;
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
        }
        public Guid? ReportId { get; set; }
        public Report Report { get; set; }

        ICollection<Teacher> Teachers { get; set; }

        ICollection<Student> Students { get; set; }
    }
}
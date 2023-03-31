﻿using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Report : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public Report()
        {
        }
 
        public Teacher Teacher { get; set;}

        public Guid? GroupStudentId { get; set; }
        public GroupStudents GroupStudents { get; set; }
    }
}

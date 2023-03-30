using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeachersAndGroupStudents
    {
        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid? GroupStudentsId { get; set; }
        public GroupStudents GroupStudents { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LessonsAndStudents
    {
        public Guid? LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public Guid? StudentId { get; set; }
        public Student Student { get; set; }
    }
}

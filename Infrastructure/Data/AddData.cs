using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FirstData
{
    public static class AddData
    {
        public static void Add()
        {
            SchoolContext context = new();

            //DateTime time = new DateTime(2023, 03, 29);
            //List<Day> days = new List<Day>();
            //for (int i = 0; i < 10; i++)
            //{
            //    days.Add(new Day(time.DayOfWeek.ToString(), time));
            //    time = time.AddDays(1);
            //}

            //GroupStudents Group1 = new GroupStudents("ТРП-1-21");
            //GroupStudents Group2 = new GroupStudents("ПИ-1-21");

            //Student student1 = new Student("Михаил", "Иванов", "Валерьвеич");
            //student1.GroupStudentsId = Group1.Id;
            //student1.GroupStudent = Group1;
            //Student student2 = new Student("Матвей", "Санников", "Игоревич");
            //student2.GroupStudentsId = Group1.Id;
            //student2.GroupStudent = Group1;
            //Student student3 = new Student("Чингиз", "Ахметов", "Олегович");
            //student3.GroupStudentsId = Group2.Id;
            //student3.GroupStudent = Group2;
            //Student student4 = new Student("Влада", "Синицина", "Александровна");
            //student4.GroupStudentsId = Group2.Id;
            //student4.GroupStudent = Group2;

            //Group1.Students.Add(student1);
            //Group1.Students.Add(student2);
            //Group2.Students.Add(student3);
            //Group2.Students.Add(student4);

            //Teacher teacher = new Teacher("Сергей", "Рыжов", "Михаилович", "стаж преподования 10 лет");
            //Lesson lesson = new Lesson("Математика", "Г-302", new DateTime(
            //    days[5].Time.Year, days[5].Time.Month, days[5].Time.Day, 17, 30, 0));

            //lesson.Teacher = teacher;
            //lesson.TeacherId = teacher.Id;
            //lesson.DayId = days[5].Id;
            //lesson.Day = days[5];
            //lesson.GroupStudents.Add(Group1);
            //lesson.GroupStudents.Add(Group2);
            //lesson.StudentsOnLesson.Add(student1);
            //lesson.StudentsOnLesson.Add(student4);
            //teacher.Lessons.Add(lesson);

            //context.Teachers.Add(teacher);
            //context.Lessons.Add(lesson);
            //context.GroupsStudents.Add(Group1);
            //context.GroupsStudents.Add(Group2);
            //context.Students.Add(student1);
            //context.Students.Add(student2);
            //context.Students.Add(student3);
            //context.Students.Add(student4);

            //foreach (var day in days)
            //{
            //    context.Days.Add(day);
            //}
            Random rnd = new Random();
            //List<Lesson> lessons = context.Lessons.ToList();
            List<Day> days = context.Days.ToList();



            Console.WriteLine();

        }
    }
}

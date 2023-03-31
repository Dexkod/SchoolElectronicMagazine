using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext
{
    public class SchoolContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<GroupStudents> GroupsStudents { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonsAndStudents> LessonsAndStudents {get; set;}
        public DbSet<Report> Reports { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeachersAndGroupStudents> TeachersAndGroupStudents { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonsAndStudents>()
                .HasNoKey();
            modelBuilder.Entity<TeachersAndGroupStudents>()
                .HasNoKey();
            modelBuilder.Entity<GroupStudents>()
                .HasMany(x => x.Reports)
                .WithOne(x => x.GroupStudents);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-SO15RKHT\MYSERVER;Database=schooldb;Trusted_Connection=true;TrustServerCertificate=True;");
        }
    }
}

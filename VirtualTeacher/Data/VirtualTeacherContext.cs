using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using VirtualTeacher.Models;

namespace VirtualTeacher.Data
{
    public class VirtualTeacherContext : DbContext
    {
        public VirtualTeacherContext(DbContextOptions<VirtualTeacherContext> options)
        : base(options)
        {
        }

        //User system
        public DbSet<Student> Students { get; set; }
        public DbSet<BaseUser> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }
        public DbSet<CourseTopic> CourseTopics { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        //Bridge tables
        public DbSet<SubmittedAssignment> SubmittedAssignments { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<BaseUser>().HasKey(b => b.Id);

            //modelBuilder.Entity<Student>().HasBaseType<BaseUser>()
            //    .ToTable("Students");

            //modelBuilder.Entity<Student>().HasMany(s => s.EnrolledCourses)
        }
    }
}

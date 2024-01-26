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

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<SubmittedAssignment> AssignmentSubmissions { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}

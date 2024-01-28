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
        public DbSet<Admin> Admins { get; set; }

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
            //Configure Assignment entity
            modelBuilder.Entity<Assignment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Lecture)
                .WithOne(l => l.Assignment)
                .HasForeignKey<Lecture>(l => l.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignment>()
                .HasMany(a => a.Submissions)
                .WithOne(sa => sa.Assignment)
                .HasForeignKey(sa => sa.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure BaseUser entity
            modelBuilder.Entity<BaseUser>()
                .ToTable("Users")
                .HasKey(bu => bu.Id);

            modelBuilder.Entity<Student>()
                .HasBaseType<BaseUser>()
                .ToTable("Students");

            modelBuilder.Entity<Teacher>()
                .HasBaseType<BaseUser>()
                .ToTable("Teachers");

            modelBuilder.Entity<Admin>()
                .HasBaseType<BaseUser>()
                .ToTable("Admins");

            // Configure Course entity
            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lectures)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Ratings)
                .WithOne(cr => cr.Course)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure CourseRating entity
            modelBuilder.Entity<CourseRating>()
                .HasKey(cr => cr.Id);

            modelBuilder.Entity<CourseRating>()
                .HasOne(cr => cr.Course)
                .WithMany(c => c.Ratings)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseRating>()
                .HasOne(cr => cr.Student)
                .WithMany(s => s.CourseRatings)
                .HasForeignKey(cr => cr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure CourseTopic entity
            modelBuilder.Entity<CourseTopic>()
                .HasKey(ct => ct.Id);

            // Configure Lecture entity
            modelBuilder.Entity<Lecture>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Assignment)
                .WithOne(a => a.Lecture)
                .HasForeignKey<Assignment>(a => a.LectureId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configure StudentCourse entity
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.EnrolledCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure SubmittedAssignment entity
            modelBuilder.Entity<SubmittedAssignment>()
                .HasKey(sa => new { sa.AssignmentId, sa.StudentId });

            modelBuilder.Entity<SubmittedAssignment>()
                .HasOne(sa => sa.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(sa => sa.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubmittedAssignment>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.Assignments)
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure TeacherCourse entity
            modelBuilder.Entity<TeacherCourse>()
                .HasKey(tc => new { tc.TeacherId, tc.CourseId });

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.CoursesTeaching)
                .HasForeignKey(tc => tc.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure TeacherAssignment entity
            modelBuilder.Entity<TeacherAssignment>()
                .HasKey(ta => new { ta.TeacherId, ta.AssignmentId, ta.StudentId });

            modelBuilder.Entity<TeacherAssignment>()
                .HasOne(ta => ta.Teacher)
                .WithMany(t => t.AssignmentsToGrade)
                .HasForeignKey(ta => ta.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeacherAssignment>()
                .HasOne(ta => ta.SubmittedAssignment)
                .WithMany(sa => sa.Graders)
                .HasForeignKey(ta => new { ta.AssignmentId, ta.StudentId })  // Use the correct foreign key properties here
                .HasPrincipalKey(sa => new { sa.AssignmentId, sa.StudentId })  // Specify the composite key of SubmittedAssignment
                .OnDelete(DeleteBehavior.Cascade);


            // Your other entity configurations...

            base.OnModelCreating(modelBuilder);
        }

    }
}

using Microsoft.EntityFrameworkCore;
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
        public DbSet<CourseRating> CoursesRatings { get; set; }
        public DbSet<CourseTopic> CoursesTopics { get; set; }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentContent> AssignmentContents { get; set; }
        public DbSet<TeacherAssignment> TeachersAssigments { get; set; }

        //Bridge tables
        public DbSet<SubmittedAssignment> SubmittedAssignments { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<ApprovedTeacher> ApprovedTeachers { get; set; }
        public DbSet<Application> Applications { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<BaseUser>()
                .ToTable("Users")
                .HasKey(bu => bu.Id);

            modelBuilder.Entity<Student>()
                .HasBaseType<BaseUser>()
                .ToTable("Students");

            modelBuilder.Entity<Teacher>()
                .HasBaseType<BaseUser>()
                .ToTable("Teachers");

            modelBuilder.Entity<Teacher>()
               .HasMany(teacher => teacher.CoursesCreated)
               .WithOne(course => course.Creator)
               .HasForeignKey(course => course.CreatorId);

            modelBuilder.Entity<Admin>()
                .HasBaseType<BaseUser>()
                .ToTable("Admins");

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

            modelBuilder.Entity<CourseTopic>()
                .HasKey(ct => ct.Id);

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

            modelBuilder.Entity<SubmittedAssignment>()
                .HasKey(sa => sa.Id); // Specify the independent primary key

            modelBuilder.Entity<SubmittedAssignment>()
                .HasAlternateKey(sa => new { sa.AssignmentId, sa.StudentId }); // Specify the composite key

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

            //modelBuilder.Entity<SubmittedAssignment>()
            //    .HasKey(sa => new { sa.AssignmentId, sa.StudentId });

            //modelBuilder.Entity<SubmittedAssignment>()
            //    .HasOne(sa => sa.Assignment)
            //    .WithMany(a => a.Submissions)
            //    .HasForeignKey(sa => sa.AssignmentId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<SubmittedAssignment>()
            //    .HasOne(sa => sa.Student)
            //    .WithMany(s => s.Assignments)
            //    .HasForeignKey(sa => sa.StudentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeacherAssignment>()
                .HasKey(ta => new { ta.TeacherId, ta.AssignmentId });

            modelBuilder.Entity<TeacherAssignment>()
                .HasOne(ta => ta.Teacher)
                .WithMany(t => t.AssignmentsToGrade)
                .HasPrincipalKey(ta => ta.Id)
                .HasForeignKey(ta => ta.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeacherAssignment>()
                .HasOne(ta => ta.SubmittedAssignment)
                .WithMany(sa => sa.Graders)
                .HasPrincipalKey(ta => ta.Id)
                .HasForeignKey(ta => ta.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);
        }

    }
}

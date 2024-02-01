using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VirtualTeacher.Models_
{
    public partial class VirtualTeacherContext_ : DbContext
    {
        public VirtualTeacherContext_()
        {
        }

        public VirtualTeacherContext_(DbContextOptions<VirtualTeacherContext_> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseRating> CourseRatings { get; set; } = null!;
        public virtual DbSet<CourseTopic> CourseTopics { get; set; } = null!;
        public virtual DbSet<Lecture> Lectures { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<SubmittedAssignment> SubmittedAssignments { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<TeacherAssignment> TeacherAssignments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1435;Initial Catalog=VirtualTeacher;User Id=sqlserver;Password=D?3F&>#(}HAmCOi%;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasIndex(e => e.LectureId, "IX_Assignments_LectureId")
                    .IsUnique();

                entity.HasOne(d => d.Lecture)
                    .WithOne(p => p.Assignment)
                    .HasForeignKey<Assignment>(d => d.LectureId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.CreatorId, "IX_Courses_CreatorId");

                entity.HasIndex(e => e.CourseTopicId, "IX_Courses_TopicId");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.CourseTopic)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CourseTopicId)
                    .HasConstraintName("FK_Courses_CourseTopics_TopicId");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CreatorId);
            });

            modelBuilder.Entity<CourseRating>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_CourseRatings_CourseId");

                entity.HasIndex(e => e.StudentId, "IX_CourseRatings_StudentId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseRatings)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseRatings)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_Lectures_CourseId");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CourseId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentsCourse",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("StudentId", "CourseId");

                            j.ToTable("StudentsCourses");

                            j.HasIndex(new[] { "CourseId" }, "IX_StudentsCourses_CourseId");
                        });
            });

            modelBuilder.Entity<SubmittedAssignment>(entity =>
            {
                entity.HasKey(e => new { e.AssignmentId, e.StudentId });

                entity.HasIndex(e => e.AssignmentId, "AK_SubmittedAssignments_AssignmentId")
                    .IsUnique();

                entity.HasIndex(e => e.StudentId, "IX_SubmittedAssignments_StudentId");

                entity.Property(e => e.Grade).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Assignment)
                    .WithOne(p => p.SubmittedAssignment)
                    .HasForeignKey<SubmittedAssignment>(d => d.AssignmentId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SubmittedAssignments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.CoursesNavigation)
                    .WithMany(p => p.Teachers)
                    .UsingEntity<Dictionary<string, object>>(
                        "TeacherCourse",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("TeacherId", "CourseId");

                            j.ToTable("TeacherCourse");

                            j.HasIndex(new[] { "CourseId" }, "IX_TeacherCourse_CourseId");
                        });
            });

            modelBuilder.Entity<TeacherAssignment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TeacherAssignment");

                entity.HasOne(d => d.Assignment)
                    .WithMany()
                    .HasPrincipalKey(p => p.AssignmentId)
                    .HasForeignKey(d => d.AssignmentId);

                entity.HasOne(d => d.Teacher)
                    .WithMany()
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

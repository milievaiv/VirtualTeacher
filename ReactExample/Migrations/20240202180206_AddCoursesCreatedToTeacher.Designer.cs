﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactExample.Data;

#nullable disable

namespace ReactExample.Migrations
{
    [DbContext(typeof(VirtualTeacherContext))]
    [Migration("20240202180206_AddCoursesCreatedToTeacher")]
    partial class AddCoursesCreatedToTeacher
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ReactExample.Models.ApprovedTeacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApprovedTeachers");
                });

            modelBuilder.Entity("ReactExample.Models.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LectureId")
                        .IsUnique();

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("ReactExample.Models.BaseUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ReactExample.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseTopicId")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CourseTopicId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ReactExample.Models.CourseRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("CoursesRatings");
                });

            modelBuilder.Entity("ReactExample.Models.CourseTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CoursesTopics");
                });

            modelBuilder.Entity("ReactExample.Models.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VideoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("ReactExample.Models.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentsCourses");
                });

            modelBuilder.Entity("ReactExample.Models.SubmittedAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Grade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("SubmittedFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("AssignmentId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("SubmittedAssignments");
                });

            modelBuilder.Entity("ReactExample.Models.TeacherAssignment", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "AssignmentId");

                    b.HasIndex("AssignmentId");

                    b.ToTable("TeachersAssigments");
                });

            modelBuilder.Entity("ReactExample.Models.TeacherCourse", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("TeacherCourse");
                });

            modelBuilder.Entity("ReactExample.Models.Admin", b =>
                {
                    b.HasBaseType("ReactExample.Models.BaseUser");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("ReactExample.Models.Student", b =>
                {
                    b.HasBaseType("ReactExample.Models.BaseUser");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("ReactExample.Models.Teacher", b =>
                {
                    b.HasBaseType("ReactExample.Models.BaseUser");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("ReactExample.Models.Assignment", b =>
                {
                    b.HasOne("ReactExample.Models.Lecture", "Lecture")
                        .WithOne("Assignment")
                        .HasForeignKey("ReactExample.Models.Assignment", "LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("ReactExample.Models.Course", b =>
                {
                    b.HasOne("ReactExample.Models.CourseTopic", "CourseTopic")
                        .WithMany()
                        .HasForeignKey("CourseTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactExample.Models.Teacher", "Creator")
                        .WithMany("CoursesCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseTopic");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("ReactExample.Models.CourseRating", b =>
                {
                    b.HasOne("ReactExample.Models.Course", "Course")
                        .WithMany("Ratings")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ReactExample.Models.Student", "Student")
                        .WithMany("CourseRatings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ReactExample.Models.Lecture", b =>
                {
                    b.HasOne("ReactExample.Models.Course", "Course")
                        .WithMany("Lectures")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ReactExample.Models.StudentCourse", b =>
                {
                    b.HasOne("ReactExample.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ReactExample.Models.Student", "Student")
                        .WithMany("EnrolledCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ReactExample.Models.SubmittedAssignment", b =>
                {
                    b.HasOne("ReactExample.Models.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactExample.Models.Student", "Student")
                        .WithMany("Assignments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ReactExample.Models.TeacherAssignment", b =>
                {
                    b.HasOne("ReactExample.Models.SubmittedAssignment", "SubmittedAssignment")
                        .WithMany("Graders")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactExample.Models.Teacher", "Teacher")
                        .WithMany("AssignmentsToGrade")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SubmittedAssignment");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ReactExample.Models.TeacherCourse", b =>
                {
                    b.HasOne("ReactExample.Models.Course", "Course")
                        .WithMany("Teachers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ReactExample.Models.Teacher", "Teacher")
                        .WithMany("CoursesTeaching")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ReactExample.Models.Admin", b =>
                {
                    b.HasOne("ReactExample.Models.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("ReactExample.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReactExample.Models.Student", b =>
                {
                    b.HasOne("ReactExample.Models.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("ReactExample.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReactExample.Models.Teacher", b =>
                {
                    b.HasOne("ReactExample.Models.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("ReactExample.Models.Teacher", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReactExample.Models.Assignment", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("ReactExample.Models.Course", b =>
                {
                    b.Navigation("Lectures");

                    b.Navigation("Ratings");

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("ReactExample.Models.Lecture", b =>
                {
                    b.Navigation("Assignment")
                        .IsRequired();
                });

            modelBuilder.Entity("ReactExample.Models.SubmittedAssignment", b =>
                {
                    b.Navigation("Graders");
                });

            modelBuilder.Entity("ReactExample.Models.Student", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("CourseRatings");

                    b.Navigation("EnrolledCourses");
                });

            modelBuilder.Entity("ReactExample.Models.Teacher", b =>
                {
                    b.Navigation("AssignmentsToGrade");

                    b.Navigation("CoursesCreated");

                    b.Navigation("CoursesTeaching");
                });
#pragma warning restore 612, 618
        }
    }
}

using EFBaigiamasisStudentai.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBaigiamasisStudentai.Database
{
    public class StudentaiContext : DbContext
    {
        public StudentaiContext() : base()
        {
        }
        public StudentaiContext(DbContextOptions<StudentaiContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=EFBaigiamasisStudentai;Trusted_Connection=True;").EnableSensitiveDataLogging();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentNumber);
                entity.Property(s => s.StudentNumber).ValueGeneratedNever();
                entity.Property(s => s.FirstName).IsRequired(true);
                entity.Property(s => s.LastName).IsRequired(true);
                entity.Property(s => s.Email).IsRequired(true);
                entity.Property(s => s.DepartmentCode).IsRequired(true);
                entity.HasOne(s => s.Department)
                    .WithMany(d => d.Students)
                    .HasForeignKey(s => s.DepartmentCode);
                entity.HasMany(s => s.Lectures)
                    .WithMany(l => l.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentLecture",
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                        j => j.HasOne<Student>().WithMany().HasForeignKey("StudentNumber"),
                        j =>
                        {
                            j.HasKey("StudentNumber", "LectureId");
                        });

                entity.HasData(
                    new Student (12345678,"John","Smith","john.smith@example.com","CS1234" ),
                    new Student (87654321,"Alice","Johnson","alice.johnson@example.com","MTH567" )
                );
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasKey(l => l.LectureId);
                entity.Property(l => l.LectureId).ValueGeneratedOnAdd();
                entity.HasIndex(l => l.LectureName).IsUnique();
                entity.Property(l => l.LectureName).IsRequired(true);
                entity.Property(l => l.LectureTimeFrom).IsRequired(true);
                entity.Property(l => l.LectureTimeTo).IsRequired(true);
                entity.Property(l => l.LectureDay).IsRequired(false);
                entity.HasMany(l => l.Students)
                    .WithMany(s => s.Lectures)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentLecture",
                        j => j.HasOne<Student>().WithMany().HasForeignKey("StudentNumber"),
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"))
                    .HasData(
                        new { StudentNumber = 12345678, LectureId = 1 },
                        new { StudentNumber = 12345678, LectureId = 3 },
                        new { StudentNumber = 87654321, LectureId = 2 });

                entity.HasData(
                    new Lecture (1,"Algorithms",new TimeOnly(10,00),new TimeOnly(11,30),null),
                    new Lecture (2,"Calculus",new TimeOnly(12,00),new TimeOnly(13, 30),null),
                    new Lecture (3,"DataStructures",new TimeOnly(14,00),new TimeOnly(15, 30),null)
                );

                entity.HasMany(l => l.Departments)
                    .WithMany(d => d.Lectures)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentLecture",
                        j => j.HasOne<Department>().WithMany().HasForeignKey("DepartmentCode"),
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"))
                    .HasData(
                        new { DepartmentCode = "CS1234", LectureId = 1 },
                        new { DepartmentCode = "CS1234", LectureId = 3 },
                        new { DepartmentCode = "MTH567", LectureId = 2 });
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.DepartmentCode);
                entity.Property(d => d.DepartmentCode).ValueGeneratedNever();
                entity.Property(d => d.DepartmentName).IsRequired(true);
                entity.HasMany(d => d.Lectures)
                    .WithMany(l => l.Departments)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentLecture",
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                        j => j.HasOne<Department>().WithMany().HasForeignKey("DepartmentCode"));

                entity.HasData(
                    new Department("CS1234","ComputerScience"),
                    new Department("MTH567","Mathematics")
                );
            });
        }
    }
}
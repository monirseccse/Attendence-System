using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public class DbEntities:DbContext
    {
        private string _connectionString;
        private string _assemblyname;
        public DbSet<User>Users { get; set; }
        public DbSet<Teacher>Teachers { get; set; }
        public DbSet<Student>Students { get; set; }
        public DbSet<Course>Courses { get; set; }
        public DbSet<CourseSchedule>CourseSchedules { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbEntities()
        {
            _connectionString = "Server=SERVER\\SQLEXPRESS;Database=Assignment;User Id=Csharpb10;Password=123456;";
            _assemblyname = Assembly.GetExecutingAssembly().FullName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_assemblyname));
            }
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<CourseStudent>().ToTable("CourseStudents");
            builder.Entity<CourseStudent>().HasKey(x=>new { x.CourseId, x.StudentId });
            base.OnModelCreating(builder);
        }
    }
}

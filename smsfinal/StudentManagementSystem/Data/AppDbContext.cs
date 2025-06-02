using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null;
        public DbSet<Course> Courses { get; set; } = null;
        public DbSet<Enrollment> Enrollments { get; set; } = null;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}

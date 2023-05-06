using Microsoft.EntityFrameworkCore;
using WebApp.API.Models;

namespace WebApp.API.CoursesDbContext
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Course> Courses { get; set; }
    }
}

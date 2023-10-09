using Microsoft.EntityFrameworkCore;
using MyNewApi.Models;

namespace MyNewApi.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext>options):base(options)
        {

        }
        public DbSet<Student> students { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Data
{
    public class ApplicationDbContexte : DbContext
    {
        public ApplicationDbContexte(DbContextOptions<ApplicationDbContexte> options): base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}

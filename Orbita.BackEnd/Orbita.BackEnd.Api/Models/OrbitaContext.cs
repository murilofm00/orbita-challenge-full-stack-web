using Microsoft.EntityFrameworkCore;

namespace Orbita.BackEnd.Api.Models
{
    public class OrbitaContext : DbContext
    {
        public OrbitaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}

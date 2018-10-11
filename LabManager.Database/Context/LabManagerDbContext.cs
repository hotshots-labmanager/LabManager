using LabManager.Database.Model;
using System.Data.Entity;

namespace LabManager.Database.Context
{
    public class LabManagerDbContext : DbContext
    {
        public LabManagerDbContext() : base("LabManagerDbContext")
        {

        }

        public DbSet<Course> Course { get; set; }
    }
}

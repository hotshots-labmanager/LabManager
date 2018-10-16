using LabManager.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Database.Context
{
    public class LabManagerDbContext : DbContext
    {
        public LabManagerDbContext()// : base("LabManagerDbContext")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=LabManager;User Id=sa;Password=INFdev1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().Property(x => x.Credits).HasColumnType("decimal");
            modelBuilder.Entity<HaveTutored>().Property(x => x.Hours).HasColumnType("decimal");

            modelBuilder.Entity<Course>().HasKey("Code");
            modelBuilder.Entity<Tutor>().HasKey("Ssn");
            modelBuilder.Entity<TutoringSession>().HasKey("Code", "StartTime", "EndTime");
            modelBuilder.Entity<PlanToTutor>().HasKey("Ssn", "Code", "StartTime", "EndTime");
            modelBuilder.Entity<HaveTutored>().HasKey("Ssn", "Code", "StartTime", "EndTime");

            //modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());

            //modelBuilder.Entity<TutoringSession>()
            //    .HasMany(ts => ts.PlanToTutor)
            //    .WithMany(t => t.PlanToTutor)
            //    .Map(mc =>
            //    {
            //        mc.MapLeftKey("code", "startTime", "endTime");
            //        mc.MapRightKey("ssn");
            //        mc.ToTable("PlanToTutor");
            //    });
        }

        public DbSet<Course> Course { get; set; }

        public DbSet<Tutor> Tutor { get; set; }

        public DbSet<TutoringSession> TutoringSession { get; set; }

        public DbSet<HaveTutored> HaveTutored { get; set; }

        public DbSet<PlanToTutor> PlanToTutor { get; set; }
    }
}

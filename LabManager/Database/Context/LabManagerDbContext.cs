﻿using LabManager.Database.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LabManager.Database.Context
{
    public class LabManagerDbContext : DbContext
    {
        public LabManagerDbContext() : base("LabManagerDbContext")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>().Property(x => x.Credits).HasColumnType("decimal");
            modelBuilder.Entity<HaveTutored>().Property(x => x.Hours).HasColumnType("decimal");

            //modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());

            modelBuilder.Entity<TutoringSession>()
                .HasMany(ts => ts.PlanToTutor)
                .WithMany(t => t.PlanToTutor)
                .Map(mc =>
                {
                    mc.MapLeftKey("code", "startTime", "endTime");
                    mc.MapRightKey("ssn");
                    mc.ToTable("PlanToTutor");
                });
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Tutor> Tutor { get; set; }
        public DbSet<TutoringSession> TutoringSession { get; set; }
        public DbSet <HaveTutored> HaveTutored { get; set; }
        }
}

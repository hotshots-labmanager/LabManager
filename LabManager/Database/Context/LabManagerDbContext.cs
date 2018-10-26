using LabManager.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LabManager.Database.Context
{
    public class LabManagerDbContext : DbContext
    {
        public LabManagerDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>().Property(x => x.Credits).HasColumnType("decimal");
            //modelBuilder.Entity<HaveTutored>().Property(x => x.Hours).HasColumnType("decimal");

            // Map stored procedures to entity Course
            modelBuilder.Entity<Course>().MapToStoredProcedures(c => c
                .Insert(sp => sp.HasName("Course_Add")
                    .Parameter(cm => cm.Code, "code")
                    .Parameter(cm => cm.Name, "name")
                    .Parameter(cm => cm.Credits, "credits")
                    .Parameter(cm => cm.NumberOfStudents, "numberOfStudents"))
                .Update(sp => sp.HasName("Course_Update")
                    .Parameter(cm => cm.Code, "code"))
                .Delete(sp => sp.HasName("Course_Delete")
                    .Parameter(cm => cm.Code, "code"))
            );

            // Map stored procedures to entity PlanToTutor
            modelBuilder.Entity<TutorTutoringSession>().MapToStoredProcedures(ppt => ppt
                .Insert(sp => sp.HasName("PlanToTutor_Add")
                    .Parameter(pptm => pptm.Ssn, "ssn")
                    .Parameter(pptm => pptm.Code, "code")
                    .Parameter(pptm => pptm.StartTime, "startTime")
                    .Parameter(pptm => pptm.EndTime, "endTime"))
                .Update(sp => sp.HasName("PlanToTutor_Update")
                    .Parameter(pptm => pptm.Ssn, "ssn")
                    .Parameter(pptm => pptm.Code, "code")
                    .Parameter(pptm => pptm.StartTime, "startTime")
                    .Parameter(pptm => pptm.EndTime, "endTime"))
                .Delete(sp => sp.HasName("PlanToTutor_Delete")
                    .Parameter(pptm => pptm.Ssn, "ssn")
                    .Parameter(pptm => pptm.Code, "code")
                    .Parameter(pptm => pptm.StartTime, "startTime")
                    .Parameter(pptm => pptm.EndTime, "endTime"))
            );

            // Map stored procedures to entity HaveTutored
            //modelBuilder.Entity<HaveTutored>().MapToStoredProcedures(ht => ht
            //    .Insert(sp => sp.HasName("HaveTutored_Add")
            //        .Parameter(htm => htm.Ssn, "ssn")
            //        .Parameter(htm => htm.Code, "code")
            //        .Parameter(htm => htm.StartTime, "startTime")
            //        .Parameter(htm => htm.EndTime, "endTime")
            //        .Parameter(htm => htm.Hours, "hours"))
            //    .Update(sp => sp.HasName("HaveTutored_Update")
            //        .Parameter(htm => htm.Ssn, "ssn")
            //        .Parameter(htm => htm.Code, "code")
            //        .Parameter(htm => htm.StartTime, "startTime")
            //        .Parameter(htm => htm.EndTime, "endTime")
            //        .Parameter(htm => htm.Hours, "hours"))
            //    .Delete(sp => sp.HasName("HaveTutored_Delete")
            //        .Parameter(htm => htm.Ssn, "ssn")
            //        .Parameter(htm => htm.Code, "code")
            //        .Parameter(htm => htm.StartTime, "startTime")
            //        .Parameter(htm => htm.EndTime, "endTime"))
            //);

            // Map stored procedures to entity Tutor
            modelBuilder.Entity<Tutor>().MapToStoredProcedures(t => t
                .Insert(sp => sp.HasName("Tutor_Add")
                    .Parameter(tm => tm.Ssn, "ssn")
                    .Parameter(tm => tm.FirstName, "firstName")
                    .Parameter(tm => tm.LastName, "lastName")
                    .Parameter(tm => tm.Email, "email")
                    .Parameter(tm => tm.Password, "password"))
                .Update(sp => sp.HasName("Tutor_Update")
                    .Parameter(tm => tm.Ssn, "ssn")
                    .Parameter(tm => tm.FirstName, "firstName")
                    .Parameter(tm => tm.LastName, "lastName")
                    .Parameter(tm => tm.Email, "email")
                    .Parameter(tm => tm.Password, "password"))
                .Delete(sp => sp.HasName("Tutor_Delete")
                    .Parameter(tm => tm.Ssn, "ssn"))
            );

            // Map stored procedures to entity TutoringSession
            modelBuilder.Entity<TutoringSession>().MapToStoredProcedures(ts => ts
                .Insert(sp => sp.HasName("TutoringSession_Add")
                    .Parameter(tsm => tsm.Code, "code")
                    .Parameter(tsm => tsm.StartTime, "startTime")
                    .Parameter(tsm => tsm.EndTime, "endTime")
                    .Parameter(tsm => tsm.NumberOfParticipants, "numberOfParticipants"))
                .Update(sp => sp.HasName("TutoringSession_Update")
                    .Parameter(tsm => tsm.Code, "code")
                    .Parameter(tsm => tsm.StartTime, "startTime")
                    .Parameter(tsm => tsm.EndTime, "endTime")
                    .Parameter(tsm => tsm.NumberOfParticipants, "numberOfParticipants"))
                .Delete(sp => sp.HasName("TutoringSession_Delete")
                    .Parameter(tsm => tsm.Code, "code")
                    .Parameter(tsm => tsm.StartTime, "startTime")
                    .Parameter(tsm => tsm.EndTime, "endTime"))
            );

            //modelBuilder.Entity<Course>().HasKey("Code");
            //modelBuilder.Entity<PlanToTutor>().HasKey("Ssn", "Code", "StartTime", "EndTime");
            //modelBuilder.Entity<HaveTutored>().HasKey("Ssn", "Code", "StartTime", "EndTime");
            //modelBuilder.Entity<Tutor>().HasKey("Ssn");
            //modelBuilder.Entity<TutoringSession>().HasKey("Code", "StartTime", "EndTime");

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=LabManager;User Id=sa;Password=INFdev1");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}

        public DbSet<Course> Course { get; set; }

        //public DbSet<HaveTutored> HaveTutored { get; set; }

        public DbSet<TutorTutoringSession> TutorTutoringSession { get; set; }

        public DbSet<Tutor> Tutor { get; set; }

        public DbSet<TutoringSession> TutoringSession { get; set; }
    }
}

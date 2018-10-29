using LabManager.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LabManager.Database.Context
{
    public class LabManagerDbContext : DbContext
    {
        public LabManagerDbContext()
        {

            Database.Log = Console.Write;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>().Property(x => x.Credits).HasColumnType("decimal");

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

            // Map stored procedures to entity TutorTutoringSession
            modelBuilder.Entity<TutorTutoringSession>().MapToStoredProcedures(ppt => ppt
                .Insert(sp => sp.HasName("TutorTutoringSession_Add")
                    .Parameter(pptm => pptm.Ssn, "ssn")
                    .Parameter(pptm => pptm.Code, "code")
                    .Parameter(pptm => pptm.StartTime, "startTime")
                    .Parameter(pptm => pptm.EndTime, "endTime"))
                .Update(sp => sp.HasName("TutorTutoringSession_Update")
                    .Parameter(pptm => pptm.Ssn, "ssn")
                    .Parameter(pptm => pptm.Code, "code")
                    .Parameter(pptm => pptm.StartTime, "startTime")
                    .Parameter(pptm => pptm.EndTime, "endTime"))
                .Delete(sp => sp.HasName("TutorTutoringSession_Delete")
                    .Parameter(pptm => pptm.Ssn, "ssn")
                    .Parameter(pptm => pptm.Code, "code")
                    .Parameter(pptm => pptm.StartTime, "startTime")
                    .Parameter(pptm => pptm.EndTime, "endTime"))
            );

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

            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Course> Course { get; set; }

        public DbSet<TutorTutoringSession> TutorTutoringSession { get; set; }

        public DbSet<Tutor> Tutor { get; set; }

        public DbSet<TutoringSession> TutoringSession { get; set; }
    }
}

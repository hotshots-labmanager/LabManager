using LabManager.Database.Context;
using LabManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class DALNEW
    {
        public void AddCourse(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@code", c.Code));
                parameters.Add(new SqlParameter("@name", c.Name));
                parameters.Add(new SqlParameter("@credits", c.Credits));
                parameters.Add(new SqlParameter("@numberOfStudents", c.NumberOfStudents));

                context.Database.ExecuteSqlCommand("EXEC Course_Add @code, @name, @credits, @numberOfStudents", parameters);
            }
        }

        public void DeleteCourse(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                context.Course.FromSql("EXEC Course_Delete {0}", c.Code);
            }
        }

        public void AddTutor(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor tbTutor = context.Tutor.FromSql("EXEC Tutor_Add {0} {1} {2} {3} {4}", t.Ssn, t.FirstName, t.LastName, t.Email, t.Password).FirstOrDefault();
                context.SaveChanges();
            }
        }

        public void DeleteTutor(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor findTutor = GetTutor(t.Ssn);
                if ( findTutor == null)
                {
                    return;
                }
                Tutor dbTutor = context.Tutor.FromSql("EXEC Tutor_Delete {0}", t.Ssn).FirstOrDefault();
                context.SaveChanges();
            }
        }

        public void AddTutoringSession(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTs = context.TutoringSession.FromSql("EXEC TutoringSession_Add {0} {1} {2} {3}", ts.Code, ts.StartTime, ts.EndTime, ts.NumberOfParticipants).FirstOrDefault();
                context.SaveChanges();
            }
        }

        public void DeleteTutoringSession(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession findTs = GetTutoringSession(ts.Code, ts.StartTime, ts.EndTime);
                if (findTs == null)
                {
                    return;
                }
                TutoringSession dbTutoringSession = context.TutoringSession.FromSql("TutoringSession_Delete {0} {1} {2}", ts.Code, ts.StartTime, ts.EndTime).FirstOrDefault();
                context.SaveChanges();
            }
        }

        public Tutor GetTutor(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor dbTutor = context.Tutor.FromSql("EXEC Tutor_Get {0}", ssn).FirstOrDefault();
                return dbTutor;
            }
        }

        public Course GetCourse(String code)
        {
            using (var context = new LabManagerDbContext())
            {
                Course dbCourse = context.Course.FromSql("EXEC Course_Get {0}", code).FirstOrDefault();
                return dbCourse;
            }
        }

        public List<Course> GetAllCourses()
        {
            using (var context = new LabManagerDbContext())
            {
                List<Course> dbCourses = context.Course.FromSql("EXEC Course_GetAll").ToList();
                dbCourses.ForEach(x =>
                {
                    if (x.TutoringSessions != null)
                        x.TutoringSessions = GetAllTutoringSessions();
                });

                return dbCourses;

            }
        }

        public List<TutoringSession> GetAllTutoringSessions()
        {
            using(var context = new LabManagerDbContext())
            {
                List<TutoringSession> dbTs = context.TutoringSession.FromSql("EXEC TutoringSession_GetAll_code").ToList();
                dbTs.ForEach(x =>
                {
                    if (x.Course != null)
                        x.Course = GetCourse(x.Code);
                });

                return dbTs;
                
            }
        }

        public List<Tutor> GetAllTutors()
        {
            using (var context = new LabManagerDbContext())
            {
                List<Tutor> dbTutors = context.Tutor.FromSql("EXEC Tutor_GetAll").ToList();
                dbTutors.ForEach(x => 
                {
                    if (x.HaveTutored.Count == 0)
                        x.HaveTutored = GetHaveTutored(x.Ssn);
                });
                dbTutors.ForEach(x =>
                {
                    if (x.PlanToTutor.Count == 0)
                        x.PlanToTutor = GetPlanToTutor(x.Ssn);
                });
                return dbTutors;
            }
        }

        public TutoringSession GetTutoringSession(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTutoringSession = context.TutoringSession.FromSql("EXEC TutoringSession_Get {0} {1} {2}", code, startTime, endTime).FirstOrDefault();
                //dbTutoringSession.ForEach(...)
                return dbTutoringSession;
            }
        }
        
        public List<HaveTutored> GetHaveTutored(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                List<HaveTutored> dbHaveTutored = context.HaveTutored.FromSql("EXEC HaveTutored_GetAll_Ssn {0}", ssn).ToList();
                dbHaveTutored.ForEach(x =>
                {
                    if (x.Tutor != null)
                        x.Tutor = GetTutor(x.Ssn);
                });
                dbHaveTutored.ForEach(x =>
                {
                    if (x.TutoringSession != null)
                        x.TutoringSession = GetTutoringSession(x.Code, x.StartTime, x.EndTime);
                });
                return dbHaveTutored;
            }
        }

        public List<PlanToTutor> GetPlanToTutor(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                List<PlanToTutor> dbPlanToTutor = context.PlanToTutor.FromSql("EXEC PlanToTutor_GetAll_Ssn {0}", ssn).ToList();
                dbPlanToTutor.ForEach(x =>
                {
                    if (x.Tutor != null)
                        x.Tutor = GetTutor(x.Ssn);
                });
                dbPlanToTutor.ForEach(x =>
                {
                    if (x.TutoringSession != null)
                    x.TutoringSession = GetTutoringSession(x.Code, x.StartTime, x.EndTime);
                });
                return dbPlanToTutor;
            }
        }

    }
}

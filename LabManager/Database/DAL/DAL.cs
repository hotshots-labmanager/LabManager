using LabManager.Database.Context;
using LabManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace LabManager.Database.DAL
{
    public class DAL
    {
        public void AddCourse(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                context.Course.Add(c);
                context.SaveChanges();
            }
        }

        public void DeleteCourse(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                Course dbCourse = context.Course.Find(c.Code);
                if (dbCourse == null)
                {
                    return;
                }
                context.Course.Remove(dbCourse);
                context.SaveChanges();
            }
        }

        public Course GetCourse(String code)
        {
            using (var context = new LabManagerDbContext())
            {
                Course dbCourse = context.Course.Find(code);
                return dbCourse;
            }
        }

        public List<Course> GetAllCourses()
        {
            using (var context = new LabManagerDbContext())
            {
                List<Course> dbCourses = context.Course.Include(c => c.TutoringSessions).ToList();
                return dbCourses;
            }
        }

        //public HaveTutored GetHaveTutored(HaveTutored ht)
        //{
        //    return GetHaveTutored(ht.Ssn, ht.Code, ht.StartTime, ht.EndTime);
        //}

        //public HaveTutored GetHaveTutored(String ssn, String code, DateTime startTime, DateTime endTime)
        //{
        //    using (var context = new LabManagerDbContext())
        //    {
        //        HaveTutored dbHaveTutored = context.HaveTutored.SingleOrDefault(x => x.Ssn.Equals(ssn) && x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
        //        return dbHaveTutored;
        //    }
        //}

        //public List<HaveTutored> GetAllHaveTutored()
        //{
        //    using (var context = new LabManagerDbContext())
        //    {
        //        List<HaveTutored> dbHt = context.HaveTutored.Include(ht => ht.Tutor).Include(ht => ht.TutoringSession).ToList();
        //        return dbHt;
        //    }
        //}

        //public List<TutorTutoringSession> GetAllPlanToTutor()
        //{
        //    using (var context = new LabManagerDbContext())
        //    {
        //        List<TutorTutoringSession> dbPt = context.TutorTutoringSession.Include(pt => pt.Tutor).Include(pt => pt.TutoringSession).ToList();
        //        return dbPt;
        //    }
        //}

        public void AddTutor(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                context.Tutor.Add(t);
                context.SaveChanges();
            }
        }

        public void DeleteTutor(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor dbTutor = context.Tutor.Find(t.Ssn);
                if (dbTutor == null)
                {
                    return;
                }
                context.Tutor.Remove(dbTutor);
                context.SaveChanges();
            }
        }

        public Tutor GetTutor(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                //Tutor dbTutor = context.Tutor.Include(x => x.PlanToTutor).Include(x => x.HaveTutored).SingleOrDefault(x => x.Ssn.Equals(ssn));
                Tutor dbTutor = context.Tutor.Include(x => x.TutoringSessions.Select(ts => ts.TutoringSession)).SingleOrDefault(x => x.Ssn.Equals(ssn));
                return dbTutor;
            }
        }

        public List<Tutor> GetAllTutors()
        {
            using (var context = new LabManagerDbContext())
            {
                //List<Tutor> dbTutors = context.Tutor.FromSql("EXEC Tutor_GetAllTutors").ToList();
                //List<Tutor> dbTutors = context.Tutor
                //                                .Include(t => t.HaveTutored)
                //                                .ThenInclude(ht => ht.TutoringSession)
                //                                .ThenInclude(c => c.Course)
                //                                .Include(t => t.PlanToTutor)
                //                                .ThenInclude(pt => pt.TutoringSession)
                //                                .ThenInclude(c => c.Course)
                //                                .ToList();
                List<Tutor> dbTutors = context.Tutor
                                                //.Include(t => t.HaveTutored.Select(ts => ts.TutoringSession.Course))
                                                .Include(t => t.TutoringSessions.Select(ts => ts.TutoringSession.Course))
                                                .ToList();
                return dbTutors;
            }
        }

        public void AddTutoringSession(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                context.TutoringSession.Add(ts);
                context.SaveChanges();
            }
        }

        public void DeleteTutoringSession(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTutoringSession = context.TutoringSession.Find(ts.Code, ts.StartTime, ts.EndTime);
                if (dbTutoringSession == null)
                {
                    return;
                }
                context.TutoringSession.Remove(dbTutoringSession);
                context.SaveChanges();
            }
        }

        public TutoringSession GetTutoringSession(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTs = context.TutoringSession
                                        //.Include(x => x.HaveTutored)
                                        .Include(x => x.Tutors)
                                        .SingleOrDefault(x => x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
                return dbTs;
            }
        }

        public List<TutoringSession> GetAllTutoringSessions()
        {
            using (var context = new LabManagerDbContext())
            {
                //List<TutoringSession> dbTs = context.TutoringSession.Include(ts => ts.HaveTutored).Include(ts => ts.PlanToTutor).Include(ts => ts.Course).ToList();
                List<TutoringSession> dbTs = context.TutoringSession.Include(ts => ts.Tutors).Include(ts => ts.Course).ToList();
                return dbTs;
            }
        }

        public void UpdateTutoringSession(TutoringSessionUpdateDTO dtoUpdate)
        {
            TutoringSession old = dtoUpdate.Old;
            TutoringSession updated = dtoUpdate.Updated;
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTs = context.TutoringSession
                                        //.Include(x => x.HaveTutored)
                                        .Include(x => x.Tutors)
                                        .SingleOrDefault(x => x.Code.Equals(old.Code) && x.StartTime.Equals(old.StartTime) && x.EndTime.Equals(old.EndTime));
                if (dbTs == null)
                {
                    return;
                }

                //List<HaveTutored> addedHaveTutored = updated.HaveTutored.Except(dbTs.HaveTutored).ToList();
                //List<HaveTutored> deletedHaveTutored = dbTs.HaveTutored.Except(updated.HaveTutored).ToList();
                List<TutorTutoringSession> addedSessions = updated.Tutors.Except(dbTs.Tutors).ToList();
                List<TutorTutoringSession> deletedSessions = dbTs.Tutors.Except(updated.Tutors).ToList();

                // Which relations are just updated? I.e. already exists in the database but has changed values
                //List<HaveTutored> updatedHaveTutored = updated.HaveTutored.Where(x => dbTs.HaveTutored.Contains(x) && !GetHaveTutored(x).FullEquals(x)).ToList();
                //addedHaveTutored = addedHaveTutored.Except(updatedHaveTutored).ToList();

                List<TutorTutoringSession> updatedSessions = addedSessions.Where(x => dbTs.Tutors.Contains(x)).ToList();
                addedSessions = addedSessions.Except(updatedSessions).ToList();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Deleted entries
                        //deletedHaveTutored.ForEach(c => dbTs.HaveTutored.Remove(c));
                        deletedSessions.ForEach(c => dbTs.Tutors.Remove(c));

                        // Add the new tutoring session first (as to avoid foreign key violations)
                        context.TutoringSession.Add(updated);

                        // Added entries
                        //foreach (HaveTutored ht in addedHaveTutored)
                        //{
                        //    DbEntityEntry htEntry = context.Entry(ht);
                        //    if (htEntry.State == EntityState.Detached)
                        //    {
                        //        context.HaveTutored.Attach(ht);
                        //    }
                        //    context.HaveTutored.Add(ht);
                        //}

                        foreach (TutorTutoringSession ptt in addedSessions)
                        {
                            DbEntityEntry tutorEntry = context.Entry(ptt);
                            if (tutorEntry.State == EntityState.Detached)
                            {
                                context.TutorTutoringSession.Attach(ptt);
                            }
                            context.TutorTutoringSession.Add(ptt);
                        }

                        // Updated entries
                        //foreach (HaveTutored ht in updatedHaveTutored)
                        //{
                        //    // Daniel 2018-10-25: not needed anymore due to ON DELETE CASCADE in database code
                        //    //HaveTutored dbHt = context.HaveTutored.FirstOrDefault(x => x.Equals(ht));
                        //    //context.HaveTutored.Remove(dbHt);
                        //    //context.SaveChanges();
                        //    context.HaveTutored.Add(ht);
                        //}
                        foreach (TutorTutoringSession ptt in updatedSessions)
                        {
                            // Daniel 2018-10-25: not needed anymore due to ON DELETE CASCADE in database code
                            //PlanToTutor dbPtt = context.PlanToTutor.FirstOrDefault(x => x.Equals(ptt));
                            //context.PlanToTutor.Remove(dbPtt);
                            //context.SaveChanges();
                            context.TutorTutoringSession.Add(ptt);
                        }

                        context.TutoringSession.Remove(dbTs);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public Decimal GetStudentsPerTutorRatio(TutoringSession ts)
        {
            return GetStudentsPerTutorRatio(ts.Code, ts.StartTime, ts.EndTime);
        }

        public Decimal GetStudentsPerTutorRatio(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                object[] parameters = new object[3];
                parameters[0] = (new SqlParameter("@code", code));
                parameters[1] = (new SqlParameter("@startTime", startTime));
                parameters[2] = (new SqlParameter("@endTime", endTime));

                return context.Database.SqlQuery<Decimal>("SELECT dbo.TutoringSession_GetStudentsPerTutorRatio (@code, @startTime, @endTime)", parameters).FirstOrDefault();

            }
        }

        public int GetNumberOfTutors(TutoringSession ts)
        {
            return GetNumberOfTutors(ts.Code, ts.StartTime, ts.EndTime);
        }

        public int GetNumberOfTutors(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                object[] parameters = new object[3];
                parameters[0] = (new SqlParameter("@code", code));
                parameters[1] = (new SqlParameter("@startTime", startTime));
                parameters[2] = (new SqlParameter("@endTime", endTime));

                return context.Database.SqlQuery<int>("SELECT dbo.TutorTutoringSession_GetNumberOfTutors (@code, @startTime, @endTime)", parameters).FirstOrDefault();

            }
        }

        public Decimal GetTutorTutoringSessionHours(Tutor t)
        {
            return GetTutorTutoringSessionHours(t.Ssn);
        }

        public Decimal GetTutorTutoringSessionHours(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {

                
                SqlParameter parameter = new SqlParameter("@ssn", ssn);

                return context.Database.SqlQuery<Decimal>("SELECT dbo.Tutor_GetTutorTutoringSessionHours (@ssn)", parameter).FirstOrDefault();

            }
        }


        public bool Exists(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.Course.Any(x => x.Equals(c));
            }
        }

        //public bool Exists(HaveTutored ht)
        //{
        //    using (var context = new LabManagerDbContext())
        //    {
        //        return context.HaveTutored.Any(x => x.Equals(ht));
        //    }
        //}

        public bool Exists(TutorTutoringSession tts)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutorTutoringSession.Any(x => x.Equals(tts));
            }
        }

        public bool Exists(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutorTutoringSession.Any(x => x.Equals(t));
            }
        }

        public bool Exists(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutoringSession.Any(x => x.Equals(ts));
            }
        }


    }
}

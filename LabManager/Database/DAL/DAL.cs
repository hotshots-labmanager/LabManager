using LabManager.Database.Context;
using LabManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public HaveTutored GetHaveTutored(HaveTutored ht)
        {
            return GetHaveTutored(ht.Ssn, ht.Code, ht.StartTime, ht.EndTime);
        }

        public HaveTutored GetHaveTutored(String ssn, String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                HaveTutored dbHaveTutored = context.HaveTutored.SingleOrDefault(x => x.Ssn.Equals(ssn) && x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
                return dbHaveTutored;
            }
        }

        public List<HaveTutored> GetAllHaveTutored()
        {
            using (var context = new LabManagerDbContext())
            {
                List<HaveTutored> dbHt = context.HaveTutored.Include(ht => ht.Tutor).Include(ht => ht.TutoringSession).ToList();
                return dbHt;
            }
        }

        public List<PlanToTutor> GetAllPlanToTutor()
        {
            using (var context = new LabManagerDbContext())
            {
                List<PlanToTutor> dbPt = context.PlanToTutor.Include(pt => pt.Tutor).Include(pt => pt.TutoringSession).ToList();
                return dbPt;
            }
        }

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
                Tutor dbTutor = context.Tutor.Include(x => x.PlanToTutor).Include(x => x.HaveTutored).SingleOrDefault(x => x.Ssn.Equals(ssn));
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
                                                .Include(t => t.HaveTutored.Select(ts => ts.TutoringSession.Course))
                                                .Include(t => t.PlanToTutor.Select(ts => ts.TutoringSession.Course))
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
                                        .Include(x => x.HaveTutored)
                                        .Include(x => x.PlanToTutor)
                                        .SingleOrDefault(x => x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
                return dbTs;
            }
        }

        public List<TutoringSession> GetAllTutoringSessions()
        {
            using (var context = new LabManagerDbContext())
            {
                List<TutoringSession> dbTs = context.TutoringSession.Include(ts => ts.HaveTutored).Include(ts => ts.PlanToTutor).Include(ts => ts.Course).ToList();
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
                                        .Include(x => x.HaveTutored)
                                        .Include(x => x.PlanToTutor)
                                        .SingleOrDefault(x => x.Code.Equals(old.Code) && x.StartTime.Equals(old.StartTime) && x.EndTime.Equals(old.EndTime));
                if (dbTs == null)
                {
                    return;
                }

                List<HaveTutored> addedHaveTutored = updated.HaveTutored.Except(dbTs.HaveTutored).ToList();
                List<HaveTutored> deletedHaveTutored = dbTs.HaveTutored.Except(updated.HaveTutored).ToList();
                List<PlanToTutor> addedPlanToTutor = updated.PlanToTutor.Except(dbTs.PlanToTutor).ToList();
                List<PlanToTutor> deletedPlanToTutor = dbTs.PlanToTutor.Except(updated.PlanToTutor).ToList();

                // Which relations are just updated? I.e. already exists in the database but has changed values
                List<HaveTutored> updatedHaveTutored = updated.HaveTutored.Where(x => dbTs.HaveTutored.Contains(x) && !GetHaveTutored(x).FullEquals(x)).ToList();
                addedHaveTutored = addedHaveTutored.Except(updatedHaveTutored).ToList();

                List<PlanToTutor> updatedPlanToTutor = addedPlanToTutor.Where(x => dbTs.PlanToTutor.Contains(x)).ToList();
                addedPlanToTutor = addedPlanToTutor.Except(updatedPlanToTutor).ToList();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Deleted entries
                        deletedHaveTutored.ForEach(c => dbTs.HaveTutored.Remove(c));
                        deletedPlanToTutor.ForEach(c => dbTs.PlanToTutor.Remove(c));

                        // Added entries
                        foreach (HaveTutored ht in addedHaveTutored)
                        {
                            DbEntityEntry htEntry = context.Entry(ht);
                            if (htEntry.State == EntityState.Detached)
                            {
                                context.HaveTutored.Add(ht);
                            }
                            dbTs.HaveTutored.Add(ht);
                        }
                        foreach (PlanToTutor ptt in addedPlanToTutor)
                        {
                            DbEntityEntry tutorEntry = context.Entry(ptt);
                            if (tutorEntry.State == EntityState.Detached)
                            {
                                context.PlanToTutor.Attach(ptt);
                            }
                            dbTs.PlanToTutor.Add(ptt);
                        }

                        // Updated entries
                        foreach (HaveTutored ht in updatedHaveTutored)
                        {
                            HaveTutored dbHt = context.HaveTutored.FirstOrDefault(x => x.Equals(ht));

                            context.HaveTutored.Remove(dbHt);
                            context.SaveChanges();
                            context.HaveTutored.Add(ht);
                        }
                        foreach (PlanToTutor ptt in updatedPlanToTutor)
                        {
                            PlanToTutor dbPtt = context.PlanToTutor.FirstOrDefault(x => x.Equals(ptt));

                            context.PlanToTutor.Remove(dbPtt);
                            context.SaveChanges();
                            context.PlanToTutor.Add(ptt);
                        }

                        // Update the tutoring session itself
                        context.TutoringSession.Remove(dbTs);
                        context.SaveChanges();
                        context.TutoringSession.Add(updated);

                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public bool Exists(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.Course.Any(x => x.Equals(c));
            }
        }

        public bool Exists(HaveTutored ht)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.HaveTutored.Any(x => x.Equals(ht));
            }
        }

        public bool Exists(PlanToTutor ptt)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.PlanToTutor.Any(x => x.Equals(ptt));
            }
        }

        public bool Exists(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.PlanToTutor.Any(x => x.Equals(t));
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

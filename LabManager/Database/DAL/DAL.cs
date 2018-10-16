using LabManager.Database.Context;
using LabManager.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void UpdateTutoringSession(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTs = context.TutoringSession
                                        .Include(x => x.HaveTutored)
                                        .Include(x => x.PlanToTutor)
                                        .SingleOrDefault(x => x.Code.Equals(ts.Code) && x.StartTime.Equals(ts.StartTime) && x.EndTime.Equals(ts.EndTime));
                if (dbTs == null)
                {
                    return;
                }

                List<HaveTutored> addedHaveTutored = ts.HaveTutored.Except(dbTs.HaveTutored).ToList();
                List<HaveTutored> deletedHaveTutored = dbTs.HaveTutored.Except(ts.HaveTutored).ToList();
                List<PlanToTutor> addedPlanToTutor = ts.PlanToTutor.Except(dbTs.PlanToTutor).ToList();
                List<PlanToTutor> deletedPlanToTutor = dbTs.PlanToTutor.Except(ts.PlanToTutor).ToList();

                // Which relations are just updated? I.e. already exists in the database
                List<HaveTutored> updatedHaveTutored = addedHaveTutored.Where(x => Exists(x)).ToList();
                addedHaveTutored = addedHaveTutored.Except(updatedHaveTutored).ToList();
                
                List<PlanToTutor> updatedPlanToTutor = addedPlanToTutor.Where(x => Exists(x)).ToList();
                addedPlanToTutor = addedPlanToTutor.Except(updatedPlanToTutor).ToList();

                // Deleted entries
                deletedHaveTutored.ForEach(c => dbTs.HaveTutored.Remove(c));
                deletedPlanToTutor.ForEach(c => dbTs.PlanToTutor.Remove(c));

                // Add entries
                foreach (HaveTutored ht in addedHaveTutored)
                {
                    EntityEntry htEntry = context.Entry(ht);
                    if (htEntry.State == EntityState.Detached)
                    {
                        ht.Tutor = context.Tutor.FirstOrDefault(x => x.Ssn.Equals(ht.Ssn));
                        ht.TutoringSession = context.TutoringSession.FirstOrDefault(x => x.Code.Equals(ht.Code) && x.StartTime.Equals(ht.StartTime) && x.EndTime.Equals(ht.EndTime));
                    }
                    context.HaveTutored.Add(ht);
                    dbTs.HaveTutored.Add(ht);
                }

                foreach (HaveTutored ht in updatedHaveTutored)
                {
                    //context.Entry(ht).State = EntityState.Modified;
                    //if (htEntry.State == EntityState.Modified)
                    //{
                    HaveTutored dbHt = context.HaveTutored.FirstOrDefault(x => x.Ssn.Equals(ht.Ssn) && x.Code.Equals(ht.Code) && x.StartTime.Equals(ht.StartTime) && x.EndTime.Equals(ht.EndTime));

                    context.HaveTutored.Remove(dbHt);
                    context.SaveChanges();
                    context.HaveTutored.Add(ht);
                    //}
                }

                //foreach (PlanToTutor ptt in addedPlanToTutor)
                //{
                //    EntityEntry tutorEntry = context.Entry(ptt);
                //    if (tutorEntry.State == EntityState.Detached)
                //    {
                //        context.PlanToTutor.Attach(ptt);
                //    }
                //    dbTs.PlanToTutor.Add(ptt);
                //}

                context.SaveChanges();
            }
        }

        public bool Exists(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutoringSession.Any(x => x.Equals(ts));
            }
        }

        public bool Exists(HaveTutored ht)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.HaveTutored.Any(x => x.Code.Equals(ht.Code) && x.StartTime.Equals(ht.StartTime) && x.EndTime.Equals(ht.EndTime));
            }
        }

        public bool Exists(PlanToTutor ptt)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.PlanToTutor.Any(x => x.Code.Equals(ptt.Code) && x.StartTime.Equals(ptt.StartTime) && x.EndTime.Equals(ptt.EndTime));
            }
        }

    }
}

using LabManager.Database.Context;
using LabManager.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class TutoringSessionDAL
    {
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
                TutoringSession dbTutoringSession = context.TutoringSession.
                                                            Include(x => x.HaveTutored).
                                                            Include(x => x.PlanToTutor).
                                                            SingleOrDefault(x => x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
                //var a = dbTutoringSession.Tutors;

                return dbTutoringSession;

            }

        }

        public void UpdateTutoringSession(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {


                TutoringSession dbTs = context.TutoringSession.Find(ts.Code, ts.StartTime, ts.EndTime);

                if (dbTs == null)
                {
                    return;
                }

                List<HaveTutored> addedHaveTutored = ts.HaveTutored.Except(dbTs.HaveTutored).ToList();
                List<HaveTutored> deletedHaveTutored = dbTs.HaveTutored.Except(ts.HaveTutored).ToList();
                List<Tutor> addedPlanToTutor = ts.PlanToTutor.Except(dbTs.PlanToTutor).ToList();
                List<Tutor> deletedPlanToTutor = dbTs.PlanToTutor.Except(ts.PlanToTutor).ToList();

                deletedHaveTutored.ForEach(c => dbTs.HaveTutored.Remove(c));
                deletedPlanToTutor.ForEach(c => dbTs.PlanToTutor.Remove(c));

                foreach (HaveTutored ht in addedHaveTutored)
                {

                    DbEntityEntry tutorEntry = context.Entry(ht);
                    if (tutorEntry.State == EntityState.Detached)
                    {
                        EntityState tutoringSessionState = context.Entry(ht.TutorSession).State;
                        //if (tutoringSessionState == EntityState.)
                        context.HaveTutored.Attach(ht);
                    }
                    dbTs.HaveTutored.Add(ht);
                }

                foreach (Tutor t in addedPlanToTutor)
                {

                    DbEntityEntry tutorEntry = context.Entry(t);
                    if (tutorEntry.State == EntityState.Detached)
                    {
                        context.Tutor.Attach(t);
                    }
                    dbTs.PlanToTutor.Add(t);
                }

                context.SaveChanges();
            }

        }
    }
}

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
                TutoringSession dbTutoringSession = context.TutoringSession.SingleOrDefault(x => x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
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

                //List<Tutor> addedTutor = ts.Tutors.Except(dbTs.Tutors).ToList();
                //List<Tutor> deletedTutor = dbTs.Tutors.Except(ts.Tutors).ToList();

                //deletedTutor.ForEach(c => dbTs.Tutors.Remove(c));

                //foreach (Tutor t in addedTutor)
                //{
                //    t.TutoringSessions = null;

                //    DbEntityEntry tutorEntry = context.Entry(t);
                //    if (tutorEntry.State == EntityState.Detached)
                //    {
                //        context.Tutor.Attach(t);
                //    }
                //    dbTs.Tutors.Add(t);

                //}

                //context.SaveChanges();
            }

        }
    }
}

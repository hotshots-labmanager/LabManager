using LabManager.Database.Context;
using LabManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class DALNEW
    {
        public Tutor GetTutor(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor dbTutor = context.Tutor.FromSql("EXEC Tutor_Get {0}", ssn).FirstOrDefault();
                return dbTutor;
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

        public TutoringSession GetTutoringSession(String ssn, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTutoringSession = context.TutoringSession.FromSql("EXEC TutoringSession_Get {0} {1} {2}", ssn, startTime, endTime).FirstOrDefault();
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

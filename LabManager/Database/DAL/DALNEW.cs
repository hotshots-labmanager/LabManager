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
        public List<Tutor> GetAllTutors()
        {
            using (var context = new LabManagerDbContext())
            {
                List<Tutor> dbTutors = context.Tutor.FromSql("EXEC Tutor_GetAllTutors").ToList();
                dbTutors.ForEach(x => x.HaveTutored = GetHaveTutored(x.Ssn));
                //dbTutors.ForEach(x => x.PlanToTutor = GetPlanToTutor(x.Ssn));
                return dbTutors;
            }
        }
        
        public List<HaveTutored> GetHaveTutored(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                List<HaveTutored> dbHaveTutored = context.HaveTutored.FromSql("EXEC HaveTutored_GetAll_Ssn {0}", ssn).ToList();
                //dbHaveTutored.ForEach(x => x.HaveTutored = GetHaveTutored(x.Ssn));
                return dbHaveTutored;
            }
        }

        public List<PlanToTutor> GetPlanToTutor(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                List<PlanToTutor> dbPlanToTutor = context.PlanToTutor.FromSql("EXEC PlanToTutor_GetAll_Ssn {0}", ssn).ToList();
                //dbHaveTutored.ForEach(x => x.HaveTutored = GetHaveTutored(x.Ssn));
                return dbPlanToTutor;
            }
        }

    }
}

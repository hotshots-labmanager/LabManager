using LabManager.Database.Context;
using LabManager.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class TutorDAL
    {
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
                Tutor dbTutor = context.Tutor.Find(ssn);


                return dbTutor;

            }

        }

        //public List<Tutor> GetTutors()
        //{
        //    using (var context = new LabManagerDbContext)
        //    {
        //        List<Tutor> dbTutors = context.Tutor.
        //    }
        //}
    }
}

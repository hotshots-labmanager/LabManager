using LabManager.Database.Context;
using LabManager.Database.Model;
using System;
using System.Collections.Generic;
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
                context.TutoringSession.Attach(ts);
                context.TutoringSession.Add(ts);
                context.SaveChanges();

            }
        }
    }
}

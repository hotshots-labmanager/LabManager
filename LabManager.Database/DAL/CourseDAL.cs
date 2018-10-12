using LabManager.Database.Context;
using LabManager.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class CourseDAL
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

        public Course FindCourse(Course c, String code)
        {
            using (var context = new LabManagerDbContext())
            {
                context.Course.Find(c, code);
                return c;
            }
        }
    }
}

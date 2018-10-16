using LabManager.Database.DAL;
using LabManager.Model;
using LabManager.Utility;
using System;

namespace LabManager.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*String hash = PasswordUtility.HashPassword("abc");

            Console.WriteLine(hash);
            
            Console.ReadKey();*/
            //CourseDAL cd = new CourseDAL();
            //TutorDAL td = new TutorDAL();
            //TutoringSessionDAL tsd = new TutoringSessionDAL();

            DAL dal = new DAL();

            /*Course c = new Course("123", "Databas", 7.5, 100);

            cd.AddCourse(c);
            td.AddTutor(tutor); 

            Tutor tutor = new Tutor("123", "Klas", "Johan", "Klas@", "123");
            td.DeleteTutor(tutor);*/

            //HaveTutored ht = new HaveTutored();
            //ht.Tutor = td.

            DateTime startTime = new DateTime(2017, 10, 04, 08, 00, 00);
            DateTime endTime = new DateTime(2017, 10, 04, 10, 00, 00);
            //TutoringSession ts = new TutoringSession("123", startTime, endTime, 50);
            ////tsd.AddTutoringSession(ts);
            //tsd.DeleteTutoringSession(ts);

            TutoringSession ts = dal.GetTutoringSession("INFC20", startTime, endTime);
            Tutor t = dal.GetTutor("333");

            HaveTutored ht = new HaveTutored();
            ht.Code = ts.Code;
            ht.Ssn = t.Ssn;
            ht.EndTime = ts.EndTime;
            ht.StartTime = ts.StartTime;
            ht.Hours = 0.5M;

            ts.AddHaveTutored(ht);

            dal.UpdateTutoringSession(ts);

            Console.ReadKey();
        }
    }
}

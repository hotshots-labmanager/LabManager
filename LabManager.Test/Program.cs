
using LabManager.Database.DAL;
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
            CourseDAL cd = new CourseDAL();
            TutorDAL td = new TutorDAL();
            TutoringSessionDAL tsd = new TutoringSessionDAL();


            /*Course c = new Course("123", "Databas", 7.5, 100);

            cd.AddCourse(c);
            td.AddTutor(tutor); 

            Tutor tutor = new Tutor("123", "Klas", "Johan", "Klas@", "123");
            td.DeleteTutor(tutor);*/

            //HaveTutored ht = new HaveTutored();
            //ht.Tutor = td.

            //DateTime startTime = new DateTime(2018, 10, 12, 10, 00, 00);
            //DateTime endTime = new DateTime(2018, 10, 12, 12, 00, 00);
            //TutoringSession ts = new TutoringSession("123", startTime, endTime, 50);
            ////tsd.AddTutoringSession(ts);
            //tsd.DeleteTutoringSession(ts);

            Console.WriteLine(td.GetTutor("333").FirstName);
            Console.WriteLine(tsd.GetTutoringSession("INFC20", new DateTime(2017, 10, 04, 08, 00, 00), new DateTime(2017, 10, 04, 10, 00, 00)).Code);
          
            Console.WriteLine(cd.GetCourse("INFC20").Name);
            Console.ReadKey();
            
            






        }
    }
}

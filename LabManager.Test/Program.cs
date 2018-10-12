using LabManager.Database.DAL;
using LabManager.Database.Model;
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
            CourseDAL cd = new CourseDAL();
            TutorDAL td = new TutorDAL();
            TutoringSessionDAL tsd = new TutoringSessionDAL();
            

            /*Course c = new Course("123", "Databas", 7.5, 100);

            cd.AddCourse(c);

            Tutor tutor = new Tutor("123", "Klas", "Johan", "Klas@", "123");

            td.AddTutor(tutor);*/


            

        }
    }
}

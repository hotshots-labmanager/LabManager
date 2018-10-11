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

            Course course = new Course { Code = "123", Name = "Databas", Credits = 7.5, NumberOfStudents = 100 };
            CourseDAL cd = new CourseDAL();
            cd.AddCourse(course);

        }
    }
}

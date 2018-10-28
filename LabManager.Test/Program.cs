using LabManager.Database.DAL;
using LabManager.Model;
using LabManager.Utility;
using LabManager.Utility.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabManager.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DAL dal = new DAL();


            DateTime startTime = new DateTime(2017, 10, 04, 08, 00, 00);
            DateTime endTime = new DateTime(2017, 10, 04, 10, 00, 00);

            TutoringSession ts = new TutoringSession("INFC20", startTime, endTime, 10);

            //try
            //{
            //    dal.AddTutoringSession(ts);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ExceptionHandler.GetErrorMessage(ex));
            //}

            //Course c1 = dal.GetCourse("INFC20");
            //c1.Name = "HEJSAN";
            //dal.UpdateCourse(c1);


            Console.ReadKey();
            
        }
    }
}

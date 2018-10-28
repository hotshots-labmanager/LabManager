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

            //Random r = new Random();
            //Course testCourse = new Course(r.Next(65535).ToString(), "testname", 10M, 10);

            //dal.AddCourse(testCourse);

            //DateTime tsOldStartTime1 = new DateTime(2017, 10, 04, 08, 00, 00);
            //DateTime tsOldEndTime1 = new DateTime(2017, 10, 04, 10, 00, 00);

            //TutoringSession ts = dal.GetTutoringSession("INFC20", tsOldStartTime1, tsOldEndTime1);

            //Console.WriteLine(dal.GetStudentsPerTutorRatio(ts.Code, ts.StartTime, ts.EndTime));

            //Console.WriteLine(dal.GetNumberOfTutors(ts.Code, ts.StartTime, ts.EndTime));

            //Console.WriteLine(dal.GetTutorTutoringSessionHours("111"));

            //Tutor t1 = dal.GetTutor("111");
            //List<TutoringSession> t2 = dal.GetAllTutoringSessions();

            //IEnumerable<TutoringSession> t1Tutoring = t1.TutoringSessions.Select(x => x.TutoringSession);

            ////IEnumerable<TutoringSession> tss2 = t2;

            //IEnumerable<TutoringSession> availableSessions = t2.Except(t1Tutoring);

            //Console.WriteLine("Handledare " + t1.FullName + " har dessa pass lediga: ");
            //foreach (TutoringSession ts in availableSessions)
            //{
            //    Console.WriteLine(ts.Code + " " + ts.StartTime + " " + ts.EndTime);
            //}


            //DateTime tsStartTime = new DateTime(2017, 10, 04, 09, 00, 00);
            //DateTime tsEndTime = new DateTime(2017, 10, 04, 10, 00, 00);

            //TutoringSession ts = new TutoringSession("INFC94", tsStartTime, tsEndTime, 50); 

            //try
            //{
            //    dal.AddTutoringSession(ts);
            //}

            //catch (Exception ex)
            //{
            //   String message = ExceptionHandler.GetErrorMessage(ex);
            //    Console.WriteLine(message);
            //}

            TutoringSession ts = new TutoringSession("INFC20", startTime, endTime, 10);

            try
            {
                dal.AddTutoringSession(ts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ExceptionHandler.GetErrorMessage(ex));
            }

            Console.ReadKey();
            
        }
    }
}

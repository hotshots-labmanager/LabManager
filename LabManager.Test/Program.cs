﻿using LabManager.Database.DAL;
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
            /*String hash = PasswordUtility.HashPassword("abc");

            Console.WriteLine(hash);
            
            Console.ReadKey();*/
            //CourseDAL cd = new CourseDAL();
            //TutorDAL td = new TutorDAL();
            //TutoringSessionDAL tsd = new TutoringSessionDAL();

            DAL dal = new DAL();

            //cd.AddCourse(c);
            

            ////Tutor tutor = new Tutor("123", "Klas", "Johan", "Klas@", "123");
            ////dal.AddTutor(tutor);

            //HaveTutored ht = new HaveTutored();
            //ht.Tutor = td.

            DateTime startTime = new DateTime(2017, 10, 04, 08, 00, 00);
            DateTime endTime = new DateTime(2017, 10, 04, 10, 00, 00);
            //TutoringSession ts = new TutoringSession("123", startTime, endTime, 50);
            ////tsd.AddTutoringSession(ts);
            //tsd.DeleteTutoringSession(ts);

            // Nedanstående kod fungerar vid uppdatering och lägga till (Daniel 2018-10-18)
            //TutoringSession ts = dal.GetTutoringSession("INFC20", startTime, endTime);
            Tutor t = dal.GetTutor("333");

            //HaveTutored ht = new HaveTutored();
            //ht.Code = ts.Code;
            //ht.Ssn = t.Ssn;
            //ht.EndTime = ts.EndTime;
            //ht.StartTime = ts.StartTime;
            //ht.Hours = 1M;

            //ts.AddHaveTutored(ht);

            //dal.UpdateTutoringSession(ts);

            //DateTime tsOldStartTime1 = new DateTime(2017, 10, 04, 10, 00, 00);
            //DateTime tsOldEndTime1 = new DateTime(2017, 10, 04, 12, 00, 00);
            //TutoringSession tsUpdateOld = dal.GetTutoringSession("INFC20", tsOldStartTime1, tsOldEndTime1);

            //TutoringSession tsUpdateUpdated = dal.GetTutoringSession("INFC20", tsOldStartTime1, tsOldEndTime1);
            //tsUpdateUpdated.EndTime = new DateTime(2017, 10, 04, 13, 00, 00);
            ////tsUpdateUpdated.HaveTutored.ElementAt(0).Hours = 1;

            //dal.UpdateTutoringSession(new TutoringSessionUpdateDTO(tsUpdateOld, tsUpdateUpdated));

            //DALNEW dalnew = new DALNEW();

            //Course c = new Course("SYSA23", "SAFETY", 7.5M, 50);
            //dal.AddCourse(c);

            //dal.DeleteCourse(c);


            //List<Tutor> tutors = dalnew.GetAllTutors();
            //foreach (Tutor t1 in tutors)
            //{
            //    foreach (HaveTutored ht in t1.HaveTutored)
            //    {
            //        Console.WriteLine(ht.Code);
            //    }
            //}

            //List<Tutor> tutors = dal.GetAllTutors();
            //foreach (Tutor t1 in tutors)
            //{
            //    Console.WriteLine(t1.FirstName + " handleder på kurser: ");
            //    foreach (TutorTutoringSession ht in t1.TutoringSessions)
            //    {
            //        Console.WriteLine(ht.TutoringSession.Course.Name);
            //    }
            //}

            //foreach (Course c in dal.GetAllCourses())
            //{
            //    Console.WriteLine(c.Code + " " + c.Name + " " + c.NumberOfStudents);
            //}

            //foreach (TutoringSession ts in dal.GetAllTutoringSessions())
            //{
            //    Console.WriteLine(ts.Code + " " +  ts.StartTime + " " + ts.EndTime + " " + ts.NumberOfParticipants);
            //}

            //Random r = new Random();
            //Course testCourse = new Course(r.Next(65535).ToString(), "testname", 10M, 10);

            //dal.AddCourse(testCourse);

            //DateTime tsOldStartTime1 = new DateTime(2017, 10, 04, 08, 00, 00);
            //DateTime tsOldEndTime1 = new DateTime(2017, 10, 04, 10, 00, 00);

            //TutoringSession ts = dal.GetTutoringSession("INFC20", tsOldStartTime1, tsOldEndTime1);

            //Console.WriteLine(dal.GetStudentsPerTutorRatio(ts.Code, ts.StartTime, ts.EndTime));

            //Console.WriteLine(dal.GetNumberOfTutors(ts.Code, ts.StartTime, ts.EndTime));

            //Console.WriteLine(dal.GetTutorTutoringSessionHours("111"));

            Tutor t1 = dal.GetTutor("111");
            List<TutoringSession> t2 = dal.GetAllTutoringSessions();

            IEnumerable<TutoringSession> t1Tutoring = t1.TutoringSessions.Select(x => x.TutoringSession);

            //IEnumerable<TutoringSession> tss2 = t2;

            IEnumerable<TutoringSession> availableSessions = t2.Except(t1Tutoring);

            Console.WriteLine("Handledare " + t1.FullName + " har dessa pass lediga: ");
            foreach (TutoringSession ts in availableSessions)
            {
                Console.WriteLine(ts.Code + " " + ts.StartTime + " " + ts.EndTime);
            }
            

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

            try
            {
                Tutor t4 = new Tutor("111", "hej", "hej", "aaa", "pass");
                dal.AddTutor(t4);
            }
            catch (Exception ex)
            {
                String message = ExceptionHandler.GetErrorMessage(ex);
                Console.WriteLine(message);
            }

            DateTime startAdd = new DateTime(2017, 10, 04, 11, 00, 00);
            DateTime endAdd = new DateTime(2017, 10, 04, 11, 30, 00);

            //TutoringSession ts11 = new TutoringSession("INFC20", startAdd, endAdd, 20);
            //try
            //{
            //    dal.AddTutoringSession(ts11);
            //}

            //catch (Exception ex)
            //{
            //    String message = ExceptionHandler.GetErrorMessage(ex);
            //    Console.WriteLine(message);
            //}
            
            Console.ReadKey();
            
        }
    }
}

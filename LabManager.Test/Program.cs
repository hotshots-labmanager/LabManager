﻿using LabManager.Database.DAL;
using LabManager.Model;
using LabManager.Utility;
using System;
using System.Collections.Generic;

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
            //DateTime tsOldEndTime1 = new DateTime(2017, 10, 04, 13, 00, 00);
            //TutoringSession tsUpdateOld = dal.GetTutoringSession("INFC20", tsOldStartTime1, tsOldEndTime1);

            //TutoringSession tsUpdateUpdated = dal.GetTutoringSession("INFC20", tsOldStartTime1, tsOldEndTime1);
            //tsUpdateUpdated.EndTime = new DateTime(2017, 10, 04, 12, 00, 00);

            //dal.UpdateTutoringSession(new TutoringSessionUpdateDTO(tsUpdateOld, tsUpdateUpdated));

            DALNEW dalnew = new DALNEW();

            Course c = new Course("SYSA23", "Säkerhet", 7.5M, 50);
            dalnew.AddCourse(c);

            //dal.DeleteCourse(c);


            //List<Tutor> tutors = dalnew.GetAllTutors();
            //foreach (Tutor t1 in tutors)
            //{
            //    foreach (HaveTutored ht in t1.HaveTutored)
            //    {
            //        Console.WriteLine(ht.Code);
            //    }
            //}


            //foreach (Tutor t1 in tutors) {
            //    Console.WriteLine(t1.FirstName + " handleder på kurser: ");
            //    foreach (HaveTutored ht in t1.HaveTutored)
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



            //Console.ReadKey();



        }
    }
}

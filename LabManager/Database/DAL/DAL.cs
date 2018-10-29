using LabManager.Database.Context;
using LabManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace LabManager.Database.DAL
{
    public class DAL
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

        public Course GetCourse(String code)
        {
            using (var context = new LabManagerDbContext())
            {
                Course dbCourse = context.Course.Find(code);
                return dbCourse;
            }
        }

        public List<Course> GetAllCourses()
        {
            using (var context = new LabManagerDbContext())
            {
                List<Course> dbCourses = context.Course
                                                .Include(x => x.TutoringSessions.Select(ts => ts.Tutors))
                                                .Include(x => x.TutoringSessions.Select(ts => ts.Course))
                                                .ToList();
                return dbCourses;
            }
        }

        public void UpdateCourse(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                Course dbC = context.Course
                                    .Include(x => x.TutoringSessions.Select(ts => ts.Tutors))
                                    .Include(x => x.TutoringSessions.Select(ts => ts.Course))
                                    .SingleOrDefault(x => x.Code.Equals(c.Code));
                if (dbC == null)
                {
                    return;
                }
                context.Entry(dbC).CurrentValues.SetValues(c);
                context.SaveChanges();
            }
        }

        public void AddTutor(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                context.Tutor.Add(t);
                context.SaveChanges();
            }
        }

        public void DeleteTutor(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor dbTutor = context.Tutor.Find(t.Ssn);
                if (dbTutor == null)
                {
                    return;
                }
                context.Tutor.Remove(dbTutor);
                context.SaveChanges();
            }
        }

        public Tutor GetTutor(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                Tutor dbTutor = context.Tutor
                                       .Include(x => x.TutoringSessions.Select(ts => ts.Tutor))
                                       .Include(x => x.TutoringSessions.Select(ts => ts.TutoringSession))
                                       .SingleOrDefault(x => x.Ssn.Equals(ssn));
                return dbTutor;
            }
        }

        public List<Tutor> GetAllTutors()
        {
            using (var context = new LabManagerDbContext())
            {
                List<Tutor> dbTutors = context.Tutor
                                              .Include(x => x.TutoringSessions.Select(ts => ts.Tutor))
                                              .Include(t => t.TutoringSessions.Select(ts => ts.TutoringSession.Course))
                                              .ToList();
                return dbTutors;
            }
        }

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
                TutoringSession dbTutoringSession = context.TutoringSession
                                                           .Find(ts.Code, ts.StartTime, ts.EndTime);
                if (dbTutoringSession == null)
                {
                    return;
                }
                context.TutoringSession.Remove(dbTutoringSession);
                context.SaveChanges();
            }
        }

        public TutoringSession GetTutoringSession(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTs = context.TutoringSession
                                              .Include(x => x.Tutors)
                                              .SingleOrDefault(x => x.Code.Equals(code) && x.StartTime.Equals(startTime) && x.EndTime.Equals(endTime));
                return dbTs;
            }
        }

        public List<TutoringSession> GetAllTutoringSessions()
        {
            using (var context = new LabManagerDbContext())
            {
                List<TutoringSession> dbTs = context.TutoringSession
                                                    .Include(ts => ts.Tutors.Select(tts => tts.Tutor))
                                                    .Include(ts => ts.Tutors.Select(tts => tts.TutoringSession))
                                                    .Include(ts => ts.Course)
                                                    .ToList();
                return dbTs;
            }
        }

        public void UpdateTutoringSession(TutoringSessionUpdateDTO dtoUpdate)
        {
            TutoringSession old = dtoUpdate.Old;
            TutoringSession updated = dtoUpdate.Updated;
            using (var context = new LabManagerDbContext())
            {
                TutoringSession dbTs = context.TutoringSession
                                              .Include(x => x.Tutors)
                                              .SingleOrDefault(x => x.Code.Equals(old.Code) && x.StartTime.Equals(old.StartTime) && x.EndTime.Equals(old.EndTime));
                if (dbTs == null)
                {
                    return;
                }
                
                List<TutorTutoringSession> addedSessions = updated.Tutors.Except(dbTs.Tutors).ToList();
                List<TutorTutoringSession> deletedSessions = dbTs.Tutors.Except(updated.Tutors).ToList();

                //List<TutorTutoringSession> deepCopy = new List<TutorTutoringSession>();
                //foreach (TutorTutoringSession temp in addedSessions)
                //{
                //    Tutor tempTutor = temp.Tutor;
                //    Tutor t1 = new Tutor(tempTutor.Ssn, tempTutor.FirstName, tempTutor.LastName, tempTutor.Email, tempTutor.Password);


                //    TutoringSession tempTutoringSession = temp.TutoringSession;
                //    TutoringSession t2 = new TutoringSession(tempTutoringSession.Code, tempTutoringSession.StartTime, tempTutoringSession.EndTime, tempTutoringSession.NumberOfParticipants);

                //    TutorTutoringSession tttt = new TutorTutoringSession(t1, t2);
                //    deepCopy.Add(tttt);
                //}

                // Which relations are just updated? I.e. already exists in the database but has changed values
                //List<TutorTutoringSession> updatedSessions = addedSessions.Where(x => dbTs.Tutors.Contains(x)).ToList();
                //addedSessions = addedSessions.Except(updatedSessions).ToList();

                Course savedCourse = updated.Course;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Deleted entries
                        //deletedHaveTutored.ForEach(c => dbTs.HaveTutored.Remove(c));
                        deletedSessions.ForEach(c => dbTs.Tutors.Remove(c));

                        if (!old.Equals(updated))
                        {
                            // Keys have been changed
                            context.TutoringSession.Remove(dbTs);
                            context.TutoringSession.Add(updated);
                        }
                        else if (!old.FullEquals(updated))
                        {
                            

                            foreach (TutorTutoringSession ptt in addedSessions)
                            {
                                dbTs.Tutors.Add(ptt);
                                ptt.TutoringSession = dbTs;

                                ptt.TutoringSession.Course = null;

                                // förmodligen inte problemet
                                //dbTs.Course = null;

                                //foreach (TutoringSession t5 in ptt.TutoringSession)
                                //{

                                //}

                                context.SaveChanges();

                                ////context.Entry(ptt.Tutor).State = EntityState.Unchanged;
                                ////context.Entry(ptt.TutoringSession).State = EntityState.Unchanged;

                                //DbEntityEntry tutorEntry = context.Entry(ptt);
                                //if (tutorEntry.State == EntityState.Detached)
                                //{
                                //    //context.TutorTutoringSession.Attach(ptt);
                                //    //context.SaveChanges();
                                //}
                                //ptt.TutoringSession.Course = null;

                                //context.TutorTutoringSession.Add(ptt);
                                ////context.SaveChanges();
                            }

                            context.Entry(dbTs).CurrentValues.SetValues(updated);
                        }

                        // Add the new tutoring session first (as to avoid foreign key violations)
                        // This convoluted approach is needed because TutoringSession is a weak entity
                        // and is therefore a pain in the *** to update

                        //Course tmpCourse = context.Course.Find(updated.Code);
                        //tmpCourse = new Course { Code = tmpCourse.Code, Credits = tmpCourse.Credits, Name = tmpCourse.Name };

                        //context.Course.Attach(tmpCourse);
                        //updated.Course = tmpCourse;
                        //context.Entry(tmpCourse).State = EntityState.Detached;

                        //updated.Course.TutoringSessions = null;

                        //foreach (TutorTutoringSession tts in updated.Tutors)
                        //{
                        //    tts.Tutor = null;
                        //    tts.TutoringSession = null;
                        //}

                        //updated.Tutors.Select(x => x.)

                        //updated.Course = tmpCourse;

                        //context.SaveChanges();

                        //context.TutoringSession.Remove(dbTs);
                        ////Course tmpCourse = context.Course.Find(updated.Code);
                        ////if (tmpCourse != null)
                        ////{
                        ////    context.Course.Remove(tmpCourse);
                        ////}

                        //context.SaveChanges();
                        //updated.Course = null;
                        ////foreach (TutorTutoringSession tts in updated.Tutors)
                        ////{
                        ////    tts.TutoringSession.Course = null;
                        ////}
                        //context.TutoringSession.Add(updated);

                        ////context.Entry(updated.Course).State = EntityState.Modified;

                        //context.SaveChanges();

                        //if (!updated.FullEquals(old))
                        //{
                        //    dbTs.NumberOfParticipants = updated.NumberOfParticipants;
                        //}

                        // Added entries
                        //foreach (TutorTutoringSession ptt in deepCopy)
                        //{
                        //    context.Entry(ptt.Tutor).State = EntityState.Unchanged;
                        //    context.Entry(ptt.TutoringSession).State = EntityState.Unchanged;

                        //    DbEntityEntry tutorEntry = context.Entry(ptt);
                        //    if (tutorEntry.State == EntityState.Detached)
                        //    {
                        //        context.TutorTutoringSession.Attach(ptt);
                        //        context.SaveChanges();
                        //    }
                        //    context.TutorTutoringSession.Add(ptt);
                        //    context.SaveChanges();
                        //}

                        // Updated entries
                        //foreach (HaveTutored ht in updatedHaveTutored)
                        //{
                        //    // Daniel 2018-10-25: not needed anymore due to ON DELETE CASCADE in database code
                        //    //HaveTutored dbHt = context.HaveTutored.FirstOrDefault(x => x.Equals(ht));
                        //    //context.HaveTutored.Remove(dbHt);
                        //    //context.SaveChanges();
                        //    context.HaveTutored.Add(ht);
                        //}
                        //foreach (TutorTutoringSession ptt in updatedSessions)
                        //{
                        //    // Daniel 2018-10-25: not needed anymore due to ON DELETE CASCADE in database code
                        //    //PlanToTutor dbPtt = context.PlanToTutor.FirstOrDefault(x => x.Equals(ptt));
                        //    //context.PlanToTutor.Remove(dbPtt);
                        //    //context.SaveChanges();
                        //    context.TutorTutoringSession.Add(ptt);
                        //}

                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                    finally
                    {
                        updated.Course = savedCourse;
                    }
                }
            }
        }

        public Decimal GetStudentsPerTutorRatio(TutoringSession ts)
        {
            return GetStudentsPerTutorRatio(ts.Code, ts.StartTime, ts.EndTime);
        }

        public Decimal GetStudentsPerTutorRatio(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                object[] parameters = new object[3];
                parameters[0] = new SqlParameter("@code", code);
                parameters[1] = new SqlParameter("@startTime", startTime);
                parameters[2] = new SqlParameter("@endTime", endTime);

                return context.Database.SqlQuery<Decimal>("SELECT dbo.TutoringSession_GetStudentsPerTutorRatio (@code, @startTime, @endTime)", parameters).FirstOrDefault();
            }
        }

        public int GetNumberOfTutors(TutoringSession ts)
        {
            return GetNumberOfTutors(ts.Code, ts.StartTime, ts.EndTime);
        }

        public int GetNumberOfTutors(String code, DateTime startTime, DateTime endTime)
        {
            using (var context = new LabManagerDbContext())
            {
                object[] parameters = new object[3];
                parameters[0] = new SqlParameter("@code", code);
                parameters[1] = new SqlParameter("@startTime", startTime);
                parameters[2] = new SqlParameter("@endTime", endTime);

                return context.Database.SqlQuery<int>("SELECT dbo.TutorTutoringSession_GetNumberOfTutors (@code, @startTime, @endTime)", parameters).FirstOrDefault();
            }
        }

        public Decimal GetTutoredHours(Tutor t)
        {
            return GetTutoredHours(t.Ssn);
        }

        public Decimal GetTutoredHours(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                SqlParameter parameter = new SqlParameter("@ssn", ssn);

                return context.Database.SqlQuery<Decimal>("SELECT dbo.Tutor_GetTutoredHours (@ssn)", parameter).FirstOrDefault();
            }
        }

        public Decimal GetPlannedHours(Tutor t)
        {
            return GetPlannedHours(t.Ssn);
        }

        public Decimal GetPlannedHours(String ssn)
        {
            using (var context = new LabManagerDbContext())
            {
                SqlParameter parameter = new SqlParameter("@ssn", ssn);

                return context.Database.SqlQuery<Decimal>("SELECT dbo.Tutor_GetPlannedHours (@ssn)", parameter).FirstOrDefault();
            }
        }

        public bool Exists(Course c)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.Course.Any(x => x.Equals(c));
            }
        }

        public bool Exists(TutorTutoringSession tts)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutorTutoringSession.Any(x => x.Equals(tts));
            }
        }

        public bool Exists(Tutor t)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutorTutoringSession.Any(x => x.Equals(t));
            }
        }

        public bool Exists(TutoringSession ts)
        {
            using (var context = new LabManagerDbContext())
            {
                return context.TutoringSession.Any(x => x.Equals(ts));
            }
        }


    }
}

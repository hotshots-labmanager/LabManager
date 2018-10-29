using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LabManager.Database.DAL;
using LabManager.Model;
using LabManager.Utility;
using LabManager.Utility.ExceptionHandling;

namespace LabManager.ViewModel
{
    public class TutorsViewModel : INotifyPropertyChanged
    {
        private DAL dal;

        private ObservableCollection<Tutor> tutors;
        //private ObservableCollection<Tutor> tutorsLazy;

        private ObservableCollection<Course> courses;
        private ObservableCollection<TutoringSession> tutoringSessions;

        private ObservableCollection<TutoringSession> availableTutoringSessions;
        private ObservableCollection<TutoringSession> plannedTutoringSessions;

        private String status = "Ready!";
        private bool slideInEnabled = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public TutorsViewModel()
        {
            dal = new DAL();

            Courses = new ObservableCollection<Course>(dal.GetAllCourses());
            TutoringSessions = new ObservableCollection<TutoringSession>(dal.GetAllTutoringSessions());
            Tutors = new ObservableCollection<Tutor>(dal.GetAllTutors());
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Tutor selectedTutor;
        public Tutor SelectedTutor
        {
            get
            {

                return selectedTutor;
            }
            set
            {
                //selectedTutor != value &&
                if (value != null)
                {
                    selectedTutor = value;

                    NotifyPropertyChanged("SelectedTutor");
                    NotifyPropertyChanged("TutorTutoredHours");
                    NotifyPropertyChanged("TutorPlannedHours");
                    NotifyPropertyChanged("TutorLastSession");
                    NotifyPropertyChanged("TutorNextSession");
                    NotifyPropertyChanged("AvailableTutoringSessions");
                    NotifyPropertyChanged("PlannedTutoringSessions");
                }
            }
        }

        private Course selectedCourse;
        public Course SelectedCourse
        {
            get
            {
                return selectedCourse;
            }
            set
            {
                if (selectedCourse != value && value != null)
                {
                    selectedCourse = value;
                    NotifyPropertyChanged("SelectedCourse");
                }
            }
        }

        private TutoringSession selectedTutoringSession;
        public TutoringSession SelectedTutoringSession
        {
            get
            {
                return selectedTutoringSession;
            }
            set
            {
                if (selectedTutoringSession != value && value != null)
                {
                    selectedTutoringSession = value;
                    NotifyPropertyChanged("SelectedCTutoringSession");
                }
            }
        }
        
        public ObservableCollection<Tutor> Tutors
        {
            get
            {
                if (tutors == null)
                {
                    tutors = new ObservableCollection<Tutor>(dal.GetAllTutors());

                }
                return tutors;
            }
            set
            {
                if (tutors != value)
                {
                    tutors = value;
                    NotifyPropertyChanged("Tutors");
                }
            }
        }

        public ObservableCollection<Course> Courses

        {
            get
            {
                if (courses == null)
                {
                    courses = new ObservableCollection<Course>(dal.GetAllCourses());
                }
                return courses;
            }
            set
            {
                if (courses != value)
                {
                    courses = value;
                    NotifyPropertyChanged("Courses");
                }
            }
        }

        public ObservableCollection<TutoringSession> TutoringSessions
        {
            get
            {
                if (tutoringSessions == null)
                {
                    tutoringSessions = new ObservableCollection<TutoringSession>(dal.GetAllTutoringSessions());
                }
                return tutoringSessions;
            }
            set
            {
                if (tutoringSessions != value)
                {
                    tutoringSessions = value;
                    NotifyPropertyChanged("TutoringSessions");
                }
            }
        }
        public ObservableCollection<TutoringSession> AvailableTutoringSessions
        {
            get
            {
                if (selectedTutor != null)
                {
                    IEnumerable<TutoringSession> pls = selectedTutor.TutoringSessions.Select(x => x.TutoringSession).ToList();
                    IEnumerable<TutoringSession> avs = TutoringSessions.Except(pls);

                    availableTutoringSessions = new ObservableCollection<TutoringSession>(avs);
                    return availableTutoringSessions;
                }
                return null;
            }
            private set
            {
                if (availableTutoringSessions != value)
                {
                    availableTutoringSessions = value;
                    NotifyPropertyChanged("AvailableTutoringSessions");
                }
            }
        }

        public ObservableCollection<TutoringSession> PlannedTutoringSessions
        {
            get
            {
                if (selectedTutor != null)
                {
                    IEnumerable<TutoringSession> pls = selectedTutor.TutoringSessions.Select(x => x.TutoringSession);

                    plannedTutoringSessions = new ObservableCollection<TutoringSession>(pls);
                    return plannedTutoringSessions;
                }
                return null;
            }
            private set
            {
                if (plannedTutoringSessions != value)
                {
                    plannedTutoringSessions = value;
                    NotifyPropertyChanged("PlannedTutoringSessions");
                }
            }
        }


        public void AddCourse(String code, decimal credits, String name, int numberOfStudents)
        {
            try
            {
                Course tmpCourse = new Course(code, name, credits, numberOfStudents);

                dal.AddCourse(tmpCourse);
                Courses.Add(tmpCourse);
                NotifyPropertyChanged("Courses");
                Status = code + " - " + name + " " + " was added to courses!";
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void AddTutor(String ssn, String firstName, String lastName, String email, String password)
        {
            try
            {
                Tutor temp = new Tutor(ssn, firstName, lastName, email, password);
                dal.AddTutor(temp);
                Tutors.Add(temp);

                Status = temp.FullName + " was added to tutors!";
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeleteTutor(String ssn)
        {
            try
            {
                Tutor temp = new Tutor();
                temp.Ssn = ssn;

                dal.DeleteTutor(temp);
                Tutors.Remove(temp);

                //Tutors = new ObservableCollection<Tutor>(dal.GetAllTutors());
                NotifyPropertyChanged("Tutors");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void AddTutor(TutoringSession ts)
        {
            if (ts != null)
            {
                TutorTutoringSession tmptts = new TutorTutoringSession(selectedTutor, ts);
                try
                {
                    TutoringSessionUpdateDTO updateDTO = new TutoringSessionUpdateDTO(ts, ts);

                    ts.Tutors.Add(tmptts);
                    selectedTutor.TutoringSessions.Add(tmptts);

                    dal.UpdateTutoringSession(updateDTO);

                    Status = "Added to planned sessions";
                }
                catch (Exception ex)
                {
                    // Rollback
                    ts.Tutors.Remove(tmptts);
                    selectedTutor.TutoringSessions.Remove(tmptts);
                    Status = ExceptionHandler.GetErrorMessage(ex);
                }
                NotifyPropertyChanged("TutorTutoredHours");
                NotifyPropertyChanged("TutorPlannedHours");
                NotifyPropertyChanged("TutorLastSession");
                NotifyPropertyChanged("TutorNextSession");
                NotifyPropertyChanged("AvailableTutoringSessions");
                NotifyPropertyChanged("PlannedTutoringSessions");
            }
        }
        public void DeleteTutor(TutoringSession ts)
        {
            if (ts != null)
            {
                ICollection<TutorTutoringSession> tutorTutoringSessionsToBeDeleted = new List<TutorTutoringSession>();
                foreach (TutorTutoringSession tts in ts.Tutors)
                {
                    if (tts.Tutor.Equals(selectedTutor))
                    {
                        tutorTutoringSessionsToBeDeleted.Add(tts);
                    }
                }
                foreach (TutorTutoringSession tts in tutorTutoringSessionsToBeDeleted)
                {
                    ts.Tutors.Remove(tts);
                    selectedTutor.TutoringSessions.Remove(tts);
                }
                try
                {
                    TutoringSessionUpdateDTO updateDTO = new TutoringSessionUpdateDTO(ts, ts);
                    dal.UpdateTutoringSession(updateDTO);

                    Status = "Removed from planned sessions";
                }
                catch (Exception ex)
                {
                    // Rollback
                    foreach (TutorTutoringSession tts in ts.Tutors)
                    {
                        ts.Tutors.Add(tts);
                        selectedTutor.TutoringSessions.Add(tts);
                    }
                    Status = ExceptionHandler.GetErrorMessage(ex);
                }
                NotifyPropertyChanged("TutorTutoredHours");
                NotifyPropertyChanged("TutorPlannedHours");
                NotifyPropertyChanged("TutorLastSession");
                NotifyPropertyChanged("TutorNextSession");
                NotifyPropertyChanged("AvailableTutoringSessions");
                NotifyPropertyChanged("PlannedTutoringSessions");
            }
        }

        public void DeleteCourse(Course course)
        {
            try
            {
                dal.DeleteCourse(course);
                Courses.Remove(course);

                //Courses = new ObservableCollection<Course>(dal.GetAllCourses());
                NotifyPropertyChanged("Courses");

                Status = course.Name + " was removed!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void UpdateCourse(Course course)
        {
            try
            {
                dal.UpdateCourse(course);
                Courses = new ObservableCollection<Course>(dal.GetAllCourses());

                NotifyPropertyChanged("Courses");

                SelectedCourse = Courses.FirstOrDefault(c => c.Code.Equals(course.Code));
                NotifyPropertyChanged("SelectedCourse");
                Status = course.Name + " was updated!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }


        public void AddTutoringSession(String code, DateTime startTime, DateTime endTime)
        {
            try
            {
                TutoringSession ts = new TutoringSession(code, startTime, endTime, null);
                dal.AddTutoringSession(ts);
                TutoringSessions.Add(ts);

                TutoringSessions = new ObservableCollection<TutoringSession>(dal.GetAllTutoringSessions());
                NotifyPropertyChanged("TutoringSessions");

                Courses = new ObservableCollection<Course>(dal.GetAllCourses());
                NotifyPropertyChanged("Courses");

                Status = "Tutoring session was added!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void UpdateTutoringSession(TutoringSession ts)
        {
            if (ts != null)
            {
                try
                {
                    //Vi måste väl ta bort tutoingsession ur kursen och lägga till en ny väl?

                    Course tmpCourse = Courses.FirstOrDefault(c => c.Code.Equals(ts.Code));
                    TutoringSessionUpdateDTO tsDTO = new TutoringSessionUpdateDTO(selectedTutoringSession, ts);
                    dal.UpdateTutoringSession(tsDTO);
                    
                    Status = "Tutoring Session was updated";
                }
                catch (Exception ex)
                {
                    Status = ExceptionHandler.GetErrorMessage(ex);
                }

                TutoringSessions = new ObservableCollection<TutoringSession>(dal.GetAllTutoringSessions());
                NotifyPropertyChanged("TutoringSessions");

                Courses = new ObservableCollection<Course>(dal.GetAllCourses());
                NotifyPropertyChanged("Courses");

                SelectedTutoringSession = null;
                NotifyPropertyChanged("SelectedTutoringSession");

                SelectedCourse = Courses.FirstOrDefault(c => c.Code.Equals(ts.Code));
                NotifyPropertyChanged("SelectedCourse");
            }
            
        }

        public void DeleteTutoringSession(String code, DateTime startTime, DateTime endTime, int? participants)
        {
            TutoringSession tmpTs = new TutoringSession(code, startTime, endTime, participants);

            ICollection<TutoringSession> toBeDeleted = new List<TutoringSession>();
            foreach (TutoringSession ts in SelectedCourse.TutoringSessions)
            {
                if (ts.Equals(tmpTs))
                {
                    toBeDeleted.Add(ts);
                }
            }

            foreach (TutoringSession ts in toBeDeleted)
            {
                selectedCourse.TutoringSessions.Remove(ts);
            }

            try
            {
                dal.DeleteTutoringSession(tmpTs);

                TutoringSessions.Remove(tmpTs);
                //selectedCourse.TutoringSessions.Remove(SelectedTutoringSession);
                
                Status = "Tutoring session was removed!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                // Rollback
                foreach (TutoringSession ts in toBeDeleted)
                {
                    selectedCourse.TutoringSessions.Add(ts);
                }
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
            Courses = new ObservableCollection<Course>(dal.GetAllCourses());
            NotifyPropertyChanged("Courses");

            NotifyPropertyChanged("TutoringSessions");

            selectedCourse = Courses.FirstOrDefault(c => c.Code.Equals(code));
            NotifyPropertyChanged("SelectedCourse");
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }

        public bool SlideInEnabled
        {
            get
            {
                return slideInEnabled;
            }
            set
            {
                slideInEnabled = value;
                NotifyPropertyChanged("Status");
            }
        }

        public Decimal tutorTutoredHours;
        public Decimal TutorTutoredHours
        {
            get
            {
                if (selectedTutor != null)
                {
                    tutorTutoredHours = dal.GetTutoredHours(selectedTutor);
                    return tutorTutoredHours;
                }
                return 0;
            }
            set
            {
                tutorTutoredHours = value;
                NotifyPropertyChanged("TutorTutoredHours");
            }
        }

        public Decimal tutorPlannedHours;
        public Decimal TutorPlannedHours
        {
            get
            {
                if (selectedTutor != null)
                {
                    tutorPlannedHours = dal.GetPlannedHours(selectedTutor);
                    return tutorPlannedHours;
                }
                return 0;
            }
            set
            {
                tutorPlannedHours = value;
                NotifyPropertyChanged("TutorPlannedHours");
            }
        }

        public DateTime? tutorLastSession;
        public DateTime? TutorLastSession
        {
            get
            {
                if (selectedTutor != null && selectedTutor.TutoringSessions.Count != 0)
                {
                    ICollection<TutorTutoringSession> filteredSessions = selectedTutor.TutoringSessions
                                                                                       .Where(x => x.EndTime < DateTime.Now)
                                                                                       .OrderByDescending(x => x.EndTime).ToList();
                    if (filteredSessions.Count > 0)
                    {
                        return filteredSessions.FirstOrDefault().StartTime;
                    }
                }
                return null;
            }
            set
            {
                tutorLastSession = value;
                NotifyPropertyChanged("TutorLastSession");
            }
        }

        public DateTime? tutorNextSession;
        public DateTime? TutorNextSession
        {
            get
            {
                if (selectedTutor != null && selectedTutor.TutoringSessions.Count != 0)
                {
                    ICollection<TutorTutoringSession> filteredSessions = selectedTutor.TutoringSessions
                                                                                      .Where(x => x.StartTime > DateTime.Now)
                                                                                      .OrderBy(x => x.StartTime).ToList();
                    if (filteredSessions.Count > 0)
                    {
                        return filteredSessions.FirstOrDefault().StartTime;
                    }
                }
                return null;
            }
            set
            {
                tutorNextSession = value;
                NotifyPropertyChanged("TutorNextSession");
            }
        }

    }
}

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
                try
                {
                    TutorTutoringSession tmptts = new TutorTutoringSession(selectedTutor, ts);
                    TutoringSessionUpdateDTO updateDTO = new TutoringSessionUpdateDTO(ts, ts);
                    dal.UpdateTutoringSession(updateDTO);

                    ts.Tutors.Add(tmptts);
                    selectedTutor.TutoringSessions.Add(tmptts);
                    
                    NotifyPropertyChanged("TutorTutoredHours");
                    NotifyPropertyChanged("TutorPlannedHours");
                    NotifyPropertyChanged("AvailableTutoringSessions");
                    NotifyPropertyChanged("PlannedTutoringSessions");

                    Status = "Added to planned sessions";
                }
                catch (Exception ex)
                {
                    Status = ExceptionHandler.GetErrorMessage(ex);
                    Console.WriteLine(Status);
                }
            }
        }
        public void DeleteTutor(TutoringSession ts)
        {
            if (ts != null)
            {
                try
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

                    TutoringSessionUpdateDTO updateDTO = new TutoringSessionUpdateDTO(ts, ts);
                    dal.UpdateTutoringSession(updateDTO);

                    NotifyPropertyChanged("TutorTutoredHours");
                    NotifyPropertyChanged("TutorPlannedHours");
                    NotifyPropertyChanged("AvailableTutoringSessions");
                    NotifyPropertyChanged("PlannedTutoringSessions");

                    Status = "Removed from planned sessions";
                }
                catch (Exception ex)
                {
                    Status = ExceptionHandler.GetErrorMessage(ex);
                }
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

                Status = course.Name + "was removed!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }
        public void UpdateTutoringSession(TutoringSession ts)
        {
            try
            {
                TutoringSessionUpdateDTO tsDTO = new TutoringSessionUpdateDTO(selectedTutoringSession, ts);
                dal.UpdateTutoringSession(tsDTO);

                TutoringSessions.Remove(selectedTutoringSession);
                TutoringSessions.Add(ts);

                SelectedCourse.TutoringSessions.Remove(selectedTutoringSession);
                SelectedCourse.TutoringSessions.Add(ts);

                SelectedTutoringSession = null;
                NotifyPropertyChanged("TutoringSessions");
                NotifyPropertyChanged("SelectedTutoringSession");

                Status = "Tutoring Session was updated";


            }catch(Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);

            }
        }
        public void DeleteTutoringSession(String code, DateTime startTime, DateTime endTime, int? participants)
        {
            try
            {
                TutoringSession tmpTs = new TutoringSession(code, startTime, endTime, participants);

                dal.DeleteTutoringSession(tmpTs);
                TutoringSessions.Remove(TutoringSessions.FirstOrDefault(ts => ts.Code.Equals(code) && ts.StartTime.Equals(startTime) && ts.EndTime.Equals(endTime)));

                SelectedCourse.TutoringSessions.Remove(TutoringSessions.FirstOrDefault(ts => ts.Code.Equals(code) && ts.StartTime.Equals(startTime) && ts.EndTime.Equals(endTime)));

                //TutoringSessions = new ObservableCollection<TutoringSession>(dal.GetAllTutoringSessions());
                //Courses = new ObservableCollection<Course>(dal.GetAllCourses());
                NotifyPropertyChanged("Courses");
                NotifyPropertyChanged("TutoringSessions");
                NotifyPropertyChanged("SelectedCourse");

                Status = "Tutoring session was removed!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }


        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if(status != value)
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
        public Decimal TutorTutoredHours { 
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
                if (selectedTutor != null)
                {
                    return selectedTutor.TutoringSessions
                                        .Where(x => x.EndTime < DateTime.Now)
                                        .OrderByDescending(x => x.EndTime)
                                        .FirstOrDefault().StartTime;
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
                if (selectedTutor != null)
                {
                    return selectedTutor.TutoringSessions
                                        .Where(x => x.StartTime > DateTime.Now)
                                        .OrderBy(x => x.StartTime)
                                        .FirstOrDefault().StartTime;
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

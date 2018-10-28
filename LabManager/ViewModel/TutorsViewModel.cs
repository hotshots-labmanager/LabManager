using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LabManager.Database.DAL;
using LabManager.Model;
using LabManager.Utility;

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

            //courseDAL = new CourseDAL();
            //tutorDAL = new TutorDAL();
            //tutoringSessionDAL = new TutoringSessionDAL();
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

        private Course selectedTutoringSession;
        public Course SelectedTutoringSession
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
                //NotifyPropertyChanged("Courses");
                Status = code + " - " + name + " " + "was added to courses!";

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
                //NotifyPropertyChanged("Tutors");

                Status = firstName + " " + lastName + " " + "was added to tutors!";
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
                Tutors.Remove(Tutors.FirstOrDefault(p => p.Ssn == temp.Ssn));
                
                
                Tutors = new ObservableCollection<Tutor>(dal.GetAllTutors());
                NotifyPropertyChanged("Tutors");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }
        public void AddTutor(TutoringSession ts)
        {
            try
            {
                TutorTutoringSession tmptts = new TutorTutoringSession
                {
                    Code = ts.Code,
                    Ssn = selectedTutor.Ssn,
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime,
                    Tutor = SelectedTutor,
                    TutoringSession = ts,

                };
                TutoringSessionUpdateDTO updateDTO = new TutoringSessionUpdateDTO(ts, ts);
                dal.UpdateTutoringSession(updateDTO);

                ts.Tutors.Add(tmptts);
                    selectedTutor.TutoringSessions.Add(tmptts);
                

              
                

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
        public void DeleteTutor(TutoringSession ts)
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
                
                NotifyPropertyChanged("AvailableTutoringSessions");
                NotifyPropertyChanged("PlannedTutoringSessions");

                Status = "Removed from planned sessions";
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeleteCourse(Course course)
        {
            try
            {
               
             

                dal.DeleteCourse(course);
                Courses.Remove(Courses.FirstOrDefault(c => c.Code == course.Code));


                Courses = new ObservableCollection<Course>(dal.GetAllCourses());
                NotifyPropertyChanged("Courses");

                Status = course.Name + "was removed!";
                SelectedTutor = null;
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeleteTutoringSession(String code, DateTime startTime, DateTime endTIme, int? participants)
        {

            try
            {

                TutoringSession tmpTs = new TutoringSession(code, startTime, endTIme, participants);

                dal.DeleteTutoringSession(tmpTs);
                TutoringSessions.Remove(TutoringSessions.FirstOrDefault(ts => ts.Code == code && ts.StartTime == startTime && ts.EndTime == endTIme));

                SelectedCourse.TutoringSessions.Remove(TutoringSessions.FirstOrDefault(ts => ts.Code == code && ts.StartTime == startTime && ts.EndTime == endTIme));

                TutoringSessions = new ObservableCollection<TutoringSession>(dal.GetAllTutoringSessions());
                Courses = new ObservableCollection<Course>(dal.GetAllCourses());
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

    }
}

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
        
        private String status;
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
                if (selectedTutor != value && value != null)
                {
                    selectedTutor = value;

                    NotifyPropertyChanged("SelectedTutor");
                    NotifyPropertyChanged("AvailableTutoringSessions");
                    NotifyPropertyChanged("PlannedTutoringSessions");
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


        public void AddTutor(String ssn, String firstName, String lastName, String email, String password)
        {
            try
            {
                Tutor temp = new Tutor(ssn, firstName, lastName, email, password);
                dal.AddTutor(temp);
                Tutors.Add(temp);
                //NotifyPropertyChanged("Tutors");
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
                //Tutors = new ObservableCollection<Tutor>(dal.GetAllTutors());
                //NotifyPropertyChanged("Tutors");
                NotifyPropertyChanged("AvailableTutoringSessions");
                NotifyPropertyChanged("PlannedTutoringSessions");
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

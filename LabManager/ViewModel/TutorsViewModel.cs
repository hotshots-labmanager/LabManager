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
        private ObservableCollection<Tutor> tutors;

        private ObservableCollection<Course> courses;
        private ObservableCollection<TutoringSession> tutoringSessions;

        private ObservableCollection<TutoringSession> availableTutoringSessions;
        private ObservableCollection<TutoringSession> plannedTutoringSessions;


      

        private DAL dal;
        
        private String status;
        private bool slideInEnabled = true;


        public event PropertyChangedEventHandler PropertyChanged;

        public TutorsViewModel()
        {
            dal = new DAL();
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

                    // FULHACK FIXA!!!
                    IEnumerable<TutoringSession> abc = TutoringSessions;

                    IEnumerable<TutoringSession> pls = selectedTutor.TutoringSessions.Select(x => x.TutoringSession).ToList();
                    IEnumerable<TutoringSession> avs = TutoringSessions.Except(pls);

                    availableTutoringSessions = new ObservableCollection<TutoringSession>(avs);
                    plannedTutoringSessions = new ObservableCollection<TutoringSession>(pls);

                    NotifyPropertyChanged("SelectedTutor");
                    NotifyPropertyChanged("AvailableTutoringSessions");
                    NotifyPropertyChanged("PlannedTutoringSessions");
                } else if(value == null)
                {
                    selectedTutor = null;
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
                return availableTutoringSessions;
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
                return plannedTutoringSessions;
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
        public void DeleteTutorTutoringSession(TutoringSession ts)
        {
            try
            {
                TutoringSession ots = new TutoringSession(ts.Code,ts.StartTime,ts.EndTime,ts.NumberOfParticipants);
                ots.Tutors = ts.Tutors;

                //IEnumerable<TutorTutoringSession> tts = TutoringSessions.Select(t => t.t)
                //tmpTutor.TutoringSessions.Remove(TutoringSessions.FirstOrDefault(t2 => t2.Code == ts.Code && t2.StartTime == ts.StartTime && t2.EndTime == ts.EndTime));

                //Tutors.Remove(Tutors.FirstOrDefault(p => p.Ssn == temp.Ssn));

//1ST ATTEMPT
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




//2ND ATTEMPT
                //for (int i = ts.Tutors.Count-1 ;i>=0 ; i--)
                //{
                //    if (ts.Tutors.ElementAt(i).Tutor.Equals(selectedTutor))
                //    {

                //        ts.Tutors.Remove(ts.Tutors.ElementAt(i));
                //        selectedTutor.TutoringSessions.Remove(ts.Tutors.ElementAt(i));


                //    }
                //}

                // här kommer databasanrop... dal.UpdateTutoringSession(...)
                TutoringSessionUpdateDTO tmpTSU_DTO = new TutoringSessionUpdateDTO(ots, ts);
                dal.UpdateTutoringSession(tmpTSU_DTO);
                Tutors = new ObservableCollection<Tutor>(dal.GetAllTutors());
                NotifyPropertyChanged("Tutors");
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

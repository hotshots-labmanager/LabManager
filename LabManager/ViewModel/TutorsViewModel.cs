using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LabManager.Database.DAL;
using LabManager.Model;

namespace LabManager.ViewModel
{
    class TutorsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tutor> tutors;
        private ObservableCollection<Course> courses;
        private ObservableCollection<TutoringSession> tutoringSessions;
        private ObservableCollection<HaveTutored> haveTutoredSessions;

        //TODO
        //private ObservableCollection<Database.Model.PlanToTutor> planToTutorSessions;

        private DAL dal;
        //private CourseDAL courseDAL;
        //private TutorDAL tutorDAL;
        //private TutoringSessionDAL tutoringSessionDAL;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Tutor> Tutors
        {
            get
            {
                if (tutors == null)
                {
                    //MISSING METHOD:tutorDAL.GetAllTutors()
                    //tutors = new ObservableCollection<Tutor>(tutorDAL.GetAllTutors());
                    
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
                    //MISSING METHOD:tutorDAL.GetAllTutors()
                    //tutors = new ObservableCollection<Tutor>(tutorDAL.GetAllTutors());
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
                    //MISSING METHOD:tutorDAL.GetAllTutors()
                    //tutors = new ObservableCollection<Tutor>(tutorDAL.GetAllTutors());
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
        public ObservableCollection<HaveTutored> HaveTutoredSessions
        {
            get
            {
                if (haveTutoredSessions == null)
                {
                    //MISSING METHOD:tutorDAL.GetAllTutors()
                    //tutors = new ObservableCollection<Tutor>(tutorDAL.GetAllTutors());
                }
                return haveTutoredSessions;
            }
            set
            {
                if (haveTutoredSessions != value)
                {
                    haveTutoredSessions = value;
                    NotifyPropertyChanged("HaveTutoredSesions");
                }
            }
        }



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

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LabManager.Database;
using LabManager.Database.DAL;
using LabManager.Database.Model;

namespace LabManager.ViewModel
{
    class TutorsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Database.Model.Tutor> tutors;
        private ObservableCollection<Database.Model.Course> courses;
        private ObservableCollection<Database.Model.TutoringSession> tutoringSessions;
        private ObservableCollection<Database.Model.HaveTutored> haveTutoredSessions;

        //TODO
        //private ObservableCollection<Database.Model.PlanToTutor> planToTutorSessions;

        private CourseDAL courseDAL;
        private TutorDAL tutorDAL;
        private TutoringSessionDAL tutoringSessionDAL;

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

        public ObservableCollection<Course> Courses { get => courses; set => courses = value; }
        public ObservableCollection<TutoringSession> TutoringSessions { get => tutoringSessions; set => tutoringSessions = value; }
        public ObservableCollection<HaveTutored> HaveTutoredSessions { get => haveTutoredSessions; set => haveTutoredSessions = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TutorsViewModel()
        {
            courseDAL = new CourseDAL();
            tutorDAL = new TutorDAL();
            tutoringSessionDAL = new TutoringSessionDAL();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

using LabManager.Database.Model;
using LabManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabManager.View
{
    /// <summary>
    /// Interaction logic for PublicView.xaml
    /// </summary>
    public partial class PublicView : Window
    {

        static DateTime startDate = System.DateTime.Now;

        DateTime endDate = startDate.AddMonths(1);

        public PublicView()
        {
           
            InitializeComponent();

            //TESTDATA
            List<Tutor> tutors = new List<Tutor>();
            List<TutoringSession> tutoringSessions = new List<TutoringSession>();
            tutoringSessions.Add(new TutoringSession() { Code = "INFC20", Course = new Course() { Name = "Advanced Databases", Code = "INFC20", Credits = 7, NumberOfStudents = 40, TutoringSessions = tutoringSessions }, StartTime = System.DateTime.Now.AddDays(1), EndTime = System.DateTime.Now.AddDays(1).AddHours(2), NumberOfParticipants = 0, Tutors = tutors });


            tutors.Add(new Tutor() { FirstName = "Jacob", LastName = "Johansson", Email = "someaddress@mail.com", Ssn = "880101-2324", Password = null, TutoringSessions = tutoringSessions });
            tutors.Add(new Tutor() { FirstName = "Johan", LastName = "Johansson", Email = "someaddress@mail.com", Ssn = "890101-2324", Password = null, TutoringSessions = tutoringSessions });
            tutors.Add(new Tutor() { FirstName = "Jesper", LastName = "Johansson", Email = "someaddress@mail.com", Ssn = "900101-2324", Password = null, TutoringSessions = tutoringSessions });
            tutors.Add(new Tutor() { FirstName = "Jörgen", LastName = "Johansson", Email = "someaddress@mail.com", Ssn = "910101-2324", Password = null, TutoringSessions = null });
            dgGeneralTemplate.ItemsSource = tutors;
            //END TESTDATA
            TutorsViewModel tvm = new TutorsViewModel();
           



            if (startDate != null && endDate != null)
            {

                while (startDate < endDate)
                {
                    DataGridTextColumn newColumn = new DataGridTextColumn();
                    newColumn.Header = startDate.ToString("ddd dd/M", CultureInfo.InvariantCulture);

                    dgGeneralTemplate.Columns.Add(newColumn);

                    startDate = startDate.AddDays(1);
                }
            }
        }

        //private void dpStartEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    startDate = (DateTime)dpStartDate.SelectedDate;
        //    dpEndDate.SelectedDate = startDate.AddMonths(1);

        //    if (startDate!=null && endDate != null)
        //    {
        //        for (int i = 1; i < dgGeneralTemplate.Columns.Count; i++)
        //        {
        //            Console.WriteLine("Removing" + i);
        //            dgGeneralTemplate.Columns.RemoveAt(i);
                   
        //        }
               

        //        while (startDate < endDate)
        //        {
                    
        //            DataGridTextColumn newColumn = new DataGridTextColumn();
        //            newColumn.Header = startDate.ToString("ddd dd/M", CultureInfo.InvariantCulture);

                   
        //            dgGeneralTemplate.Columns.Add(newColumn);

                    
        //        }
        //    }
            
        //}
    }
}

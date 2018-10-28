using LabManager.Model;
using LabManager.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UCCourseDetails.xaml
    /// </summary>
    public partial class UCCourseDetails : UserControl
    {
        private TutorsViewModel tvm;
        bool editable = false;
        public UCCourseDetails(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
        }

        private void btnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Do you really want to remove " + lblName.Content + " ?";
            string sCaption = "Warning!";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    
                    tvm.DeleteCourse(tvm.SelectedCourse);
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;

            }
        }

        private void btnEditCourse_Click(object sender, RoutedEventArgs e)
        {

            ToggleEditable(true);

            
            
            
        }

        private void btnAbortChanges_Click(object sender, RoutedEventArgs e)
        {

            ToggleEditable(false);

            tbxCode.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            tbxCredits.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            tbxNumberOfStudents.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

            tvm.SelectedCourse = tvm.SelectedCourse;
        }

        private void btnConfirmChanges_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditable(false);
        }

        private void ToggleEditable(bool b)
        {
            tbxCode.IsEnabled = b;
            tbxCode.IsReadOnly = !b;

            tbxCredits.IsEnabled = b;
            tbxCredits.IsReadOnly = !b;

            tbxNumberOfStudents.IsEnabled = b;
            tbxNumberOfStudents.IsReadOnly = !b;

            if (b)
            {
                btnGrpConfirmation.Visibility = Visibility.Visible;

                btnEditCourse.Visibility = Visibility.Hidden;
                btnEditCourseDisabled.Visibility = Visibility.Visible;

                grdName.Visibility = Visibility.Visible;
                lblName.Visibility = Visibility.Hidden;
      
            }
            else
            {
                btnGrpConfirmation.Visibility = Visibility.Hidden;

                btnEditCourse.Visibility = Visibility.Visible;
                btnEditCourseDisabled.Visibility = Visibility.Hidden;

                grdName.Visibility = Visibility.Hidden;
                lblName.Visibility = Visibility.Visible;

            }

            this.editable = b;
        }

        private void btnDeleteTutoringSession_Click(object sender, RoutedEventArgs e)
        {
            TutoringSession ts = (TutoringSession)lvTutoringSessions.SelectedItem;



            if(ts!= null)
            {
                string sMessageBoxText = "Do you really want to remove " + tvm.SelectedTutoringSession.Code +"'s tutoring session on \n" + tvm.SelectedTutoringSession.StartTime + " -- " + tvm.SelectedTutoringSession.EndTime + " ?";
                string sCaption = "Warning!";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:

                        tvm.DeleteCourse(tvm.SelectedCourse);
                        break;

                    case MessageBoxResult.No:
                        /* ... */
                        break;

                }

                var code = ts.Code;
                var startTime = ts.StartTime;
                var endTime = ts.EndTime;
                var participants = ts.NumberOfParticipants;

                tvm.DeleteTutoringSession(code, startTime, endTime, participants);

                lvTutoringSessions.GetBindingExpression(ListView.ItemsSourceProperty).UpdateTarget();

            } else
            {
                tvm.Status = "You must select a row";
            }
            
        }
    }
}

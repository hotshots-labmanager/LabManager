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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LabManager.Model;
using LabManager.ViewModel;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCTutorDetails : UserControl
    {

        TutorsViewModel tvm;
        private bool editable = false;

        public UCTutorDetails(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();

        }

        private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditable(true);

        }

        private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Do you really want to remove " + lblFullName.Content + " ?";
            string sCaption = "Warning!";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    tvm.DeleteTutor(tbxSsn.Text);

                    Storyboard sb = this.FindResource("SlideOut") as Storyboard;
                    Storyboard.SetTarget(sb, this);
                    sb.Begin();

                    tvm.Status = "Creation of new course was aborted.";
                    tvm.SlideInEnabled = true;
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;

            }





            String ssn = tbxSsn.Text;
            // tvm.DeleteTutor(ssn);
        }

        private void btnAbortTutor_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditable(false);

            tbxSsn.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            tbxEmail.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

        }

        private void btnConfirmTutor_Click(object sender, RoutedEventArgs e)
        {

            //RUN UPDATE METHOD FROM TUTORSVIEWMODEL

            ToggleEditable(false);

        }

        private void ToggleEditable(bool b)
        {
            tbxSsn.IsEnabled = b;
            tbxSsn.IsReadOnly = !b;

            tbxEmail.IsEnabled = b;
            tbxEmail.IsReadOnly = !b;


            if (b)
            {
                btnGrpConfirmation.Visibility = Visibility.Visible;

                btnEditTutor.Visibility = Visibility.Hidden;
                btnEditTutorDisabled.Visibility = Visibility.Visible;
            }
            else
            {
                btnGrpConfirmation.Visibility = Visibility.Hidden;

                btnEditTutor.Visibility = Visibility.Visible;
                btnEditTutorDisabled.Visibility = Visibility.Hidden;
            }

            this.editable = b;
        }
    }
}


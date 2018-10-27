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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCNewTutorDetails : UserControl
    {
        TutorsViewModel tvm;
        public UCNewTutorDetails(TutorsViewModel tvm)
        {
            
            InitializeComponent();
            this.tvm = tvm;
        }

        private void btnConfirmTutor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxPassword.Password.Equals(tbxRePassword.Password))
                {
                    tvm.AddTutor(tbxSsn.Text, tbxFirstName.Text, tbxLastName.Text, tbxEmail.Text, tbxPassword.Password);
                    tvm.Status = tbxFirstName.Text + " " + tbxLastName.Text + " " + "was added to tutors!";
                    ((Panel)this.Parent).Children.Remove(this);

                }
                else {
                    tvm.Status = "Passwords does not match";
                        }
               
            }
            catch
            {

            }

        }
        private void btnAbortTutor_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("SlideOut") as Storyboard;
            Storyboard.SetTarget(sb, this);
            sb.Begin();

            tvm.Status = "Creation of new tutor was aborted.";
            tvm.SlideInEnabled = true;
        }
    }
}

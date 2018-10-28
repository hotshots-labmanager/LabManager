using LabManager.Utility;
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
        private TutorsViewModel tvm;

        public UCNewTutorDetails(TutorsViewModel tvm)
        {
            InitializeComponent();
            this.tvm = tvm;
        }

        private void btnConfirmTutor_Click(object sender, RoutedEventArgs e)
        {
            // General input handling
            Dictionary<String, String> inputValues = new Dictionary<string, string>();
            inputValues.Add("Social security number", tbxSsn.Text);
            inputValues.Add("First name", tbxFirstName.Text);
            inputValues.Add("Last Name", tbxLastName.Text);
            inputValues.Add("Email", tbxEmail.Text);
            inputValues.Add("Password", tbxRePassword.Password);

            String message;
            if (!InputHandler.IsFieldsFilledOut(out message, inputValues))
            {
                tvm.Status = message;
            }
            else if (!tbxPassword.Password.Equals(tbxRePassword.Password))
            {
                tvm.Status = "Passwords does not match!";
            }
            else
            {
                String hashedPassword = PasswordUtility.HashPassword(tbxPassword.Password);
                tvm.AddTutor(tbxSsn.Text, tbxFirstName.Text, tbxLastName.Text, tbxEmail.Text, hashedPassword);
                ((Panel)this.Parent).Children.Remove(this);
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

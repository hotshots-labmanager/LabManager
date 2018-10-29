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
    /// Interaction logic for UCNewCourseDetails.xaml
    /// </summary>
    /// 
  
    public partial class UCNewCourseDetails : UserControl
    {
        private TutorsViewModel tvm;

        public UCNewCourseDetails(TutorsViewModel tvm)
        {
            InitializeComponent();
            this.tvm = tvm;
        }

        private void btnConfirmChanges_Click(object sender, RoutedEventArgs e)
        {
            // General input handling
            Dictionary<String, String> inputValues = new Dictionary<string, string>();
            inputValues.Add("Code", tbxCode.Text);
            inputValues.Add("Credits", tbxCredits.Text);
            inputValues.Add("Name", tbxName.Text);
            inputValues.Add("Number of students", tbxNumberOfStudents.Text);

            String message;
            if (!InputHandler.IsFieldsFilledOut(out message, inputValues))
            {
                tvm.Status = message;
            }
            else if (!int.TryParse(tbxNumberOfStudents.Text, out int intOfStudents))
            {
                tvm.Status = "Number of students must be written as a whole number.";
            }
            else if (!decimal.TryParse(tbxCredits.Text, out decimal decimalOfCredits))
            {
                tvm.Status = "Credits must be written as a decimal number (example 7,5 or 8)";
            }
            else
            {
                tvm.AddCourse(tbxCode.Text, decimalOfCredits, tbxName.Text, intOfStudents);
                ((Panel)this.Parent).Children.Remove(this);
                tvm.SlideInEnabled = true;
            }
        }

        private void btnAbortChanges_Click(object sender, RoutedEventArgs e)
        {

            Storyboard sb = this.FindResource("SlideOut") as Storyboard;
            Storyboard.SetTarget(sb, this);
            sb.Begin();

            tvm.Status = "Creation of new course was aborted.";
            tvm.SlideInEnabled = true;
        }
    }
}

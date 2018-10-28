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
        TutorsViewModel tvm;
        public UCNewCourseDetails(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
        }

        private void btnConfirmChanges_Click(object sender, RoutedEventArgs e)
        {
            String numberOfStudents = tbxNumberOfStudents.Text;
            int intOfStudents;
            decimal decimalOfCredits;

            if (int.TryParse(numberOfStudents, out intOfStudents))
            {

                if (decimal.TryParse(tbxCredits.Text, out decimalOfCredits))
                {
                    tvm.AddCourse(tbxCode.Text, decimalOfCredits, tbxName.Text, intOfStudents);
                } else
                {
                    tvm.Status = "Credits must be written as a decimal number (Example 7,5 or 8)";
                }



            }
            else
            {
                tvm.Status = "Number of students must be written as an integer";
            }



            ((Panel)this.Parent).Children.Remove(this);
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

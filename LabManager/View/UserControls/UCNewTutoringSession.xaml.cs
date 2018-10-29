using LabManager.Model;
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
    /// Interaction logic for UCNewTutorSession.xaml
    /// </summary>
    
    public partial class UCNewTutoringSession : UserControl
    {

        TutorsViewModel tvm;
        public UCNewTutoringSession(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DateTime startTime = dtpStartTime.Value ?? default(DateTime);
            DateTime endTime = dtpEndTime.Value ?? default(DateTime);

            // General input handling
            Dictionary<String, String> inputValues = new Dictionary<string, string>();
            inputValues.Add("Start date", startTime.ToString());
            inputValues.Add("End date", endTime.ToString());

            String message;
            if (!InputHandler.IsFieldsFilledOut(out message, inputValues))
            {
                tvm.Status = message;
            }
            else if (lvCourses.SelectedItem == null)
            {
                tvm.Status = "Please choose a course from the list.";
            }
            else
            {
                Course c = lvCourses.SelectedItem as Course;
                tvm.AddTutoringSession(c.Code, startTime, endTime);
                ((Panel)this.Parent).Children.Remove(this);
            }
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("SlideOut") as Storyboard;
            Storyboard.SetTarget(sb, this);
            sb.Begin();

            tvm.Status = "Creation of new tutoring session was aborted.";
            tvm.SlideInEnabled = true;
        }
    }
}

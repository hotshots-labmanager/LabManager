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
    /// Interaction logic for UCCourses.xaml
    /// </summary>
    public partial class UCCourses : UserControl
    {
        TutorsViewModel tvm;
        UCCourseDetails ucCourseDetails;

        public UCCourses(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            ucCourseDetails = new UCCourseDetails(tvm);

            InitializeComponent();
        }

        private void dgCourses_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (tvm.SlideInEnabled)
            {
                splDetails.Children.Clear();
                splDetails.Children.Add(ucCourseDetails);
                Storyboard sb = this.FindResource("SlideIn") as Storyboard;
                Storyboard.SetTarget(sb, this.ucCourseDetails);
                sb.Begin();

                tvm.SlideInEnabled = false;
            }
        }
    }
}

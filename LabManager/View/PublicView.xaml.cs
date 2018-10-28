using LabManager.Model;
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

using LabManager.View.UserControls;

namespace LabManager.View
{
    /// <summary>
    /// Interaction logic for PublicView.xaml
    /// </summary>
    public partial class PublicView : Window
    {

        private TutorsViewModel tvm;

        private UCCourses ucCourses;
        private UCTutors ucTutors;
        private UCSchedule ucSchedule;
    

        public PublicView()
        {
            tvm = new TutorsViewModel();

            ucCourses = new UCCourses(tvm);
            ucTutors = new UCTutors(tvm);
            ucSchedule = new UCSchedule(tvm);

            

            DataContext = tvm;

            InitializeComponent();

            //DataGrid details = (DataGrid)dgGeneralTemplate.RowDetailsTemplate.Resources.FindName("dgDetailsTemplate");
            //Console.WriteLine(details.Name);
        }

        private void brdCourses_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(ucCourses);
            tvm.SlideInEnabled = true;
        }

        private void BrdrTutors_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(ucTutors);
            tvm.SlideInEnabled = true;

        }
        private void BrdrSchedule_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(ucSchedule);
            tvm.SlideInEnabled = true;


        }


    }
}

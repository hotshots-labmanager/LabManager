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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UCTutors.xaml
    /// </summary>
    public partial class UCTutors : UserControl
    {
        TutorsViewModel tvm;
        UCNewTutorDetails ucNewTutorDetails;
        UCTutorDetails ucTutorDetails;

    

        public UCTutors(TutorsViewModel tvm)
        {
            this.tvm = tvm;

            ucTutorDetails = new UCTutorDetails(tvm);
            
          
            InitializeComponent();
            
        }



        private void btnNewTutor_Click(object sender, RoutedEventArgs e)
        {
            ucNewTutorDetails = new UCNewTutorDetails(tvm);
            splDetails.Children.Clear();
            splDetails.Children.Add(ucNewTutorDetails);

            if (tvm.SlideInEnabled)
            {
                
                Storyboard sb = this.FindResource("SlideIn") as Storyboard;
                Storyboard.SetTarget(sb, this.ucNewTutorDetails);
                sb.Begin();

                tvm.SelectedTutor = null;
                tvm.SlideInEnabled = false;

            }
            
        }

        private void dgTutors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splDetails.Children.Clear();
            splDetails.Children.Add(ucTutorDetails);

            if (tvm.SlideInEnabled)
            {
                Storyboard sb = this.FindResource("SlideIn") as Storyboard;
                Storyboard.SetTarget(sb, this.ucTutorDetails);
                sb.Begin();

                tvm.SlideInEnabled = false;
            }

        
            
        }

        

    }
}

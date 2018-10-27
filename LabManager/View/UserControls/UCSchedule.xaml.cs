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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCSchedule : UserControl
    {

        private TutorsViewModel tvm;

        public UCSchedule(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
            
        }

        


        //private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    splDetails.Children.Clear();

        //}
        
        //private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        //{


        //    btnGrpConfirmation.Visibility = Visibility.Visible;
            
        //    btnEditTutor.Visibility = Visibility.Hidden;
        //    btnEditTutorDisabled.Visibility = Visibility.Visible;

        //}

        private void BtnAddToPlannedSessions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TutoringSession tempSession = (TutoringSession)dgAvailableSessions.SelectedItem;
                
            }
            catch
            {

            }

            tvm.Status = "Added to planned sessions";

        }
        private void BtnRemoveFromPlannedSessions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tvm.DeleteTutor((TutoringSession)dgPlannedSessions.SelectedItem);
                tvm.Status = "Removed from planned sessions";
            }
            catch (Exception ex)
            {
                tvm.Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }
    }
}


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
    /// Interaction logic for UCTutors.xaml
    /// </summary>
    public partial class UCTutors : UserControl
    {
        TutorsViewModel tvm;

        public UCTutors(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
        }


        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            splDetails.Children.Clear();

        }

        private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        {


            btnGrpConfirmation.Visibility = Visibility.Visible;

            btnEditTutor.Visibility = Visibility.Hidden;
            btnEditTutorDisabled.Visibility = Visibility.Visible;

        }

        private void BtnAddToPlannedSessions_Click(object sender, RoutedEventArgs e)
        {


            lblStatusText.Content = "Added to planned sessions";

        }
        private void BtnRemoveFromPlannedSessions_Click(object sender, RoutedEventArgs e)
        {


            lblStatusText.Content = "Removed from planned sessions";

        }


        private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        {

            // tvm.DeleteTutor(ssn);
        }

        private void btnAbortTutor_Click(object sender, RoutedEventArgs e)
        {
            //INSTEAD OF USING TWO HANDLERS
            //Button tempBtn = sender as Button;
            //if (tempBtn.Name.Equals("btnConfirmTutor"){

            //}

            btnGrpConfirmation.Visibility = Visibility.Hidden;
            btnEditTutorDisabled.Visibility = Visibility.Hidden;
            btnEditTutor.Visibility = Visibility.Visible;




        }

        private void btnConfirmTutor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //RUN UPDATE METHOD FROM TUTORSVIEWMODEL

                btnGrpConfirmation.Visibility = Visibility.Hidden;
                btnEditTutorDisabled.Visibility = Visibility.Hidden;
                btnEditTutor.Visibility = Visibility.Visible;


            }
            catch
            {

            }
        }

        private void cbTutorSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void dgPlannedSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

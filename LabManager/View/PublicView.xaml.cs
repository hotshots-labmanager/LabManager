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

        UCTutorDetails uCTutorDetails;

        static DateTime startDate = System.DateTime.Now;

        DateTime endDate = startDate.AddMonths(1);

        TutorsViewModel tvm = new TutorsViewModel();

        public PublicView()
        {
            

            InitializeComponent();
            uCTutorDetails = new UCTutorDetails();


            DataContext = tvm;
            






            //DataGrid details = (DataGrid)dgGeneralTemplate.RowDetailsTemplate.Resources.FindName("dgDetailsTemplate");
            //Console.WriteLine(details.Name);
        }

        //FLYTTADE TILL UserControl
        //private bool isEditable = false;
        //private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!isEditable) { 
        //        lblSsn.IsEnabled = true;
        //        lblSsn.IsReadOnly = false;

        //        tbxEmail.IsEnabled = true;
        //        tbxEmail.IsReadOnly = false;
        //    } else
        //    {
        //        lblSsn.IsEnabled = false;
        //        lblSsn.IsReadOnly = true;

        //        tbxEmail.IsEnabled = false;
        //        tbxEmail.IsReadOnly = true;
        //    }
        //    isEditable = !isEditable;

           
            

        //}

        //private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    String ssn = lblSsn.Text;
        //    tvm.DeleteTutor(ssn);
        //}


        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            splDetails.Children.Clear();

        }

        //private void MasterDataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    splDetails.Children.Clear();
            
        //    //NOT NECECARRY, INHERITED BY PARENT
        //    //uCTutorDetails.DataContext = tvm.Tutors;
        //    //uCTutorDetails.selectedTutor = (Tutor)dgGeneralTemplate.SelectedItem;

        //    //Binding b = new Binding();
        //    //b.Source = dgGeneralTemplate.SelectedItem;
        //    //b.Path = new PropertyPath("Email");
            
        //    //uCTutorDetails.tbxEmail.SetBinding(TextBox.TextProperty, b);

        //    //BindingExpression bindingExpression = dgcolTutor.GetBindingExpression(DataGridTextColumn.DataContextProperty);
        //    //Binding parentBinding = bindingExpression.ParentBinding;
        //    //uCTutorDetails.tbxEmail.SetBinding(TextBox.TextProperty, parentBinding);
        //  //  tvm.SelectedItem = dgGeneralTemplate.SelectedItem;
        //    splDetails.DataContext = dgGeneralTemplate.SelectedItem;
        //    splDetails.Children.Add(uCTutorDetails);
        //}


            //3RD ATTEMPT
   






        //private void dpStartEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    startDate = (DateTime)dpStartDate.SelectedDate;
        //    dpEndDate.SelectedDate = startDate.AddMonths(1);

        //    if (startDate!=null && endDate != null)
        //    {
        //        for (int i = 1; i < dgGeneralTemplate.Columns.Count; i++)
        //        {
        //            Console.WriteLine("Removing" + i);
        //            dgGeneralTemplate.Columns.RemoveAt(i);

        //        }


        //        while (startDate < endDate)
        //        {

        //            DataGridTextColumn newColumn = new DataGridTextColumn();
        //            newColumn.Header = startDate.ToString("ddd dd/M", CultureInfo.InvariantCulture);


        //            dgGeneralTemplate.Columns.Add(newColumn);


        //        }
        //    }

        //}

        private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        {
            

            btnGrpConfirmation.Visibility = Visibility.Visible;
            //imgConfigButton.Source = new BitmapImage(new Uri("../img/Font-Awsome/cog-wheel-silhouette-gray.png", UriKind.Relative));
            //btnEditTutor.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)BtnEditTutor_Click);
            //btnEditTutor.Style = Resources["disabledImageButtonStyle"] as Style;
            btnEditTutor.Visibility = Visibility.Hidden;
            btnEditTutorDisabled.Visibility = Visibility.Visible;

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
            ComboBox cb = sender as ComboBox;
            tvm.SelectedItem = (Tutor)cb.SelectedItem;
        }
    }
}

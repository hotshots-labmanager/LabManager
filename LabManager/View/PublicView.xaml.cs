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


            dgGeneralTemplate.ItemsSource = tvm.Tutors;
            DataContext = tvm;
           
            
           



            if (startDate != null && endDate != null)
            {


                while (startDate < endDate)
                {
                    DataGridTextColumn newColumn = new DataGridTextColumn();
                    newColumn.Header = startDate.ToString("ddd dd/M", CultureInfo.InvariantCulture);
                    dgGeneralTemplate.Columns.Add(newColumn);
                    

                    startDate = startDate.AddDays(1);
                }
            }
        }
        //FLYTTADE TILL UserControl
        //private bool isEditable = false;
        //private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!isEditable) { 
        //        tbxSsn.IsEnabled = true;
        //        tbxSsn.IsReadOnly = false;

        //        tbxEmail.IsEnabled = true;
        //        tbxEmail.IsReadOnly = false;
        //    } else
        //    {
        //        tbxSsn.IsEnabled = false;
        //        tbxSsn.IsReadOnly = true;

        //        tbxEmail.IsEnabled = false;
        //        tbxEmail.IsReadOnly = true;
        //    }
        //    isEditable = !isEditable;

           
            

        //}

        //private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        //{
        //    String ssn = tbxSsn.Text;
        //    tvm.DeleteTutor(ssn);
        //}


        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           // lblHeader.SetBinding(TextBlock.TextProperty, "ssn");
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

            //2ND ATTEMPT
        //private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    object tempItem = dgGeneralTemplate.SelectedItem;

        //    if (tempItem is Tutor)
        //    {
        //        Console.WriteLine(tempItem.GetType());

        //        splDetails.Children.Clear();
        //        splDetails.DataContext = tempItem;
        //        splDetails.Children.Add(uCTutorDetails);

        //        Console.WriteLine(dgGeneralTemplate.RowDetailsTemplate.DataTemplateKey);
        //    }
        //    else
        //    {
        //        splDetails.Children.Clear();
                
        //    }
          
        //}

            //3RD ATTEMPT
        private void dgGeneralTemplate_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            object tempItem = dgGeneralTemplate.SelectedItem;

            if (tempItem is Tutor)
            {
                Console.WriteLine(tempItem.GetType());

                splDetails.Children.Clear();
                splDetails.DataContext = tempItem;
                splDetails.Children.Add(uCTutorDetails);

                Console.WriteLine(dgGeneralTemplate.RowDetailsTemplate.DataTemplateKey);
            }
            else
            {
                splDetails.Children.Clear();

            }

        }
    





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
    }
}

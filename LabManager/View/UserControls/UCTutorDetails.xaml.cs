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
using LabManager.Model;
using LabManager.ViewModel;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCTutorDetails : UserControl
    {

        TutorsViewModel tvm;
        //public Tutor MyProperty
        //{
        //    get { return (Tutor)GetValue(MyProperty); }
        //    set { SetValue(MyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyProperty =
        //    DependencyProperty.Register("MyProperty", typeof(object), typeof(UCTutorDetails), new PropertyMetadata(0));


        //public Tutor selectedTutor
        //{
        //    get { return (Tutor)GetValue(selectedTutorProperty); }
        //    set { SetValue(selectedTutorProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for selectedTutor.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty selectedTutorProperty =
        //    DependencyProperty.Register("selectedTutor", typeof(Tutor), typeof(UCTutorDetails), new PropertyMetadata(0));



        public UCTutorDetails(TutorsViewModel tvm)
        {
            this.tvm = tvm;
            InitializeComponent();
            
        }
       
        private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        {
                tbxSsn.IsEnabled = true;
                tbxSsn.IsReadOnly = false;

                tbxEmail.IsEnabled = true;
                tbxEmail.IsReadOnly = false;

                btnGrpConfirmation.Visibility = Visibility.Visible;
                //imgConfigButton.Source = new BitmapImage(new Uri("../img/Font-Awsome/cog-wheel-silhouette-gray.png", UriKind.Relative));
                //btnEditTutor.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)BtnEditTutor_Click);
                //btnEditTutor.Style = Resources["disabledImageButtonStyle"] as Style;
                btnEditTutor.Visibility = Visibility.Hidden;
                btnEditTutorDisabled.Visibility = Visibility.Visible;

        }

        private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Do you really want to remove " + lblFullName.Content + " ?";
            string sCaption = "Warning!";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    tvm.DeleteTutor(tbxSsn.Text);
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;

            }





            String ssn = tbxSsn.Text;
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

            tbxSsn.IsEnabled = false;
            tbxSsn.IsReadOnly = true;

            tbxEmail.IsEnabled = false;
            tbxEmail.IsReadOnly = true;


        }

        private void btnConfirmTutor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //RUN UPDATE METHOD FROM TUTORSVIEWMODEL

                btnGrpConfirmation.Visibility = Visibility.Hidden;
                btnEditTutorDisabled.Visibility = Visibility.Hidden;
                btnEditTutor.Visibility = Visibility.Visible;

                tbxSsn.IsEnabled = false;
                tbxSsn.IsReadOnly = true;

                tbxEmail.IsEnabled = false;
                tbxEmail.IsReadOnly = true;
            }
            catch
            {

            }
        }
    }
}

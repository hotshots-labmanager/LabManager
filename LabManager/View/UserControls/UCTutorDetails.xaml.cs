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

using LabManager.ViewModel;

namespace LabManager.View.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCTutorDetails : UserControl
    {
        
        public UCTutorDetails()
        {
            InitializeComponent();
          
        }
        private bool isEditable = false;
        private void BtnEditTutor_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditable)
            {
                tbxSsn.IsEnabled = true;
                tbxSsn.IsReadOnly = false;

                tbxEmail.IsEnabled = true;
                tbxEmail.IsReadOnly = false;
            }
            else
            {
                tbxSsn.IsEnabled = false;
                tbxSsn.IsReadOnly = true;

                tbxEmail.IsEnabled = false;
                tbxEmail.IsReadOnly = true;
            }
            isEditable = !isEditable;




        }

        private void BtnDeleteTutor_Click(object sender, RoutedEventArgs e)
        {
            String ssn = tbxSsn.Text;
           // tvm.DeleteTutor(ssn);
        }
    }
}

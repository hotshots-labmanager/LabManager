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

        private UCTutors uCTutors;
        private UCSchedule uCSchedule;
    

        public PublicView()
        {
            tvm = new TutorsViewModel();
            uCSchedule = new UCSchedule(tvm);
            uCTutors = new UCTutors(tvm);

            DataContext = tvm;

            InitializeComponent();

            //DataGrid details = (DataGrid)dgGeneralTemplate.RowDetailsTemplate.Resources.FindName("dgDetailsTemplate");
            //Console.WriteLine(details.Name);
        }


        
        private void BrdrTutors_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uCTutors);
        }
        private void BrdrSchedule_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Children.Clear();
            
            mainGrid.Children.Add(uCSchedule);
            
        }
    }
}

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
using System.Windows.Shapes;

namespace LabManager.View
{
    /// <summary>
    /// Interaction logic for PublicView.xaml
    /// </summary>
    public partial class PublicView : Window
    {
        public PublicView()
        {
            InitializeComponent();

            DateTime startDate = (DateTime)dpStartDate.SelectedDate;
            DateTime endDate = (DateTime)dpEndDate.SelectedDate;

            if (startDate != null && endDate != null)
            {
                while (startDate < endDate)
                {
                    DataGridTextColumn newColumn = new DataGridTextColumn();
                    newColumn.Header = startDate.ToShortDateString();

                    dgGeneralTemplate.Columns.Add(newColumn);

                    startDate = startDate.AddDays(1);
                }
            }
        }

    }
}

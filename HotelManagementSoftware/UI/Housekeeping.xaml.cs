using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

namespace HotelManagementSoftware.UI
{
    /// <summary>
    /// Interaction logic for Housekeeping.xaml
    /// </summary>
    public partial class Housekeeping : UserControl
    {
        public Housekeeping()
        {
            InitializeComponent();
        }

        private void AddHousekeepingRequest_Click(object sender, RoutedEventArgs e)
        {
            HousekeepingEditWindow window = new HousekeepingEditWindow();
            window.Show();
            window.Closed += Window_Closed;
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            ((HousekeepingVM)DataContext).GetAllItem();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int id = (int)button.Tag;

            HousekeepingEditWindow window = new HousekeepingEditWindow(id);
            window.Show();
            window.Closed += Window_Closed;
        }
    }
}

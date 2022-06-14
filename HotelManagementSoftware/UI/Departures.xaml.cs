using System;
using System.Collections.Generic;
using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSoftware.UI
{
    /// <summary>
    /// Interaction logic for Departures.xaml
    /// </summary>
    public partial class Departures : UserControl
    {
        public Departures()
        {
            InitializeComponent();
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            CheckoutWindow window = new CheckoutWindow();
            window.Show();
            window.Closed += Window_Closed;
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            ((DeparturesVM)DataContext).LoadDepartures();
        }
    }
}

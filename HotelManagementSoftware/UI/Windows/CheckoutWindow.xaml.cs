using HotelManagementSoftware.ViewModels.WindowVMs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace HotelManagementSoftware.UI
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        public CheckoutWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<CheckoutWindowVM>();

        }

        public CheckoutWindow(int reservationId)
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<CheckoutWindowVM>();
            ((CheckoutWindowVM)DataContext).LoadReservationFromId(reservationId);
        }

        
    }
}

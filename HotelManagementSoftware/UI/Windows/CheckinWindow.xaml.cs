using HotelManagementSoftware.ViewModels.WindowVMs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
namespace HotelManagementSoftware.UI
{
    /// <summary>
    /// Interaction logic for CheckinWindow.xaml
    /// </summary>
    public partial class CheckinWindow : Window
    {
        public CheckinWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<CheckinWindowVM>();

        }

        


    }
}

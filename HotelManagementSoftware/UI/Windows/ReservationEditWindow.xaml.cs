using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels.Utils;
using HotelManagementSoftware.ViewModels.WindowVMs;
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
using System.Windows.Shapes;

namespace HotelManagementSoftware.UI
{
    /// <summary>
    /// Interaction logic for CreateReservationWindow.xaml
    /// </summary>
    public partial class ReservationEditWindow : Window
    {
        public ReservationEditWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<ReservationEditWindowVM>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ChooseCustomer_Click(object sender, RoutedEventArgs e)
        {
            var chooseCustomer = new ChooseCustomerWindow();
            chooseCustomer.DialogFinished += ChooseCustomerWindow_DialogFinished;
            chooseCustomer.ShowDialog();

        }
        void ChooseCustomerWindow_DialogFinished(object sender, WindowEventArgs e)
        {
            ((ReservationEditWindowVM)DataContext).LoadCustomerFromId(e.Id);
        }
 
    }
}

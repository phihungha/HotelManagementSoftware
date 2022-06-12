using HotelManagementSoftware.ViewModels.WindowVMs;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace HotelManagementSoftware.UI.Windows
{
    /// <summary>
    /// Interaction logic for CustomerEditWindow.xaml
    /// </summary>
    public partial class CustomerEditWindow : Window
    {
        private CustomerEditWindowVM viewModel;

        public CustomerEditWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<CustomerEditWindowVM>();
            viewModel = ((CustomerEditWindowVM)DataContext);
        }

        public CustomerEditWindow(int customerId)
            : this()
        {
            viewModel.LoadCustomerFromId(customerId);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (await viewModel.SaveCustomer())
                Close();
        }
    }
}

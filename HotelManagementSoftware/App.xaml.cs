using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.Utils;
using HotelManagementSoftware.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace HotelManagementSoftware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
            testDb();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<EmployeeBusiness, EmployeeBusiness>();
            services.AddSingleton<MainWindowVM, MainWindowVM>();

            return services.BuildServiceProvider();
        }

        private async void testDb()
        {
            EmployeeBusiness eb = new EmployeeBusiness();
            Employee e = (await eb.GetAllEmloyees())[0];
        }
    }
}

using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            EmployeeTypeBusiness b = new EmployeeTypeBusiness();
            List<EmployeeType> types = await b.GetAllEmployeeTypes();
            EmployeeType t = types.First(i => i.Name == "Receptionist");

            Employee newEmployee = new Employee("john",
                                                "skeet",
                                                "johnskeet",
                                                Gender.Male,
                                                new DateTime(1975, 5, 10),
                                                "0123456789",
                                                "130");
            newEmployee.EmployeeType = t;

            EmployeeBusiness eb = new EmployeeBusiness();
            eb.CreateEmployee(newEmployee, "4321");
        }
    }
}

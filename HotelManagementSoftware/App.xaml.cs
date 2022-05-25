using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.ViewModels;
using HotelManagementSoftware.ViewModels.WindowVMs;
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
            InitiateDatabase();
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

            // Business services
            services.AddSingleton<EmployeeBusiness>();
            services.AddSingleton<EmployeeTypeBusiness>();
            services.AddSingleton<CustomerBusiness>();
            services.AddSingleton<ReservationBusiness>();
            services.AddSingleton<HousekeepingBusiness>();
            services.AddSingleton<MaintenanceBusiness>();
            services.AddSingleton<RoomBusiness>();
            services.AddSingleton<RoomTypeBusiness>();

            // View models
            services.AddTransient<MainWindowVM>();
            services.AddTransient<LoginVM>();
            services.AddTransient<LoggedInVM>();
            services.AddTransient<DashboardVM>();
            services.AddTransient<ReservationsVM>();
            services.AddTransient<ArrivalsVM>();
            services.AddTransient<DeparturesVM>();
            services.AddTransient<HousekeepingVM>();
            services.AddTransient<MaintenanceVM>();
            services.AddTransient<CustomersVM>();
            services.AddTransient<RoomsVM>();
            services.AddTransient<RoomTypesVM>();
            services.AddTransient<EmployeesVM>();

            services.AddTransient<CancelReservationWindowVM>();
            services.AddTransient<CheckinWindowVM>();
            services.AddTransient<CheckoutWindowVM>();
            services.AddTransient<ChooseRoomTypeWindowVM>();
            services.AddTransient<ChooseRoomWindowVM>();
            services.AddTransient<CustomerEditWindowVM>();
            services.AddTransient<EmployeeEditWindowVM>();
            services.AddTransient<HousekeepingEditWindowVM>();
            services.AddTransient<MaintenanceEditWindowVM>();
            services.AddTransient<ReservationEditWindowVM>();
            services.AddTransient<RoomEditWindowVM>();
            services.AddTransient<RoomTypeEditWindowVM>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Create the database if it doesn't exist yet;
        /// </summary>
        private void InitiateDatabase()
        {
            using (var db = new Database())
                db.Database.EnsureCreated();
        }


        private async void testDb()
        {

        }
    }
}

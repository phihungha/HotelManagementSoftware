using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.Tests;
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
            GenerateTestData();
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
            services.AddSingleton<CustomerBusiness>();
            services.AddSingleton<CountryBusiness>();
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

        /// <summary>
        /// Run backend tests
        /// </summary>
        private async void GenerateTestData()
        {
            //var testDataGenerator = new TestDataGenerator();
            //await testDataGenerator.GenerateEmployees();
            //await testDataGenerator.GenerateRooms();
            //await testDataGenerator.GenerateCustomers();
            //await testDataGenerator.GenerateReservations();
            //await testDataGenerator.GenerateHousekeepingRequests();
            //await testDataGenerator.GenerateMaintenanceRequests();
            //await testDataGenerator.CancelReservation();
            //await testDataGenerator.CloseHousekeepingRequest();
            //await testDataGenerator.CloseMaintenanceRequest();
        }
    }
}

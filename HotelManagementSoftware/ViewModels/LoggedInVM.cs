using HotelManagementSoftware.ViewModels.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public enum SidebarPageName
    {
        Dashboard,
        Reservations,
        Arrivals,
        Departures,
        Customers,
        Rooms,
        RoomTypes,
        Employees
    }

    public class LoggedInVM : ObservableObject
    {
        public SidebarPageName[] SidebarPages { get; } = {
            SidebarPageName.Dashboard,
            SidebarPageName.Reservations,
            SidebarPageName.Arrivals,
            SidebarPageName.Departures,
            SidebarPageName.Customers,
            SidebarPageName.Rooms,
            SidebarPageName.RoomTypes,
            SidebarPageName.Employees
        };

        // Currently selected page on the sidebar
        private SidebarPageName currentPage = SidebarPageName.Dashboard;
        public SidebarPageName CurrentPage
        {
            get => currentPage;
            set
            {
                SetProperty(ref currentPage, value);
                NavigateToPage(currentPage);
            }
        }

        // View model of current page
        ObservableObject currentPageVM = App.Current.Services.GetRequiredService<DashboardVM>();
        public ObservableObject CurrentPageVM
        {
            get => currentPageVM;
            set => SetProperty(ref currentPageVM, value);
        }

        public ICommand LogoutCommand { get; }

        public LoggedInVM()
        {
            LogoutCommand = new RelayCommand(
                () => MainWindowNavigationUtils.NavigateTo(MainWindowPageName.Login)
            );
        }

        private void NavigateToPage(SidebarPageName pageName)
        {
            switch (pageName)
            {
                case SidebarPageName.Dashboard:
                    CurrentPageVM = App.Current.Services.GetRequiredService<DashboardVM>();
                    break;
                case SidebarPageName.Reservations:
                    CurrentPageVM = App.Current.Services.GetRequiredService<ReservationsVM>();
                    break;
                case SidebarPageName.Arrivals:
                    CurrentPageVM = App.Current.Services.GetRequiredService<ArrivalsVM>();
                    break;
                case SidebarPageName.Departures:
                    CurrentPageVM = App.Current.Services.GetRequiredService<DeparturesVM>();
                    break;
                case SidebarPageName.Customers:
                    CurrentPageVM = App.Current.Services.GetRequiredService<CustomersVM>();
                    break;
                case SidebarPageName.Rooms:
                    CurrentPageVM = App.Current.Services.GetRequiredService<RoomsVM>();
                    break;
                case SidebarPageName.RoomTypes:
                    CurrentPageVM = App.Current.Services.GetRequiredService<RoomTypesVM>();
                    break;
                case SidebarPageName.Employees:
                    CurrentPageVM = App.Current.Services.GetRequiredService<EmployeesVM>();
                    break;
            }
        }
    }
}

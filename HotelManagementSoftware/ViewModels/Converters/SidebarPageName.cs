using System;
using System.Globalization;
using System.Windows.Data;

namespace HotelManagementSoftware.ViewModels.Converters
{
    /// <summary>
    /// Convert SidebarPageName enum values to displayed strings.
    /// </summary>
    public class SidebarPageNameToString : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SidebarPageName pageName = (SidebarPageName)value;
            switch (pageName)
            {
                case SidebarPageName.Dashboard:
                    return "Dashboard";
                case SidebarPageName.Reservations:
                    return "Reservations";
                case SidebarPageName.Arrivals:
                    return "Arrivals";
                case SidebarPageName.Departures:
                    return "Departures";
                case SidebarPageName.Customers:
                    return "Customers";
                case SidebarPageName.Rooms:
                    return "Rooms";
                case SidebarPageName.RoomTypes:
                    return "Room Types";
                case SidebarPageName.Housekeeping:
                    return "Housekeeping";
                case SidebarPageName.Maintenance:
                    return "Maintenance";
                case SidebarPageName.Employees:
                    return "Employees";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SidebarPageNameToImageSource : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SidebarPageName pageName = (SidebarPageName)value;
            switch (pageName)
            {
                case SidebarPageName.Dashboard:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Reservations:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Arrivals:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Departures:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Customers:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Rooms:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.RoomTypes:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Housekeeping:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Maintenance:
                    return App.Current.FindResource("UserIcon");
                case SidebarPageName.Employees:
                    return App.Current.FindResource("UserIcon");
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

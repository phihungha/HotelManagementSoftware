using HotelManagementSoftware.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HotelManagementSoftware.ViewModels.Converters
{
    public class IdNumberTypeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IdNumberType employeeType = (IdNumberType)value;
            switch (employeeType)
            {
                case IdNumberType.Cmnd:
                    return "CMND";
                case IdNumberType.Passport:
                    return "Passport";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;
            switch (text)
            {
                case "CMND":
                    return IdNumberType.Cmnd;
                case "Passport":
                    return IdNumberType.Passport;
                default:
                    return null;
            }
        }
    }
}

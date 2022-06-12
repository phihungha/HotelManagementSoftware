using System;
using System.Globalization;
using System.Windows.Data;
using HotelManagementSoftware.Data;

namespace HotelManagementSoftware.ViewModels.Converters
{
    public class GenderToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Gender employeeType = (Gender)value;
            switch (employeeType)
            {
                case Gender.Male:
                    return "Male";
                case Gender.Female:
                    return "Female";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;
            switch (text)
            {
                case "Male":
                    return Gender.Male;
                case "Female":
                    return Gender.Female;
                default:
                    return null;
            }
        }
    }
}

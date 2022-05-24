using HotelManagementSoftware.ViewModels.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class LoggedInVM : ObservableObject
    {
        public ICommand LogoutCommand { get; }

        public LoggedInVM()
        {
            LogoutCommand = new RelayCommand(
                () => MainWindowNavigationUtils.NavigateTo(MainWindowPageName.Login)
            );
        }
    }
}

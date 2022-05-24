using HotelManagementSoftware.Business;
using HotelManagementSoftware.ViewModels.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class LoginVM : ObservableObject
    {
        private readonly EmployeeBusiness employeeBusiness;

        private string userName = "";
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string password = "";
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private bool isLoginInfoIncorrect = false;
        public bool IsLoginInfoIncorrect
        {
            get => isLoginInfoIncorrect;
            set => SetProperty(ref isLoginInfoIncorrect, value);
        }

        public ICommand LoginCommand { get; }

        public LoginVM(EmployeeBusiness employeeBusiness)
        {
            LoginCommand = new RelayCommand(Login);
            this.employeeBusiness = employeeBusiness;
        }

        private async void Login()
        {
            if (await employeeBusiness.Login(UserName, Password))
            {
                MainWindowNavigationUtils.NavigateTo(MainWindowUIName.LoggedIn);
                IsLoginInfoIncorrect = false;
            }
            else
                IsLoginInfoIncorrect = true;
        }
    }

}

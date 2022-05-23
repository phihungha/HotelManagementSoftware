using HotelManagementSoftware.Business;
using HotelManagementSoftware.ViewModels.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.ViewModels
{
    public class MainWindowVM : ObservableObject
    {
        ObservableObject currentPageVM;
        public ObservableObject CurrentPageVM
        {
            get => currentPageVM;
            set => SetProperty(ref currentPageVM, value);
        }

        public MainWindowVM()
        {
            WeakReferenceMessenger.Default.Register<MainWindowNavigationMessage>(
                this, (recipient, message) => NavigateTo(message.Value));
            NavigateTo(MainWindowUIName.Login);
        }

        public void NavigateTo(MainWindowUIName ui)
        {
            switch (ui)
            {
                case MainWindowUIName.Login:
                    CurrentPageVM = App.Current.Services.GetRequiredService<LoginVM>();
                    break;
                case MainWindowUIName.LoggedIn:
                    CurrentPageVM = App.Current.Services.GetRequiredService<LoggedInVM>();
                    break;
            }
        }
    }
}

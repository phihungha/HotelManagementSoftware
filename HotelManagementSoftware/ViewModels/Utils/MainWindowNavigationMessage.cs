using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.ViewModels.Utils
{
    public enum MainWindowPageName
    {
        Login,
        LoggedIn
    }

    public class MainWindowNavigationMessage : ValueChangedMessage<MainWindowPageName>
    {
        public MainWindowNavigationMessage(MainWindowPageName value) : base(value)
        {
        }
    }

    public class MainWindowNavigationUtils
    {
        public static void NavigateTo(MainWindowPageName ui)
        {
            WeakReferenceMessenger.Default.Send(new MainWindowNavigationMessage(ui));
        }
    }
}

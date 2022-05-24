using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.ViewModels.Utils
{
    public enum MainWindowUIName
    {
        Login,
        LoggedIn
    }

    public class MainWindowNavigationMessage : ValueChangedMessage<MainWindowUIName>
    {
        public MainWindowNavigationMessage(MainWindowUIName value) : base(value)
        {
        }
    }

    public class MainWindowNavigationUtils
    {
        public static void NavigateTo(MainWindowUIName ui)
        {
            WeakReferenceMessenger.Default.Send(new MainWindowNavigationMessage(ui));
        }
    }
}

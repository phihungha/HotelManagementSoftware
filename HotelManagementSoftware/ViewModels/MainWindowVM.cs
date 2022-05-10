using Microsoft.Toolkit.Mvvm.ComponentModel;
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
            CurrentPageVM = new LoginVM(this);
        }

        public void DisplayMainUI()
        {
            CurrentPageVM = new MainUIVM();
        }

        public void DisplayLoginUI()
        {
            CurrentPageVM = new LoginVM(this);
        }
    }
}

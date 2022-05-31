using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class MaintenanceVM : ObservableValidator
    {
        public ObservableCollection<MaintenanceCustomItem> MaintenanceCustomLists { get; set; }

        #region ctor
        public MaintenanceVM()
        {
            MaintenanceCustomLists = new ObservableCollection<MaintenanceCustomItem>();
            addMaintenanceItem();

            CommandAddNewIssue = new RelayCommand(executeAddIssueAction);
            CommandDeleteIssue = new RelayCommand(executeDeleteIssueAction);
            CommandSearch = new RelayCommand(executeSearchIssueAction);
            CommandFilterIssue = new RelayCommand(executeFilterIssueAction);
        }
       
        private void addMaintenanceItem()
        {
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(101, "note1", "customername1", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(102, "note2", "customername2", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(103, "note3", "customername3", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(104, "note4", "customername4", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(105, "note5", "customername5", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(106, "note6", "customername6", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(107, "note7", "customername7", MaintenanceRequestStatus.Opened));
            MaintenanceCustomLists.Add(new MaintenanceCustomItem(108, "note8", "customername8", MaintenanceRequestStatus.Opened));
        }

        #endregion

        #region command
        public ICommand CommandFilterIssue { get; }
        public ICommand CommandDeleteIssue { get; }
        public ICommand CommandAddNewIssue { get; }
        public ICommand CommandSearch { get; }

        public void executeFilterIssueAction()
        {
            MessageBox.Show("Filter");
        }
        public void executeSearchIssueAction()
        {
            MessageBox.Show("Search");
        }
        public void executeDeleteIssueAction()
        {
            MessageBox.Show("Open confirm delete message box");
        }
        public void executeAddIssueAction()
        {
            MessageBox.Show("Open add Issue edit window");
        }
        #endregion
    }

    /// <summary>
    /// Item in main window Maintenance
    /// </summary>
    public class MaintenanceCustomItem
    {
        public int RoomNumber { get; set; }
        public string Note { get; set; }
        public string CustomerName { get; set; }
        public MaintenanceRequestStatus Status { get; set; }

        public MaintenanceCustomItem()
        {

        }

        public MaintenanceCustomItem(int roomNumber, string note, string customerName, MaintenanceRequestStatus status)
        {
            RoomNumber = roomNumber;
            Note = note;
            CustomerName = customerName;
            Status = status;
        }
    }
}

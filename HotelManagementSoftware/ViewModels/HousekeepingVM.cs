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
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class HousekeepingVM : ObservableValidator
    {
        public ObservableCollection<HouseKeepingCustomItem> HouseKeepingCustomLists { get; set; }

        #region ctor
        public HousekeepingVM()
        {
            HouseKeepingCustomLists = new ObservableCollection<HouseKeepingCustomItem>();
            addMaintenanceItem();

            CommandAddNewIssue = new RelayCommand(executeAddIssueAction);
            CommandDeleteIssue = new RelayCommand(executeDeleteIssueAction);
            CommandSearch = new RelayCommand(executeSearchIssueAction);
            CommandFilterIssue = new RelayCommand(executeFilterIssueAction);
        }

        private void addMaintenanceItem()
        {
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(101, "note1", "customername1", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(102, "note2", "customername2", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(103, "note3", "customername3", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(104, "note4", "customername4", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(105, "note5", "customername5", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(106, "note6", "customername6", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(107, "note7", "customername7", HousekeepingRequestStatus.Opened));
            HouseKeepingCustomLists.Add(new HouseKeepingCustomItem(108, "note8", "customername8", HousekeepingRequestStatus.Opened));
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
    public class HouseKeepingCustomItem
    {
        public int RoomNumber { get; set; }
        public string Note { get; set; }
        public string CustomerName { get; set; }
        public HousekeepingRequestStatus Status { get; set; }

        public HouseKeepingCustomItem()
        {

        }

        public HouseKeepingCustomItem(int roomNumber, string note, string customerName, HousekeepingRequestStatus status)
        {
            RoomNumber = roomNumber;
            Note = note;
            CustomerName = customerName;
            Status = status;
        }
    }
}

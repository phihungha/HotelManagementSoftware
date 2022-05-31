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
        #region private variable


        #endregion

        #region property

        public ObservableCollection<HouseKeepingCustomItem> HouseKeepingCustomLists { get; set; }
        public ObservableCollection<ReporterItemHouseKeeping> ReporterLists { get; set; }
        public ObservableCollection<ActivityItemHouseKeeping> ActivityLists { get; set; }
        public int AllReports { get; set; }
        public int TotalReportsDone { get; set; }
        public int TotalReportsNeedToDone { get; set; }

        #endregion

        #region ctor
        public HousekeepingVM()
        {
            HouseKeepingCustomLists = new ObservableCollection<HouseKeepingCustomItem>();
            addMaintenanceItem();

            ReporterLists = new ObservableCollection<ReporterItemHouseKeeping>();
            addReporter();

            ActivityLists = new ObservableCollection<ActivityItemHouseKeeping>();
            addActivity();

            AllReports = 23;
            TotalReportsDone = 10;
            TotalReportsNeedToDone = 13;

            CommandAddNewIssue = new RelayCommand(executeAddIssueAction);
            CommandDeleteIssue = new RelayCommand(executeDeleteIssueAction);
            CommandSearch = new RelayCommand(executeSearchIssueAction);
            CommandFilterIssue = new RelayCommand(executeFilterIssueAction);
        }
        private void addActivity()
        {
            ActivityLists.Add(new ActivityItemHouseKeeping("Name1", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name2", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name3", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name4", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name5", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name6", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name7", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItemHouseKeeping("Name8", "Completed the task", DateTime.Now));
        }

        private void addReporter()
        {
            ReporterLists.Add(new ReporterItemHouseKeeping("Name1", "note1"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name2", "note2"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name3", "note3"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name4", "note4"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name5", "note5"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name6", "note6"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name7", "note7"));
            ReporterLists.Add(new ReporterItemHouseKeeping("Name8", "note8"));
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
    /// Item for activity listbox
    /// </summary>
    public class ActivityItemHouseKeeping
    {
        public string Name { get; set; }
        public string StatusString { get; set; }
        public DateTime CreateTime { get; set; }
        public ActivityItemHouseKeeping()
        {

        }

        public ActivityItemHouseKeeping(string name, string statusString, DateTime createTime)
        {
            Name = name;
            StatusString = statusString;
            CreateTime = createTime;
        }
    }

    /// <summary>
    /// Item for reporter listbox
    /// </summary>
    public class ReporterItemHouseKeeping
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public ReporterItemHouseKeeping()
        {

        }

        public ReporterItemHouseKeeping(string name, string note)
        {
            Name = name;
            Note = note;
        }
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

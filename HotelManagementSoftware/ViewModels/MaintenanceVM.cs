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
        #region private variable


        #endregion

        #region property

        public ObservableCollection<MaintenanceCustomItem> MaintenanceCustomLists { get; set; }
        public ObservableCollection<ReporterItem> ReporterLists { get; set; }
        public ObservableCollection<ActivityItem> ActivityLists { get; set; }
        public int AllReports { get; set; }
        public int TotalReportsDone { get; set; }
        public int TotalReportsNeedToDone { get; set; }

        #endregion

        #region ctor
        public MaintenanceVM()
        {
            MaintenanceCustomLists = new ObservableCollection<MaintenanceCustomItem>();
            addMaintenanceItem();

            ReporterLists = new ObservableCollection<ReporterItem>();
            addReporter();

            ActivityLists = new ObservableCollection<ActivityItem>();
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
            ActivityLists.Add(new ActivityItem("Name1", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name2", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name3", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name4", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name5", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name6", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name7", "Completed the task", DateTime.Now));
            ActivityLists.Add(new ActivityItem("Name8", "Completed the task", DateTime.Now));
        }

        private void addReporter()
        {
            ReporterLists.Add(new ReporterItem("Name1", "note1"));
            ReporterLists.Add(new ReporterItem("Name2", "note2"));
            ReporterLists.Add(new ReporterItem("Name3", "note3"));
            ReporterLists.Add(new ReporterItem("Name4", "note4"));
            ReporterLists.Add(new ReporterItem("Name5", "note5"));
            ReporterLists.Add(new ReporterItem("Name6", "note6"));
            ReporterLists.Add(new ReporterItem("Name7", "note7"));
            ReporterLists.Add(new ReporterItem("Name8", "note8"));
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
    /// Item for activity listbox
    /// </summary>
    public class ActivityItem
    {
        public string Name { get; set; }
        public string StatusString { get; set; }
        public DateTime CreateTime { get; set; }
        public ActivityItem()
        {
           
        }

        public ActivityItem(string name, string statusString, DateTime createTime)
        {
            Name = name;
            StatusString = statusString;
            CreateTime = createTime;
        }
    }

    /// <summary>
    /// Item for reporter listbox
    /// </summary>
    public class ReporterItem
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public ReporterItem()
        {

        }

        public ReporterItem(string name, string note)
        {
            Name = name;
            Note = note;
        }
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

using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class HousekeepingEditWindowVM : ObservableValidator
    {
        #region private variables
        private int issueID;
        private int roomID;
        private DateTime startTime;
        private DateTime endTime;
        private DateTime closeTime;
        private HousekeepingRequestStatus status;
        private string? note;
        #endregion


        #region property validation
        public int IssueID
        {
            get => issueID;
            set => SetProperty(ref issueID, value, true);
        }

        [Required(ErrorMessage = "Room cannot be empty")]
        public int RoomID
        {
            get => roomID;
            set => SetProperty(ref roomID, value, true);
        }

        [Required(ErrorMessage = "Start time cannot be empty")]
        public DateTime StartTime
        {
            get => startTime;
            set
            {
                SetProperty(ref startTime, value, true);
                ValidateProperty(EndTime, nameof(EndTime));
            }
        }

        [Required(ErrorMessage = "End time cannot be empty")]
        [GreaterThan(nameof(StartTime), "End date should come after start date.")]
        public DateTime EndTime
        {
            get => endTime;
            set
            {
                SetProperty(ref endTime, value, EndTime > StartTime);
            }
        }

        public DateTime CloseTime
        {
            get => closeTime;
            set => SetProperty(ref closeTime, value, true);
        }

        [Required(ErrorMessage = "Status time cannot be empty")]
        public HousekeepingRequestStatus Status
        {
            get => status;
            set => SetProperty(ref status, value, true);
        }

        public string? Note
        {
            get => note;
            set => SetProperty(ref note, value, true);
        }
        #endregion

        public HousekeepingEditWindowVM()
        {
            IssueID = 1;
            RoomID = 404;
            StartTime = new DateTime(2020, 1, 1);
            EndTime = new DateTime(2019, 1, 2);
            CloseTime = new DateTime(2020, 1, 3);
            Status = HousekeepingRequestStatus.Opened;
            Note = "note";

            CommandCancel = new RelayCommand(executeCancelAction);
            CommandUpdate = new RelayCommand(executeUpdateAction, canUpdate);
        }

        #region command
        public ICommand CommandCancel { get; }
        public ICommand CommandUpdate { get; }


        public bool canUpdate()
        {
            return true;
        }

        public void executeUpdateAction()
        {
            MessageBox.Show("Update");
        }
        public void executeCancelAction()
        {
            MessageBox.Show("Cancel");
        }
        #endregion
    }
}

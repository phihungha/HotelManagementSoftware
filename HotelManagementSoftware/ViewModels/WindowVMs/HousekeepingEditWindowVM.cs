using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private HousekeepingBusiness? housekeepingBusiness;
        private EmployeeBusiness? employeeBusiness;
        private RoomBusiness roomBusiness;
        private HousekeepingRequestType housekeepingRequestType;
        private int? currentRequestId;

        public HousekeepingRequestType HousekeepingRequestType
        {
            get => housekeepingRequestType;
            set
            {
                SetProperty(ref housekeepingRequestType, value, true);
                if (HousekeepingRequestType.Equals(HousekeepingRequestType.Add))
                {
                    Title = "Add housekeeping request window";
                    VisibilityCbx = Visibility.Visible;
                    VisibilityTextbox = Visibility.Hidden;
                    IsEnabled = true;
                }
                else
                {
                    Title = "Edit housekeeping request window";
                    VisibilityCbx = Visibility.Hidden;
                    VisibilityTextbox = Visibility.Visible;
                }
            }
        }
        public int? CurrentRequestId
        {
            get => currentRequestId;
            set
            {
                SetProperty(ref currentRequestId, value, true);
                if (CurrentRequestId != null)
                {
                    GetCurrentRequest();
                }
            }
        }



        public bool IsEnabled { get; set; }
        public Visibility VisibilityCbx { get; set; }
        public Visibility VisibilityTextbox { get; set; }
        public HousekeepingVM HousekeepingVM { get; set; }
        public ObservableCollection<Room> RoomLists { get; set; } = new();
        public String Title { get; set; }
        public HousekeepingRequest? Current { get; set; }

        #region private variables
        private Room? room;
        private DateTime startTime;
        private DateTime endTime;
        private DateTime? closeTime;
        private HousekeepingRequestStatus status;
        private string? note;
        #endregion

        #region property validation

        [Required(ErrorMessage = "Room cannot be empty")]
        public Room Room
        {
            get => room;
            set => SetProperty(ref room, value, true);
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
        // [GreaterThan(nameof(StartTime), "End date should come after start date.")]
        public DateTime EndTime
        {
            get => endTime;
            set
            {
                SetProperty(ref endTime, value, EndTime > StartTime);
            }
        }

        public DateTime? CloseTime
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

        public HousekeepingEditWindowVM(HousekeepingBusiness? housekeepingBusiness, EmployeeBusiness? employeeBusiness, RoomBusiness roomBusiness)
        {
            this.roomBusiness = roomBusiness;
            this.employeeBusiness = employeeBusiness;
            this.housekeepingBusiness = housekeepingBusiness;
            FillRoomCombobox();

            CommandCancel = new RelayCommand(executeCancelAction);
            CommandUpdate = new RelayCommand(executeUpdateAction);
            CommandClose = new RelayCommand(executeCloseAction);
        }

        private async void FillRoomCombobox()
        {
            if (roomBusiness != null)
            {
                List<Room> rooms = await roomBusiness.GetRooms();
                RoomLists.Clear();
                rooms.ForEach(room =>
                {
                    RoomLists.Add(room);
                });

            }
        }
        private async void GetCurrentRequest()
        {
            if (housekeepingBusiness == null || CurrentRequestId == null) return;
            Current = await housekeepingBusiness.GetHousekeepingRequestById(CurrentRequestId.Value);

            if (Current == null) return;
            HousekeepingRequest request = Current;
            Room = request.Room;
            StartTime = request.StartTime;
            EndTime = request.EndTime;
            CloseTime = request.CloseTime;
            Status = request.Status;
            Note = request.Note;
        }

        #region command
        public ICommand CommandCancel { get; }
        public ICommand CommandClose { get; }
        public ICommand CommandUpdate { get; }
        public void executeUpdateAction()
        {
            if (HousekeepingRequestType.Equals(HousekeepingRequestType.Add))
            {
                AddRequest();
            }
            else
            {
                UpdateRequest();
            }
        }
        public void executeCloseAction()
        {
            CloseRequest();
        }
        public void executeCancelAction()
        {
            CloseAction();
        }
        #endregion
        public Action CloseAction { get; set; }

        private async void AddRequest()
        {
            try
            {
                ValidateAllProperties();
                if (employeeBusiness == null || housekeepingBusiness == null) return;

                Employee? current = employeeBusiness.CurrentEmployee;

                if (current == null || Room == null) return;
                HousekeepingRequest request = new HousekeepingRequest(current.EmployeeId, current, Room, StartTime, EndTime, Note);

                await housekeepingBusiness.CreateHousekeepingRequest(request);
                HousekeepingVM.GetAllItem();
                CloseAction();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void UpdateRequest()
        {
            try
            {
                ValidateAllProperties();
                if (housekeepingBusiness == null || Current==null) return;
                HousekeepingRequest request = Current;
                request.StartTime = StartTime;
                request.EndTime = EndTime;
                request.Note = Note;
                await housekeepingBusiness.EditHousekeepingRequest(request);
                HousekeepingVM.GetAllItem();
                CloseAction();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async void CloseRequest()
        {
            try
            {
                ValidateAllProperties();
                if (housekeepingBusiness == null || employeeBusiness == null || Current == null) return;
                Employee? current = employeeBusiness.CurrentEmployee;
                HousekeepingRequest request = Current;
                request.StartTime = StartTime;
                request.EndTime = EndTime;
                request.Note = Note;

                if (current == null) return;
                await housekeepingBusiness.CloseHousekeepingRequest(request, DateTime.Now, current);
                HousekeepingVM.GetAllItem();
                CloseAction();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }

    public enum HousekeepingRequestType
    {
        Add, Edit
    }
}

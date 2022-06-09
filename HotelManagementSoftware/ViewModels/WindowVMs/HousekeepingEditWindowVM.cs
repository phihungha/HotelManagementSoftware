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
        private RoomBusiness? roomBusiness;
        private HousekeepingVM housekeepingVM;
        public bool IsEnabled { get; set; }
        public Visibility VisibilityCbx { get; set; }
        public Visibility VisibilityTextbox { get; set; }
        public HousekeepingVM HousekeepingVM
        {
            get => housekeepingVM;
            set
            {
                SetProperty(ref housekeepingVM, value);
                if (HousekeepingVM != null)
                {
                    if (HousekeepingVM.SelectedItemHouseKeepingRequest != null)
                    {

                        Room = HousekeepingVM.SelectedItemHouseKeepingRequest.Room;

                        StartTime=HousekeepingVM.SelectedItemHouseKeepingRequest.StartTime;
                        EndTime=HousekeepingVM.SelectedItemHouseKeepingRequest.EndTime;

                        CloseTime= HousekeepingVM.SelectedItemHouseKeepingRequest.CloseTime;
                        Status=HousekeepingVM.SelectedItemHouseKeepingRequest.Status;

                        Note=HousekeepingVM.SelectedItemHouseKeepingRequest.Note;
                    }
                }
            }
        }
        public ObservableCollection<Room> RoomLists { get; set; } = new();
        public String Title { get; set; }
        public HousekeepingRequestType HousekeepingRequestType { get; set; }
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
        public Room? Room
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
            this.housekeepingBusiness= housekeepingBusiness;
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
                if (employeeBusiness == null || housekeepingBusiness == null) return;

                Employee? current = employeeBusiness.CurrentEmployee;

                if (current == null || Room == null) return;
                HousekeepingRequest request = new HousekeepingRequest(current.EmployeeId, current, Room, StartTime, EndTime, Note);

                await housekeepingBusiness.CreateHousekeepingRequest(request);
                housekeepingVM.GetAllItem();
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
                if (housekeepingBusiness == null) return;
                HousekeepingRequest request = housekeepingVM.SelectedItemHouseKeepingRequest;
                request.StartTime = StartTime;
                request.EndTime = EndTime;
                request.Note = Note;
                await housekeepingBusiness.EditHousekeepingRequest(request);
                housekeepingVM.GetAllItem();
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
                if (housekeepingBusiness == null || employeeBusiness == null) return;
                Employee? current = employeeBusiness.CurrentEmployee;
                HousekeepingRequest request = housekeepingVM.SelectedItemHouseKeepingRequest;
                request.StartTime = StartTime;
                request.EndTime = EndTime;
                request.Note = Note;

                if (current == null) return;
                await housekeepingBusiness.CloseHousekeepingRequest(request, DateTime.Now, current);
                housekeepingVM.GetAllItem();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
          
        }
         
    }

    public enum HousekeepingRequestType
    {
        Add,Edit
    }
}

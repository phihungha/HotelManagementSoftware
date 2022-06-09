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
    public class MaintenanceEditWindowVM : ObservableValidator
    {
        private MaintenanceBusiness? maintenanceBusiness;
        private EmployeeBusiness? employeeBusiness;
        private RoomBusiness? roomBusiness;
        private MaintenanceVM maintenanceVM;
        private MaintenanceRequest? currentItem;
        public bool IsEnabled { get; set; }
        public Visibility VisibilityCbx { get; set; }
        public Visibility VisibilityTextbox { get; set; }
        public MaintenanceRequest? CurrentItem
        {
            get => currentItem;
            set
            {
                SetProperty(ref currentItem, value, true);
                if (CurrentItem != null)
                {
                    Room = CurrentItem.Room;

                    StartTime = CurrentItem.StartTime;
                    EndTime = CurrentItem.EndTime;

                    CloseTime = CurrentItem.CloseTime;
                    Status = CurrentItem.Status;

                    Note = CurrentItem.Note;
                }
            }
        }
        public MaintenanceVM MaintenanceVM
        {
            get => maintenanceVM;
            set
            {
                SetProperty(ref maintenanceVM, value);
            }
        }
        public ObservableCollection<Room> RoomLists { get; set; } = new();
        public String Title { get; set; }
        public MaintenanceRequestType MaintenanceRequestType { get; set; }
        #region private variables
        private Room? room;
        private DateTime startTime;
        private DateTime endTime;
        private DateTime? closeTime;
        private MaintenanceRequestStatus status;
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
        public MaintenanceRequestStatus Status
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

        public MaintenanceEditWindowVM(MaintenanceBusiness? maintenanceBusiness, EmployeeBusiness? employeeBusiness, RoomBusiness roomBusiness)
        {
            this.roomBusiness = roomBusiness;
            this.employeeBusiness = employeeBusiness;
            this.maintenanceBusiness = maintenanceBusiness;
            FillRoomCombobox();

            CommandCancel = new RelayCommand(executeCancelAction);
            CommandUpdate = new RelayCommand(executeUpdateAction);
            CommandClose = new RelayCommand(executeCloseAction);
            CommandEditItem = new RelayCommand(executeEditItemAction);
            CommandDeleteItem = new RelayCommand(executeDeleteItemAction);
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
        public ICommand CommandDeleteItem { get; }
        public ICommand CommandEditItem { get; }
        public ICommand CommandCancel { get; }
        public ICommand CommandClose { get; }
        public ICommand CommandUpdate { get; }
        public void executeUpdateAction()
        {
            if (MaintenanceRequestType.Equals(MaintenanceRequestType.Add))
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
        public void executeEditItemAction()
        {
            MessageBox.Show("Edit item");
        }
        public void executeDeleteItemAction()
        {
            MessageBox.Show("Delete");
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
                if (employeeBusiness == null || maintenanceBusiness == null) return;

                Employee? current = employeeBusiness.CurrentEmployee;

                if (current == null || Room == null) return;
                MaintenanceRequest request = new MaintenanceRequest(current.EmployeeId, current, Room, StartTime, EndTime, Note);

                await maintenanceBusiness.CreateMaintenanceRequest(request);
                maintenanceVM.GetAllItem();
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
                if (maintenanceBusiness == null || CurrentItem==null) return;
                MaintenanceRequest request = CurrentItem;
                request.StartTime = StartTime;
                request.EndTime = EndTime;
                request.Note = Note;
                await maintenanceBusiness.EditMaintenanceRequest(request);
                maintenanceVM.GetAllItem();
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
                if (maintenanceBusiness == null || employeeBusiness == null || CurrentItem == null) return;
                Employee? current = employeeBusiness.CurrentEmployee;
                MaintenanceRequest request = CurrentItem;
                request.StartTime = StartTime;
                request.EndTime = EndTime;
                request.Note = Note;

                if (current == null) return;
                await maintenanceBusiness.CloseMaintenanceRequest(request, DateTime.Now, current);
                maintenanceVM.GetAllItem();
                CloseAction();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
    public enum MaintenanceRequestType
    {
        Add, Edit
    }
}

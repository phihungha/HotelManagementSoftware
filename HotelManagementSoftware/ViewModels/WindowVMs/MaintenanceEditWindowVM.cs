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
        private int? currentRequestId;
        private MaintenanceRequestType maintenanceRequestType;

        public MaintenanceVM MaintenanceVM
        {
            get => maintenanceVM;
            set
            {
                SetProperty(ref maintenanceVM, value, true);
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
        public MaintenanceRequestType MaintenanceRequestType
        {
            get => maintenanceRequestType;
            set
            {
                SetProperty(ref maintenanceRequestType, value, true);
                if (MaintenanceRequestType.Equals(MaintenanceRequestType.Add))
                {
                    Title = "Add maintenance request window";
                    VisibilityCbx = Visibility.Visible;
                    VisibilityTextbox = Visibility.Hidden;
                    IsEnabled = true;
                }
                else
                {
                    Title = "Edit maintenance request window";
                    VisibilityCbx = Visibility.Hidden;
                    VisibilityTextbox = Visibility.Visible;
                }
            }
        }

        public ObservableCollection<MaintenanceItem> Items { get; set; } = new();
        public MaintenanceItem SelectedItem { get; set; }
        public bool IsEnabled { get; set; }
        public Visibility VisibilityCbx { get; set; }
        public Visibility VisibilityTextbox { get; set; }
        public ObservableCollection<Room> RoomLists { get; set; } = new();
        public String Title { get; set; }
        public MaintenanceRequest? CurrentItem { get; set; }

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
            CommandDeleteItem = new RelayCommand(executeDeleteItemAction);
            CommandAddRow = new RelayCommand(executeAddRowAction);
        }
        private async void GetCurrentRequest()
        {
            if (maintenanceBusiness == null || CurrentRequestId==null) return;
            CurrentItem = await maintenanceBusiness.GetMaintenanceRequestById(CurrentRequestId.Value);

            if (CurrentItem == null) return;
            MaintenanceRequest request = CurrentItem;
            Room = request.Room;
            StartTime = request.StartTime;
            EndTime = request.EndTime;
            CloseTime = request.CloseTime;
            Status = request.Status;
            Note = request.Note;

            List<MaintenanceItem> listItem = CurrentItem.MaintenanceItems;
            Items.Clear();
            listItem.ForEach(item =>
            {
                Items.Add(item);
            });
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
        public ICommand CommandAddRow { get; }
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
        public void executeAddRowAction()
        {
            Items.Add(new MaintenanceItem("name", 0));
        }
        public void executeCloseAction()
        {
            CloseRequest();
        }
        public void executeDeleteItemAction()
        {
            MessageBoxResult result = MessageBox.Show("Delete this item", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes && SelectedItem != null)
            {
                Items.Remove(SelectedItem);   
            }
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

                //add item
                List<MaintenanceItem> listItem = Items.ToList();
                request.MaintenanceItems = listItem;

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

                //add item
                List<MaintenanceItem> listItem = Items.ToList();
                MessageBox.Show(Items.Count.ToString());
                request.MaintenanceItems = listItem;

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

                //add item
                List<MaintenanceItem> listItem = Items.ToList();
                request.MaintenanceItems = listItem;

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

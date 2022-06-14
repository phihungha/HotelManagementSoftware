﻿using HotelManagementSoftware.Business;
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
        private MaintenanceBusiness maintenanceBusiness;
        private EmployeeBusiness employeeBusiness;
        private RoomBusiness roomBusiness;
        private MaintenanceRequest? maintenance = null;

        private bool canClose = false;
        public bool CanClose
        {
            get => canClose;
            set
            {
                SetProperty(ref canClose, value);
            }
        }

        private bool canNotClose = true;
        public bool CanNotClose
        {
            get => canNotClose;
            set
            {
                SetProperty(ref canNotClose, value);
            }
        }

        private Visibility visibilityCbx = Visibility.Visible;
        public Visibility VisibilityCbx
        {
            get => visibilityCbx;
            set
            {
                SetProperty(ref visibilityCbx, value);
            }
        }

        private Visibility visibilityTextbox = Visibility.Hidden;
        public Visibility VisibilityTextbox
        {
            get => visibilityTextbox;
            set
            {
                SetProperty(ref visibilityTextbox, value);
            }
        }
        private bool isEnabled = true;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                SetProperty(ref isEnabled, value);
            }
        }

        public ObservableCollection<Room> RoomLists { get; set; } = new();

        public ObservableCollection<MaintenanceItem> Items { get; set; } = new();
        public List<MaintenanceItem> ItemNeedToDelete { get; set; } = new();
        public MaintenanceItem SelectedItem { get; set; }

        public DateTime MinStartDay { get; set; }
        public DateTime MaxStartDay { get; set; }
        public DateTime MinEndDay { get; set; }
        public DateTime DefaultDate { get; set; }

        #region private variables
        private int roomNumber;
        private Room room;
        private DateTime startTime;
        private DateTime endTime;
        private DateTime? closeTime;
        private MaintenanceRequestStatus status;
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
            }
        }

        [Required(ErrorMessage = "End time cannot be empty")]
        public DateTime EndTime
        {
            get => endTime;
            set
            {
                SetProperty(ref endTime, value, true);
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

        public int RoomNumber
        {
            get => roomNumber;
            set => SetProperty(ref roomNumber, value, true);
        }
        #endregion
        public MaintenanceEditWindowVM(MaintenanceBusiness maintenanceBusiness, EmployeeBusiness employeeBusiness, RoomBusiness roomBusiness)
        {
            this.roomBusiness = roomBusiness;
            this.employeeBusiness = employeeBusiness;
            this.maintenanceBusiness = maintenanceBusiness;

            FillRoomCombobox();
            CommandAddRow = new RelayCommand(AddRow);
        }
        public ICommand CommandAddRow { get; set; } 
        private void AddRow()
        {
            Items.Add(new MaintenanceItem("name", 0));
        }
        public void setUpDatePicker()
        {
            MinStartDay = new DateTime(1970, 1, 1);
            MaxStartDay = DateTime.Now.AddYears(-18);

            MinEndDay = MaxStartDay.AddDays(1);
            DefaultDate = DateTime.Now;

            StartTime = MaxStartDay;
            EndTime = MinEndDay;
        }
        public void setDisplayForAdd()
        {
            VisibilityCbx = Visibility.Visible;
            VisibilityTextbox = Visibility.Hidden;
            IsEnabled = true;
        }

        public void setDisplayForEdit()
        {
            VisibilityCbx = Visibility.Hidden;
            VisibilityTextbox = Visibility.Visible;
            IsEnabled = true;
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
        public async void GetCurrentRequestWithId(int id)
        {
            MaintenanceRequest? request = await maintenanceBusiness.GetMaintenanceRequestById(id);

            if (request == null)
            {
                return;
            }

            maintenance = request;

            CanClose = true;
            CanNotClose = false;
            setUpDatePicker();
            setDisplayForEdit();
            if (maintenance.Status.Equals(MaintenanceRequestStatus.Closed))
            {
                IsEnabled = false;
            }

            DisplayItems();

            Room = maintenance.Room;
            RoomNumber = maintenance.Room.RoomNumber;
            StartTime = maintenance.StartTime;
            EndTime = maintenance.EndTime;
            CloseTime = maintenance.CloseTime;
            Status = maintenance.Status;
            Note = maintenance.Note;

        }

        public void DisplayItems()
        {
            Items.Clear();
            maintenance.MaintenanceItems.ForEach(item =>
            {
                Items.Add(item);
            });
        }

        public async Task<bool> SaveRequest()
        {
            ValidateAllProperties();
            if (GetErrors().Count() != 0)
                return false;

            if (maintenance != null && maintenanceBusiness!=null)
            {
                maintenance.StartTime = StartTime;
                maintenance.EndTime = EndTime;
                maintenance.Note = Note;

                await maintenanceBusiness.DeleteMaintenanceItems(maintenance.MaintenanceItems);
                //maintenance.MaintenanceItems = Items.ToList();
                // MessageBox.Show(maintenance.MaintenanceItems.Count.ToString());
                await maintenanceBusiness.EditMaintenanceRequest(maintenance);
            }
            else
            {
                if (employeeBusiness.CurrentEmployee != null && maintenanceBusiness!=null)
                {
                    Employee openEmployee = employeeBusiness.CurrentEmployee;
                    var request = new MaintenanceRequest(openEmployee.EmployeeId,
                                       openEmployee,
                                       Room,
                                       StartTime,
                                       EndTime,
                                       Note);
                    request.MaintenanceItems = Items.ToList();
                    await maintenanceBusiness.CreateMaintenanceRequest(request);
                }
            }
            return true;
        }

        public async Task<bool> CloseRequest()
        {
            ValidateAllProperties();
            if (GetErrors().Count() != 0)
                return false;

            if (maintenanceBusiness == null || employeeBusiness == null || maintenance == null) return false;
            Employee? current = employeeBusiness.CurrentEmployee;
            MaintenanceRequest request = maintenance;
            request.StartTime = StartTime;
            request.EndTime = EndTime;
            request.Note = Note;

            /*await maintenanceBusiness.DeleteMaintenanceItems(request.MaintenanceItems);
            request.MaintenanceItems = Items.ToList();*/


            if (current == null) return false;
            await maintenanceBusiness.CloseMaintenanceRequest(request, DateTime.Now, current);

            return true;
        }

        public void DeleteItem()
        {
            Items.Remove(SelectedItem);
        }


    }
}

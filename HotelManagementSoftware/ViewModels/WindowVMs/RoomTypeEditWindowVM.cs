using HotelManagementSoftware.Business;
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
    public class RoomTypeEditWindowVM : ObservableValidator
    {
        #region private variables
        private int rate;
        private int capacity;
        private string name;
        private string? description;
        private RoomTypeBusiness? roomTypeBusiness;
        private RoomTypesVM roomTypesVM;
        public RoomTypesVM RoomTypesVM
        {
            get => roomTypesVM;
            set
            {
                SetProperty(ref roomTypesVM, value);
                if (RoomTypesVM != null)
                {
                    if (RoomTypesVM.SelectedRoomType != null)
                    {
                        Name = RoomTypesVM.SelectedRoomType.Name;
                        Capacity = RoomTypesVM.SelectedRoomType.Capacity;
                        Rate = (int)RoomTypesVM.SelectedRoomType.Rate;
                        NumOfRooms = RoomTypesVM.SelectedRoomType.Rooms.Count;
                        Description = RoomTypesVM.SelectedRoomType.Description;

                    }
                }
            }
        }
        public RoomTypeEditWindowType type { get; set; }
        #endregion
        #region property validation
        [Required(ErrorMessage = "Rate cannot be empty")]
        public int Rate
        {
            get => rate;
            set => SetProperty(ref rate, value, true);
        }
        [Required(ErrorMessage = "Capacity cannot be empty")]
        public int Capacity
        {
            get => capacity;
            set => SetProperty(ref capacity, value, true);
        }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, true);
        }
        public string? Description
        {
            get => description;
            set => SetProperty(ref description, value, true);
        }
        #endregion
        public int NumOfRooms { get; set; }
        public RoomTypeEditWindowVM(RoomTypeBusiness? roomTypeBusiness)
        {
            this.roomTypeBusiness = roomTypeBusiness;
            CommandCancel = new RelayCommand(executeCancelAction);
            CommandSave = new RelayCommand(executeCancelAction);
        }
        #region command
        public ICommand CommandCancel { get; }
        public ICommand CommandSave { get; }

        public async void executeSaveAction()
        {
            
             if (type == RoomTypeEditWindowType.Edit)
            {
                RoomType roomType = RoomTypesVM.SelectedRoomType;
                roomType.Name = Name;
                roomType.Rate = Rate;
                roomType.Capacity = Capacity;
                roomType.Description = Description;
                roomTypeBusiness.EditRoomType(roomType);
            }
            else 
            {
                RoomType roomType = new RoomType(Name, Capacity, Rate, Description);
                roomTypeBusiness.AddRoomType(roomType);
            }
            CloseAction();
            RoomTypesVM.GetAllRoomType();
        }
        public void executeCancelAction()
        {
            CloseAction();
        }
        #endregion
        public Action CloseAction { get; set; }

        public enum RoomTypeEditWindowType
        {
            Add, Edit
        }
    }
}

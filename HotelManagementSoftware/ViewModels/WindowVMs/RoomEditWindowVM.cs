using HandyControl.Controls;
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
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class RoomEditWindowVM : ObservableValidator
    {
        #region private variables
        private int number;
        private int floor;
        public RoomStatus[] Status { get; set; }
        private RoomBusiness? roomBusiness;
        public RoomEditWindowType roomEditWindowType { get; set; }

        private string? note;
        #endregion
        #region property validation
        private RoomsVM roomsVM;
        public RoomsVM RoomsVM
        {
            get => roomsVM;
            set
            {
                SetProperty(ref roomsVM, value);
                if (RoomsVM != null)
                {
                    if (RoomsVM.SelectedRoom != null)
                    {
                        Number = RoomsVM.SelectedRoom.RoomNumber;
                        Note = RoomsVM.SelectedRoom.Note;
                        selectedstatus = RoomsVM.SelectedRoom.Status;
                        SelectedRoomType = RoomsVM.SelectedRoom.RoomType;

                    }
                }
            }
        }
        [Required(ErrorMessage = "Number cannot be empty")]
        public int Number
        {
            get => number;
            set => SetProperty(ref number, value, true);
        }
        [Required(ErrorMessage = "Floor cannot be empty")]
        public int Floor
        {
            get => floor;
            set => SetProperty(ref floor, value, true);
        }
        public string? Note
        {
            get => note;
            set => SetProperty(ref note, value, true);
        }
        #endregion
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public RoomType? SelectedRoomType { get; set; }
        public RoomStatus selectedstatus { get; set; }
        public RoomEditWindowVM(RoomBusiness? roomBusiness)
        {

            this.roomBusiness = roomBusiness;
            CommandCancel = new RelayCommand(executeCancelAction);
            CommandSave = new RelayCommand(executeCancelAction);
        }

        #region command
        public ICommand CommandCancel { get; }
        public ICommand CommandSave { get; }

        public void executeSaveAction()
        {

            if (roomEditWindowType == RoomEditWindowType.Edit)
            {
                Room room = RoomsVM.SelectedRoom;
                room.RoomNumber = Number;
                room.RoomType = SelectedRoomType;
                room.Floor = Floor;
                room.Note = Note;
                roomBusiness.EditRoom(room);
            }
            else
            {
                Room room = new Room(Number, SelectedRoomType, Floor, selectedstatus, Note);
                roomBusiness.AddRoom(room);
            }
            CloseAction();
            RoomsVM.GetAllRoom();
        }
        public void executeCancelAction()
        {
            CloseAction();
        }
        #endregion
        public Action CloseAction { get; set; }

        public enum RoomEditWindowType
        {
            Add, Edit
        }
    }
}

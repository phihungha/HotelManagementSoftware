using HandyControl.Controls;
using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels.WindowVMs;
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
    public class RoomsVM: ObservableValidator
    {
        public ObservableCollection<Room> Rooms { get; set; }
        private RoomBusiness? roomBusiness;
        public Room SelectedRoom { get; set; }

        public RoomsVM(RoomBusiness? _roomBusiness)
        {
            roomBusiness = _roomBusiness;
            Rooms = new ObservableCollection<Room>();
            GetAllRoom();
            CommandAdd = new RelayCommand(executeAddAction);
            CommandDelete = new RelayCommand(executeDeleteAction);
            CommandEdit = new RelayCommand(executeEditAction);
        }
        #region command
        public ICommand CommandAdd { get; }
        public ICommand CommandDelete { get; }
        public ICommand CommandEdit { get; }
        public void executeAddAction()
        {

        }
        public async void executeDeleteAction()
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Delete this employee", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (roomBusiness != null && SelectedRoom != null)
                {
                    roomBusiness.RemoveRoom(SelectedRoom);
                }
                GetAllRoom();
            }
        }
        public void executeEditAction()
        {
            RoomEditWindow roomEditWindow = new RoomEditWindow();
            RoomEditWindowVM vm = new RoomEditWindowVM(roomBusiness);
            vm.RoomsVM = this;
            vm.roomEditWindowType = RoomEditWindowVM.RoomEditWindowType.Edit;
            roomEditWindow.DataContext = vm;
            roomEditWindow.ShowDialog();
        }
        #endregion
        public async void GetAllRoom()
        {
            if (roomBusiness != null)
            {
                List<Room> roomTypes = await roomBusiness.GetRooms(null, null, null);
                Rooms.Clear();
                roomTypes.ForEach(roomtype =>
                {
                    Rooms.Add(roomtype);
                });

            }
        }
    }
}

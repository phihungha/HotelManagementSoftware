using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class ChooseRoomWindowVM
    {
        public ObservableCollection<Room> Rooms { get; set; }
        private RoomBusiness? roomBusiness;
        public Room SelectedRoom { get; set; }
        public RoomType SelectedRoomType { get; set; }
        public ChooseRoomWindowVM(RoomBusiness? roomBusiness)
        {
            this.roomBusiness = roomBusiness;
            Rooms = new ObservableCollection<Room>();
            GetUsableRoom();
        }
        public async void GetUsableRoom()
        {
            if (roomBusiness != null)
            {
                List<Room> rooms = await roomBusiness.GetUsableRooms(SelectedRoomType.Name, 1, DateTime.Now, DateTime.Now.AddYears(1));
                Rooms.Clear();
                rooms.ForEach(roomtype =>
                {
                    Rooms.Add(roomtype);
                });

            }
        }
    }
}

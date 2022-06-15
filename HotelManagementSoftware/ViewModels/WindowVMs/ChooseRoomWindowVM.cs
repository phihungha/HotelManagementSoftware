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
        private RoomTypeBusiness? roomTypeBusiness;
        private FloorBusiness? floorBusiness;
        public Room SelectedRoom { get; set; }
        public RoomType SelectedRoomType { get; set; }
        public ChooseRoomWindowVM(RoomBusiness? roomBusiness, RoomTypeBusiness? roomTypeBusiness, FloorBusiness? floorBusiness)
        {
            this.roomBusiness = roomBusiness;
            this.roomTypeBusiness = roomTypeBusiness;
            this.floorBusiness = floorBusiness;
            Rooms = new ObservableCollection<Room>();
            
        }
        public async void GetUsableRoom(int id)
        {
           
              
                RoomType roomType = await roomTypeBusiness.GetRoomTypeById(id);
                this.SelectedRoomType = roomType;
                if (roomBusiness != null)
                {
                    // List<Room> rooms = await roomBusiness.GetUsableRooms(SelectedRoomType.Name, await floorBusiness.GetMaxFloorNumber(), DateTime.Now, DateTime.Now.AddYears(1));
                    List<Room> rooms = await roomBusiness.GetUsableRooms(DateTime.Now, DateTime.Now.AddYears(1), SelectedRoomType.Name);
                Rooms.Clear();
                rooms.ForEach(room =>
                    {
                        Rooms.Add(room);
                    });

                }
            
            
        }
        public async void LoadRooms(DateTime Arrivaltime, DateTime Departuretime)
        {
            if (roomBusiness == null) return;
            Rooms.Clear();
            List<Room> rooms = await roomBusiness.GetUsableRooms(Arrivaltime, Departuretime, null);
            rooms.ForEach(roomtype => Rooms.Add(roomtype));
        }
    }
}

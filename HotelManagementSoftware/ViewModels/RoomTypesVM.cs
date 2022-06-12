using System.Collections.Generic;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.Business;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HotelManagementSoftware.ViewModels
{
    public class RoomTypesVM: ObservableValidator
    {
        private RoomTypeBusiness roomTypeBusiness;

        public ObservableCollection<RoomType> RoomTypes { get; } = new();

        public RoomTypesVM(RoomTypeBusiness roomTypeBusiness)
        {
            this.roomTypeBusiness = roomTypeBusiness;
            LoadAllRoomTypes();
        }

        public async void LoadAllRoomTypes()
        {
            RoomTypes.Clear();
            List<RoomType> roomTypes = await roomTypeBusiness.GetRoomTypes();
            roomTypes.ForEach(roomtype => RoomTypes.Add(roomtype));
        }
    }
}

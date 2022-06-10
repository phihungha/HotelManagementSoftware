using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.Business;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels.WindowVMs;
using Microsoft.Toolkit.Mvvm.Input;
using HandyControl.Controls;
using static HotelManagementSoftware.ViewModels.WindowVMs.RoomTypeEditWindowVM;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HotelManagementSoftware.ViewModels
{
    public class RoomTypesVM: ObservableValidator
    {
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        private RoomTypeBusiness? roomTypeBusiness;
        public RoomType SelectedRoomType { get; set; }
        public RoomTypesVM(RoomTypeBusiness? _roomTypeBusiness)
        {
            roomTypeBusiness = _roomTypeBusiness;
            RoomTypes = new ObservableCollection<RoomType>();
            GetAllRoomType();
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
            RoomTypeEditWindow roomTypeEditWindow = new RoomTypeEditWindow();
            RoomTypeEditWindowVM vm = new RoomTypeEditWindowVM(roomTypeBusiness);
            vm.RoomTypesVM = this;
            vm.type = RoomTypeEditWindowType.Add;
            roomTypeEditWindow.DataContext = vm;
            roomTypeEditWindow.ShowDialog();
        }
        public void executeDeleteAction()
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("Delete this employee", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (roomTypeBusiness != null && SelectedRoomType != null)
                {
                    roomTypeBusiness.RemoveRoomType(SelectedRoomType);
                }
                GetAllRoomType();
            }
        }
        public void executeEditAction()
        {
            RoomTypeEditWindow roomTypeEditWindow = new RoomTypeEditWindow();
            RoomTypeEditWindowVM vm = new RoomTypeEditWindowVM(roomTypeBusiness);
            vm.RoomTypesVM = this;
            vm.type = RoomTypeEditWindowType.Edit;
            roomTypeEditWindow.DataContext = vm;
            roomTypeEditWindow.ShowDialog();
        }
        #endregion
        public async void GetAllRoomType()
        {
            if (roomTypeBusiness != null)
            {
                List<RoomType> roomTypes = await roomTypeBusiness.GetRoomTypes(null, null, null, null, null);
                RoomTypes.Clear();
                roomTypes.ForEach(roomtype =>
                {
                    RoomTypes.Add(roomtype);
                });

            }
        }
    }
}

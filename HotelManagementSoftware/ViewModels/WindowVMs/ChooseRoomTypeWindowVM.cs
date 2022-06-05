using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class ChooseRoomTypeWindowVM
    {
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ChooseRoomTypeWindowVM()
        {
            RoomTypes = new ObservableCollection<RoomType>();
        }
        #region command

        #endregion
    }
}

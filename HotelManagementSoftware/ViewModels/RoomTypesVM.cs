using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HotelManagementSoftware.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.Input;
using HandyControl.Controls;

namespace HotelManagementSoftware.ViewModels
{
    public class RoomTypesVM
    {
        public ObservableCollection<RoomType> RoomTypes { get; set; }

        public RoomTypesVM()
        {
            RoomTypes = new ObservableCollection<RoomType>();
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
            MessageBox.Show("Add");
        }
        public void executeDeleteAction()
        {
            MessageBox.Show("Delete");
        }
        public void executeEditAction()
        {
            MessageBox.Show("Edit");
        }
        #endregion
    }
}

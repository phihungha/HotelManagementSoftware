using HandyControl.Controls;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class RoomsVM
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public RoomsVM()
        {
            Rooms = new ObservableCollection<Room>();
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

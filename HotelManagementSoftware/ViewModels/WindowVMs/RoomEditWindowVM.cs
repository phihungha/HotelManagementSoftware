using HandyControl.Controls;
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

        private string? note;
        #endregion
        #region property validation
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
        public RoomEditWindowVM()
        {
            CommandCancel = new RelayCommand(executeCancelAction);
            CommandSave = new RelayCommand(executeCancelAction);
        }

        #region command
        public ICommand CommandCancel { get; }
        public ICommand CommandSave { get; }

        public void executeSaveAction()
        {
            MessageBox.Show("Update");
        }
        public void executeCancelAction()
        {
            MessageBox.Show("Cancel");
        }
        #endregion
    }
}

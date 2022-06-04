using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class RoomTypeEditWindowVM : ObservableValidator
    {
        #region private variables
        private int rate;
        private int capacity;
        private string name;
        private string? description;
        #endregion
        #region property validation
        [Required(ErrorMessage = "Rate cannot be empty")]
        public int Rate
        {
            get => rate;
            set => SetProperty(ref rate, value, true);
        }
        [Required(ErrorMessage = "Capacity cannot be empty")]
        public int Capacity
        {
            get => capacity;
            set => SetProperty(ref capacity, value, true);
        }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, true);
        }
        public string? Description
        {
            get => description;
            set => SetProperty(ref description, value, true);
        }
        #endregion
        public int NumOfRooms { get; set; }
        public RoomTypeEditWindowVM()
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

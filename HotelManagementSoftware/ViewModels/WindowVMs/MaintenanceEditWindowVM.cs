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
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class MaintenanceEditWindowVM : ObservableValidator
    {
        #region private variables
        private int issueID;
        private int roomID;
        private DateTime startTime;
        private DateTime endTime;
        private DateTime closeTime;
        private MaintenanceRequestStatus status;
        private string? note;

        private string? itemName;
        private int itemQuantity;
        private string? itemDescription;
        #endregion

        #region property validation
        public int IssueID
        {
            get => issueID;
            set => SetProperty(ref issueID, value, true);
        }

        [Required(ErrorMessage = "Room cannot be empty")]
        public int RoomID
        {
            get => roomID;
            set => SetProperty(ref roomID, value, true);
        }

        [Required(ErrorMessage = "Start time cannot be empty")]
        public DateTime StartTime
        {
            get => startTime;
            set
            {
                SetProperty(ref startTime, value, true);
                ValidateProperty(EndTime, nameof(EndTime));
            }
        }

        [Required(ErrorMessage = "End time cannot be empty")]
        [GreaterThan(nameof(StartTime),"End date should come after start date.")]
        public DateTime EndTime
        {
            get => endTime;
            set
            {
                SetProperty(ref endTime, value, EndTime>StartTime);
            }
        }

        public DateTime CloseTime
        {
            get => closeTime;
            set => SetProperty(ref closeTime, value, true);
        }

        [Required(ErrorMessage = "Status time cannot be empty")]
        public MaintenanceRequestStatus Status
        {
            get => status;
            set => SetProperty(ref status, value, true);
        }

        public string? Note
        {
            get => note;
            set => SetProperty(ref note, value, true);
        }

        [Required(ErrorMessage = "Item name cannot be empty")]
        public string? ItemName
        {
            get => itemName;
            set => SetProperty(ref itemName, value, true);
        }

        public string? ItemDescription
        {
            get => itemDescription;
            set => SetProperty(ref itemDescription, value, true);
        }
        [Required(ErrorMessage = "Item quantity cannot be empty")]
        public int ItemQuantity
        {
            get => itemQuantity;
            set => SetProperty(ref itemQuantity, value, true);
        }
        #endregion

        public ObservableCollection<Item> Items { get; set; }

        #region ctor
        public MaintenanceEditWindowVM()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item("1Lightbulb", 3, "20W lightbulb"));
            Items.Add(new Item("2TV", 1, "Samsung TV"));
            Items.Add(new Item("3Lightbulb", 3, "20W lightbulb"));
            Items.Add(new Item("4Lightbulb", 3, "20W lightbulb"));
            Items.Add(new Item("5TV", 1, "Samsung TV"));
            Items.Add(new Item("6TV", 1, "Samsung TV"));

            IssueID = 1;
            RoomID = 404;
            StartTime = new DateTime(2020, 1, 1);
            EndTime = new DateTime(2019, 1, 2);
            CloseTime = new DateTime(2020, 1, 3);
            Status = MaintenanceRequestStatus.Opened;
            Note = "note";
            ItemName = "Item name";
            ItemQuantity = 2;
            ItemDescription = "Description";

            CommandAddNewItem = new RelayCommand(executeAddItemAction, canAddItem);
            CommandCancel = new RelayCommand(executeCancelAction);
            CommandUpdate = new RelayCommand(executeUpdateAction, canUpdate);
            CommandAddImage = new RelayCommand(executeAddImageAction);
        }

        #endregion

        #region command
        public ICommand CommandAddImage { get; }
        public ICommand CommandAddNewItem { get; }
        public ICommand CommandCancel { get; }
        public ICommand CommandUpdate { get; }


        public bool canUpdate()
        {
            return true;
        }

        public bool canAddItem()
        {
            return true;
        }

        public void executeAddImageAction()
        {
            MessageBox.Show("Add image");
        }
        public void executeUpdateAction()
        {
            MessageBox.Show("Update");
        }
        public void executeCancelAction()
        {
            MessageBox.Show("Cancel");
        }
        public void executeAddItemAction()
        {
            MessageBox.Show("Add item");
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class GreaterThanAttribute : ValidationAttribute
    {
        private string _errorMessage;

        public GreaterThanAttribute(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            _errorMessage = errorMessage;
        }

        public string PropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var instance = validationContext.ObjectInstance;

            var otherValue = instance.GetType()
                .GetProperty(PropertyName)
                .GetValue(instance);

            if (((IComparable)value).CompareTo(otherValue) > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(_errorMessage);
        }
    }
    public class Item
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }

        public Item(string? name, int quantity, string? description)
        {
            Name = name;
            Quantity = quantity;
            Description = description;
        }
    }
}

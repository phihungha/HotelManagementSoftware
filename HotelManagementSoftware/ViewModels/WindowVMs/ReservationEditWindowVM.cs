using HandyControl.Controls;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class ReservationEditWindowVM
    {
        //Guest info
        public string CMND { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }

        //Room info
        public int RoomNumber { get; set; }
        public RoomType? RoomType { get; set; }
        public string? Note { get; set; }
        public int Floor { get; set; }
        public int TotalPayment { get; set; }
        // Stay info
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int NumOfDay { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        //Payment info
        public PaymentMethod[] PaymentMethod { get; set; }
        public PaymentMethod SelectedPaymentMethod { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public ReservationEditWindowVM()
        {
            CommandChooseRoom = new RelayCommand(executeChooseRoom);
            CommandChooseCustomer = new RelayCommand(executeChooseCustomer);
            CommandChooseRoomType = new RelayCommand(executeChooseRoomType);
        }
        #region command
        public ICommand CommandChooseRoom { get; }
        public ICommand CommandChooseCustomer { get; }
        public ICommand CommandChooseRoomType { get; }

        public void executeChooseRoom()
        {
            MessageBox.Show("Choose");
        }
        public void executeChooseCustomer()
        {
            MessageBox.Show("Choose");
        }
        public void executeChooseRoomType()
        {
            MessageBox.Show("Choose");
        }
        #endregion
    }
}

using HandyControl.Controls;
using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class ReservationEditWindowVM : ObservableValidator
    {
        private ReservationBusiness? reservationBusiness;
        private RoomBusiness? roomBusiness;
        private CustomerBusiness? customerBusiness;
        private EmployeeBusiness? employeeBusiness;
        private Customer? customer;
        private Reservation? reservation1;
        private Room? room;
        public ReservationEditWindowType reservationEditWindowType { get; set; }

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
        public decimal TotalPayment { get; set; }
        // Stay info
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int NumOfDay { get; set; }
        public int Person { get; set; }

        //Payment info
        public PaymentMethod[] PaymentMethod { get; set; }
        public PaymentMethod SelectedPaymentMethod { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public ReservationEditWindowVM(RoomBusiness? roomBusiness, CustomerBusiness? customerBusiness, ReservationBusiness? reservationBusiness, EmployeeBusiness? employeeBusiness)
        {
            this.roomBusiness = roomBusiness;
            this.customerBusiness = customerBusiness;
            this.reservationBusiness = reservationBusiness;
            this.employeeBusiness = employeeBusiness;
        }
        public async void LoadReservationFromId(int reservationId)
        {
            Reservation? reservation = await reservationBusiness.GetReservationById(reservationId);
            this.reservation1 = reservation;
            this.room = reservation.Room;
            LoadRoomFromId(reservation.Room.RoomId);
            this.customer = reservation.Customer;
            LoadCustomerFromId(reservation.Customer.CustomerId);
            Person = reservation.NumberOfPeople;
            ArrivalTime = reservation.ArrivalTime;
            DepartureTime = reservation.DepartureTime;
            TotalPayment = reservationBusiness.GetTotalRentFee(reservation);
        }
        public async void LoadRoomFromId(int RoomId)
        {
            Room? room = await roomBusiness.GetRoomById(RoomId);
            this.room = room;
            RoomNumber = room.RoomNumber;
            RoomType = room.RoomType;
            Note = room.Note;
            Floor = room.Floor;
        }
        public async void LoadCustomerFromId(int customerId)
        {
            Customer? customer = await customerBusiness.GetCustomerById(customerId);
            this.customer = customer;
            CMND = customer.IdNumber;
            Name = customer.Name;
            Gender = customer.Gender;
            BirthDate = customer.BirthDate;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            Address = customer.Address;
            SelectedPaymentMethod = customer.PaymentMethod;
            ExpireDate = customer.ExpireDate;
            CardNumber = customer.CardNumber;
        }
        #region command
        public ICommand CommandSave { get; }
        public ICommand CommandCancel { get; }

        public async Task<bool> Save()
        {
            if(reservation1 != null)
            {
                reservation1.ArrivalTime = ArrivalTime;
                reservation1.DepartureTime = DepartureTime;
                reservation1.NumberOfPeople = Person;
                reservation1.Room = room;
                reservation1.Customer = customer;
                reservationBusiness.EditReservation(reservation1);
                return true;
            }
            else
            {
                Reservation reservation = new Reservation(ArrivalTime, DepartureTime, Person, room, customer, employeeBusiness.CurrentEmployee);
                var result = HandyControl.Controls.MessageBox.Show(
    "Are you sure that you want to check in this Reservation now? This action cannot be undone.",
    "check in this Reservation?",
    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    reservationBusiness.CreateReservation(reservation,true);
                }else
                {
                    reservationBusiness.CreateReservation(reservation, false);
                }
                return true;  
            }
        }

        
        public Action CloseAction { get; set; }
        #endregion
        public enum ReservationEditWindowType
        {
            Add, Edit
        }
    }
}

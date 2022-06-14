using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelManagementSoftware.Business;
using System.Collections.ObjectModel;
using HotelManagementSoftware.ViewModels.Validators;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class CheckinWindowVM : ObservableValidator
    {
      
        private ReservationBusiness? reservationBusiness;
        private RoomBusiness? roomBusiness;
        private CustomerBusiness? customerBusiness;
        private EmployeeBusiness? employeeBusiness;
        private Customer? customer;
        private Reservation? reservation;
        private Room? room;

        private string cmnd;
        private string name;
        private Gender gender;
        private string phoneNumber;
        private DateTime birthDate;
        private string? email;
        private string address;
        private int roomNumber;
        private RoomType? roomType;
        private string? note;
        private int floor;
        private decimal totalPayment;
        private DateTime arrivalTime;
        private DateTime departureTime;
        private int numOfDay;
        private int person;
        private PaymentMethod[] paymentMethod;
        private PaymentMethod selectedPaymentMethod;
        private string? cardNumber;
        private DateTime? expireDate;

        public string CMND
        {
            get => cmnd;
            set
            {
                SetProperty(ref cmnd, value, true);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value, true);
            }
        }
        public Gender Gender
        {
            get => gender;
            set
            {
                SetProperty(ref gender, value, true);
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                SetProperty(ref phoneNumber, value, true);
            }
        }
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                SetProperty(ref birthDate, value, true);
            }
        }
        public string? Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value, true);
            }
        }
        public string Address
        {
            get => address;
            set
            {
                SetProperty(ref address, value, true);
            }
        }

        public int RoomNumber
        {
            get => roomNumber;
            set
            {
                SetProperty(ref roomNumber, value, true);
            }
        }
        public RoomType? RoomType
        {
            get => roomType;
            set
            {
                SetProperty(ref roomType, value, true);
            }
        }
        public string? Note
        {
            get => note;
            set
            {
                SetProperty(ref note, value, true);
            }
        }
        public int Floor
        {
            get => floor;
            set
            {
                SetProperty(ref floor, value, true);
            }
        }
        public decimal TotalPayment
        {
            get => totalPayment;
            set
            {
                SetProperty(ref totalPayment, value, true);
            }
        }

        public DateTime ArrivalTime
        {
            get => arrivalTime;
            set
            {
                SetProperty(ref arrivalTime, value, true);
            }
        }
        public DateTime DepartureTime
        {
            get => departureTime;
            set
            {
                SetProperty(ref departureTime, value, true);
            }
        }
        public int NumOfDay
        {
            get => numOfDay;
            set
            {
                SetProperty(ref numOfDay, value, true);
            }
        }
        public int Person
        {
            get => person;
            set
            {
                SetProperty(ref person, value, true);
            }
        }
        public CheckinWindowVM(CustomerBusiness customerBusiness,
                                    ReservationBusiness reservationBusiness, RoomBusiness roomBusiness,EmployeeBusiness employeeBusiness)
        {
            this.roomBusiness = roomBusiness;
            this.customerBusiness = customerBusiness;
            this.reservationBusiness = reservationBusiness;
            this.employeeBusiness = employeeBusiness;
        }


        public async void LoadReservationFromId(int reservationId)
        {
            Reservation? reservation = await reservationBusiness.GetReservationById(reservationId);
            this.reservation = reservation;
            this.room = reservation.Room;
            LoadRoomFromId(reservation.Room.RoomId);
            this.customer = reservation.Customer;
            LoadCustomerFromId(reservation.Customer.CustomerId);
            Person = reservation.NumberOfPeople;
            ArrivalTime = reservation.ArrivalTime;
            DepartureTime = reservation.DepartureTime;
            TotalPayment = reservationBusiness.GetTotalRentFee(reservation);
            TimeSpan stayPeriod = reservation.DepartureTime - reservation.ArrivalTime;
            NumOfDay = (int)Math.Ceiling(stayPeriod.TotalDays);
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
            //SelectedPaymentMethod = customer.PaymentMethod;
            //ExpireDate = customer.ExpireDate;
            //CardNumber = customer.CardNumber;
        }


    }
}

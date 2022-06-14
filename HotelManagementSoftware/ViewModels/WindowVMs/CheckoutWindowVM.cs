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
    public class CheckoutWindowVM : ObservableValidator
    {
        private ReservationBusiness reservationBusiness;
        private RoomBusiness? roomBusiness;
        private CustomerBusiness customerBusiness;
        private Reservation? reservation;
        private Customer? customer;
        private Room? room;


        public CheckoutWindowVM(CustomerBusiness customerBusiness,
                                    ReservationBusiness reservationBusiness, RoomBusiness roomBusiness)
        {
            this.customerBusiness = customerBusiness;
            this.reservationBusiness = reservationBusiness;
            this.roomBusiness = roomBusiness;
        }

        public string CMND { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }

        //Room information
        public int RoomNumber { get; set; }
        public RoomType? RoomType { get; set; }
        public string? Note { get; set; }
        public int Floor { get; set; }
        public decimal TotalPayment { get; set; }

        //Payment information
        public PaymentMethod[] PaymentMethod { get; set; }
        public PaymentMethod SelectedPaymentMethod { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }


        // Stay information
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int NumOfDay { get; set; }
        public int Person { get; set; }



    }
}


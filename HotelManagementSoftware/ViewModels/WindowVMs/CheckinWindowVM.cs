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
        private ReservationBusiness reservationBusiness;
        private CustomerBusiness customerBusiness;
        private Reservation? reservation;

        private string idNumber;
        private IdNumberType idNumberType = IdNumberType.Cmnd;
        private string name;
        private Gender gender = Gender.Male;
        private DateTime birthDate = new DateTime(1970, 1, 1);
        private string phoneNumber;
        private string? email;
        private string address;
        private PaymentMethod paymentMethod = PaymentMethod.Cash;
        private string? cardNumber;
        private DateTime? expireDate;



        public CheckinWindowVM(CustomerBusiness customerBusiness,
                                    ReservationBusiness reservationBusiness)
        {
            this.customerBusiness = customerBusiness;
            this.reservationBusiness = reservationBusiness;
        }



    }
}

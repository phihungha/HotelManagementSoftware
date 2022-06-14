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


        private bool isUsingBankCard = false;
        public bool IsUsingBankCard
        {
            get => isUsingBankCard;
            set
            {
                SetProperty(ref isUsingBankCard, value);
                if (value == true)
                {
                    CardNumber = "";
                    ExpireDate = DateTime.Now.Date.AddYears(5);
                }
                else
                {
                    CardNumber = "";
                    ExpireDate = null;
                }
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, true);
        }

        public IdNumberType IdNumberType
        {
            get => idNumberType;
            set => SetProperty(ref idNumberType, value, true);
        }


        public string IdNumber
        {
            get => idNumber;
            set => SetProperty(ref idNumber, value, true);
        }

        public Gender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value, true);
        }


        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value, true);
        }

        public string? Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value, true);
        }


        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value, true);
        }

        public PaymentMethod PaymentMethod
        {
            get => paymentMethod;
            set
            {
                SetProperty(ref paymentMethod, value, true);
                if (value != PaymentMethod.Cash)
                    IsUsingBankCard = true;
                else
                    IsUsingBankCard = false;
            }
        }

        public string? CardNumber
        {
            get => cardNumber;
            set => SetProperty(ref cardNumber, value, true);
        }

        public DateTime MinimumExpireDate
        {
            get => DateTime.Now.Date.AddDays(7);
        }

        public DateTime? ExpireDate
        {
            get => expireDate;
            set => SetProperty(ref expireDate, value, true);
        }

        public CheckinWindowVM(CustomerBusiness customerBusiness,
                                    ReservationBusiness reservationBusiness)
        {
            this.customerBusiness = customerBusiness;
            this.reservationBusiness = reservationBusiness;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class CustomerEditWindowVM : ObservableValidator
    {
        private string idNumber;
        private string name;
        private Gender gender;
        private DateTime birthDate;
        private string phoneNumber;
        private string? email;
        private string address;
        private string city;
        private string province;
        private Country country;

        [Required(ErrorMessage = "Name cannot be empty")]
        [MinLength(2, ErrorMessage = "Name cannot be shorter than 2 character")]
        [MaxLength(100, ErrorMessage = "Name should be shorter than 100 character")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, true);
        }

        [Required(ErrorMessage = "ID cannot be empty")]
        [RegularExpression(@"^(\d{9}|\d{12})$", ErrorMessage = "Invalid ID")]
        public string IdNumber
        {
            get => idNumber;
            set => SetProperty(ref idNumber, value, true);
        }


        [Required(ErrorMessage = "Gender cannot be empty")]
        public Gender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value, true);
        }

        [Required(ErrorMessage = "Phone cannot be empty")]
        [RegularExpression(@"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Invalid phone")]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value, true);
        }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email")]
        public string? Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        [Required(ErrorMessage = "Address cannot be empty")]
        [RegularExpression(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$", ErrorMessage = "Invalid address")]
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value, true);
        }

        [Required(ErrorMessage = "City cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$", ErrorMessage = "Invalid city")]
        public string City
        {
            get => city;
            set => SetProperty(ref city, value, true);
        }


        [Required(ErrorMessage = "Province/State cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$", ErrorMessage = "Invalid Province/State")]
        public string Province
        {
            get => province;
            set => SetProperty(ref province, value, true);
        }


        [Required(ErrorMessage = "Country cannot be empty")]
        public Country Country
        {
            get => country;
            set => SetProperty(ref country, value, true);
        }

        [Required(ErrorMessage = "Date of birth cannot be empty")]
        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value, true);
        }


    }
}

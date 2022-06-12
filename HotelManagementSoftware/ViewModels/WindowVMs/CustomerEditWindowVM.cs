using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelManagementSoftware.Business;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class CustomerEditWindowVM : ObservableValidator
    {
        private CustomerBusiness customerBusiness;
        private CountryBusiness countryBusiness;

        private Customer? customer;

        private bool editMode = false;

        public bool EditMode
        {
            get => editMode;
            set => SetProperty(ref editMode, value);
        }

        private string idNumber = "";
        private IdNumberType idNumberType = IdNumberType.Cmnd;
        private string name = "";
        private Gender gender = Gender.Male;
        private DateTime birthDate;
        private string phoneNumber = "";
        private string? email = "";
        private string address = "";
        private string city = "";
        private string province = "";
        private Country country = new Country("", "");

        [Required(ErrorMessage = "Name cannot be empty")]
        [MinLength(2, ErrorMessage = "Name cannot be shorter than 2 character")]
        [MaxLength(100, ErrorMessage = "Name should be shorter than 100 character")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, true);
        }

        [Required(ErrorMessage = "Identity number cannot be empty")]
        [RegularExpression(@"^(\d{9}|\d{12})$", ErrorMessage = "Invalid Identity number")]
        public string IdNumber
        {
            get => idNumber;
            set => SetProperty(ref idNumber, value, true);
        }

        public IdNumberType IdNumberType
        {
            get => idNumberType;
            set => SetProperty(ref idNumberType, value, true);
        }

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

        public ObservableCollection<Country> Countries { get; } = new();

        public Country Country
        {
            get => country;
            set => SetProperty(ref country, value, true);
        }

        public DateTime MinimumBirthDate
        {
            get => DateTime.Now.AddYears(-18);
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value, true);
        }

        public CustomerEditWindowVM(CustomerBusiness customerBusiness, CountryBusiness countryBusiness)
        {
            this.customerBusiness = customerBusiness;
            this.countryBusiness = countryBusiness;

            Populate();
        }

        private async void Populate()
        {
            List<Country> result = await countryBusiness.GetAllCountries();
            result.ForEach(i => Countries.Add(i));
            Country = result.First(i => i.CountryCode == "VN");
        }

        public async void LoadCustomerFromId(int customerId)
        {
            Customer? customer = await customerBusiness.GetCustomerById(customerId);

            if (customer == null)
                return;

            this.customer = customer;
            EditMode = true;

            IdNumber = customer.IdNumber;
            IdNumberType = customer.IdNumberType;
            Name = customer.Name;
            Gender = customer.Gender;
            BirthDate = customer.BirthDate;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            Address = customer.Address;
            City = customer.City;
            Province = customer.Province;
            Country = customer.Country ?? Country;
        }

        public async Task<bool> SaveCustomer()
        {
            ValidateAllProperties();
            if (GetErrors().Count() != 0)
                return false;

            if (customer != null)
            {
                customer.IdNumber = IdNumber;
                customer.IdNumberType = IdNumberType;
                customer.Name = Name;
                customer.Gender = Gender;
                customer.BirthDate = BirthDate;
                customer.PhoneNumber = PhoneNumber;
                customer.Email = Email;
                customer.Address = Address;
                customer.City = City;
                customer.Province = Province;
                customer.Country = Country;

                await customerBusiness.EditCustomer(customer);
            }
            else
            {
                var newCustomer = new Customer(Name,
                                               BirthDate,
                                               IdNumberType,
                                               IdNumber,
                                               Gender,
                                               PhoneNumber,
                                               Address,
                                               City,
                                               Province,
                                               Country);
                await customerBusiness.CreateCustomer(newCustomer);
            }
            return true;
        }

        public async Task DeleteCustomer()
        {
            if (customer != null)
                await customerBusiness.DeleteCustomer(customer);
        }
    }
}

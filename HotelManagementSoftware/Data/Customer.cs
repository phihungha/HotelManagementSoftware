using System;
using System.Collections.Generic;

namespace HotelManagementSoftware.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IdNumberType IdNumberType { get; set; }

        public string IdNumber { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public Country? Country { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }

        public List<Reservation> Reservations { get; } = new();

        public Customer(string firstName,
                        string lastName,
                        IdNumberType idNumberType,
                        string idNumber,
                        Gender gender,
                        string phoneNumber,
                        string address,
                        string city,
                        string province,
                        PaymentMethod paymentMethod)
        {
            FirstName = firstName;
            LastName = lastName;
            IdNumber = idNumber;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            Province = province;
            PaymentMethod = paymentMethod;
            IdNumberType = idNumberType;
        }
    }

    public enum PaymentMethod
    {
        Cash,
        Visa,
        Mastercard
    }

    public enum IdNumberType
    {
        Cmnd,
        Passport
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class Country
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public List<Customer> Reservations { get; set; } = new();

        public Country(string name)
        {
            Name = name;
        }
    }
}

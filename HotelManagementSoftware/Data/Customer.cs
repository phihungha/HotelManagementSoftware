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

        public string Gender { get; set; }

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
                        string idNumber,
                        string gender,
                        string phoneNumber,
                        string address,
                        string city,
                        string province,
                        PaymentMethod paymentMethod,
                        IdNumberType idNumberType)
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
        CASH,
        VISA,
        MASTERCARD
    }

    public enum IdNumberType
    {
        CMND,
        PASSPORT
    }

    public class Country
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public List<Customer> Reservations { get; } = new();

        public Country(string name)
        {
            Name = name;
        }
    }
}

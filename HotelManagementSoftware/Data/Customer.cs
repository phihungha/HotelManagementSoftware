using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public IdNumberType? IdNumberType { get; set; }

        [Required]
        public string IdNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        public Country? Country { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public string CardNumber { get; set; }
        public DateTime ExpireDate { get; set; }

        public List<Reservation> Reservations { get; } = new();
    }

    public class IdNumberType
    {
        public int IdNumberTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Customer> Reservations { get; } = new();
    }

    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Customer> Reservations { get; } = new();
    }

    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Customer> Reservations { get; } = new();
    }
}

using HotelManagementSoftware.Data;
using HotelManagementSoftware.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class CustomerBusiness
    {
        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <returns>List of customers</returns>
        public async Task<List<Customer>> GetAllCustomers()
        {
            using (var db = new Database())
            {
                return await db.Customers.Include(i => i.Country).ToListAsync();
            }
        }

        /// <summary>
        /// Create a customer.
        /// </summary>
        /// <param name="customer">Customer</param>
        public async void CreateCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            using (var db = new Database())
            {
                if (customer.Country == null)
                    return;
                db.Attach(customer.Country);
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a customer.
        /// </summary>
        /// <param name="customer">Updated customer</param>
        public async void EditCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            using (var db = new Database())
            {
                db.Customers.Update(customer);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="customer">Customer to delete</param>
        public async void DeleteCustomer(Customer customer)
        {
            using (var db = new Database())
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Validate customer's information before adding or updating.
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <exception cref="ArgumentException">Validation failure</exception>
        public void ValidateCustomer(Customer customer)
        {
            if (customer.FirstName == "")
                throw new ArgumentException("First name cannot be empty");
            if (customer.LastName == "")
                throw new ArgumentException("Last name cannot be empty");
            if (customer.IdNumber == "")
                throw new ArgumentException("Id number cannot be empty");
            if (customer.BirthDate > DateTime.Now.AddYears(-18))
                throw new ArgumentException("Age cannot be less than 18 years old");
            if (!ValidationUtils.ValidatePhoneNumber(customer.PhoneNumber, "VN"))
                throw new ArgumentException("Phone number is invalid");
            if (customer.Address == "")
                throw new ArgumentException("Address cannot be empty");
            if (customer.City == "")
                throw new ArgumentException("City cannot be empty");
            if (customer.Province == "")
                throw new ArgumentException("Province cannot be empty");
            if (customer.Country == null)
                throw new ArgumentException("Country cannot be empty");
            if (customer.Email != null && !ValidationUtils.ValidateEmail(customer.Email))
                throw new ArgumentException("Email is invalid");
            if (customer.PaymentMethod != PaymentMethod.Cash)
            {
                if (customer.CardNumber == null || customer.CardNumber == "")
                    throw new ArgumentException("Card number cannot be empty if payment method needs a card");
                if (customer.ExpireDate == null)
                    throw new ArgumentException("Card expiration date cannot be empty");
                if (customer.ExpireDate <= DateTime.Now.Date)
                    throw new ArgumentException("Card's expiration date cannot be earlier than today");
            }
        }
    }
}

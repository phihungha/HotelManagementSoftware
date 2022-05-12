using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class CustomerBusiness
    {
        public async Task<List<Customer>> GetAllCustomers()
        {
            using (var db = new Database())
            {
                return await db.Customers.Include(i => i.Country).ToListAsync();
            }
        }

        public async void CreateNewCustomer(Customer customer)
        {
            using (var db = new Database())
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
            }
        }

        public async void EditCustomer(Customer customer)
        {
            using (var db = new Database())
            {
                db.Customers.Update(customer);
                await db.SaveChangesAsync();
            }
        }

        public async void DeleteCustomer(Customer customer)
        {
            using (var db = new Database())
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
            }
        }
    }
}

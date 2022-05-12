using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class EmployeeBusiness
    {
        public async Task<List<Employee>> GetAllCustomers()
        {
            using (var db = new Database())
            {
                return await db.Employees.Include(i => i.EmployeeType).ToListAsync();
            }
        }

        public async void CreateNewEmployee(Employee employee, string password)
        {
            using (var db = new Database())
            {
                employee.HashedPassword = GetHashedPassword(password);
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
            }
        }

        public async void EditEmployee(Employee employee)
        {
            using (var db = new Database())
            {
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
            }
        }

        public async void ChangePassword(Employee employee, string password)
        {
            using (var db = new Database())
            {
                employee.HashedPassword = GetHashedPassword(password);
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
            }
        }

        public async void DeleteEmployee(Employee employee)
        {
            using (var db = new Database())
            {
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> Login(string userName, string password)
        {
            using (var db = new Database())
            {
                Employee employee = await db.Employees.SingleAsync(i => i.UserName == userName);
                if (employee.HashedPassword == GetHashedPassword(password))
                    return true;
                return false;
            }
        }

        public string GetHashedPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}

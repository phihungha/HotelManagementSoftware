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
        public async Task<List<Employee>> GetAllEmloyees()
        {
            using (var db = new Database())
            {
                return await db.Employees.Include(i => i.EmployeeType).ToListAsync();
            }
        }

        public async void CreateEmployee(Employee employee, string password)
        {
            using (var db = new Database())
            {
                byte[] salt = GetNewSalt();
                employee.HashedPassword = GetHashedPassword(password, salt);
                employee.Salt = Convert.ToBase64String(salt);
                db.EmployeeTypes.Attach(employee.EmployeeType);
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
                byte[] salt = GetNewSalt();
                employee.HashedPassword = GetHashedPassword(password, salt);
                employee.Salt = Convert.ToBase64String(salt);
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
                byte[] salt = Convert.FromBase64String(employee.Salt);
                string hashedPassword = GetHashedPassword(password, salt);
                if (employee.HashedPassword == hashedPassword)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Get salt for password hasing.
        /// </summary>
        /// <returns>Salt string as byte array</returns>
        public byte[] GetNewSalt()
        {
            return RandomNumberGenerator.GetBytes(128 / 8);
        }

        /// <summary>
        /// Get hashed password from a password and salt.
        /// </summary>
        /// <param name="password">Plain text password to hash</param>
        /// <param name="salt">Salt</param>
        /// <returns></returns>
        public string GetHashedPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }

    public class EmployeeTypeBusiness
    {
        public async Task<List<EmployeeType>> GetAllEmployeeTypes()
        {
            using (var db = new Database())
                return await db.EmployeeTypes.ToListAsync();
        }
    }
}

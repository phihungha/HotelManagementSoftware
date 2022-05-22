using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HotelManagementSoftware.Utils;

namespace HotelManagementSoftware.Business
{
    public class EmployeeBusiness
    {
        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <returns>List of employees</returns>
        public async Task<List<Employee>> GetAllEmloyees()
        {
            using (var db = new Database())
            {
                /// Don't map password hash and salt.
                return await db.Employees.Include(i => i.EmployeeType)
                    .Select(i =>
                        new Employee(
                            i.FirstName,
                            i.LastName,
                            i.UserName,
                            i.Gender,
                            i.BirthDate,
                            i.PhoneNumber,
                            i.Address)
                        {
                            Email = i.Email,
                            EmployeeType = i.EmployeeType
                        }
                    ).ToListAsync();
            }
        }

        /// <summary>
        /// Create a new employee.
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <param name="password">Login password of the employee</param>
        public async void CreateEmployee(Employee employee, string password)
        {
            ValidateEmployee(employee);
            using (var db = new Database())
            {
                if (employee.EmployeeType == null)
                    return;
                db.EmployeeTypes.Attach(employee.EmployeeType);

                byte[] salt = GetNewSalt();
                employee.HashedPassword = GetHashedPassword(password, salt);
                employee.Salt = Convert.ToBase64String(salt);

                db.Employees.Add(employee);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit an employee.
        /// </summary>
        /// <param name="employee">Updated employee</param>
        public async void EditEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            using (var db = new Database())
            {
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Change employee's login password
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <param name="password">New password</param>
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

        /// <summary>
        /// Delete an employee.
        /// </summary>
        /// <param name="employee">Employee to delete</param>
        public async void DeleteEmployee(Employee employee)
        {
            using (var db = new Database())
            {
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Validate employee's information before adding or updating.
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <exception cref="ArgumentException">Validation failure</exception>
        public void ValidateEmployee(Employee employee)
        {
            if (employee.FirstName == "")
                throw new ArgumentException("First name cannot be empty");
            if (employee.LastName == "")
                throw new ArgumentException("Last name cannot be empty");
            if (employee.UserName == "")
                throw new ArgumentException("User name cannot be empty");
            if (employee.EmployeeType == null)
                throw new ArgumentException("Employee type cannot be null");
            if (employee.BirthDate > DateTime.Now.AddYears(-18))
                throw new ArgumentException("Age cannot be less than 18 years old");
            if (!ValidationUtils.ValidatePhoneNumber(employee.PhoneNumber, "VN"))
                throw new ArgumentException("Phone number is invalid");
            if (employee.Address == "")
                throw new ArgumentException("Address cannot be empty");
            if (employee.Email != null && !ValidationUtils.ValidateEmail(employee.Email))
                throw new ArgumentException("Email is invalid");
        }

        /// <summary>
        /// Login using a username and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns>True if login succeeds</returns>
        public async Task<bool> Login(string userName, string password)
        {
            using (var db = new Database())
            {
                Employee employee = await db.Employees.SingleAsync(i => i.UserName == userName);
                if (employee.Salt == null || employee.HashedPassword == null)
                    return false;
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
        /// <summary>
        /// Get all employee types.
        /// </summary>
        /// <returns>List of employee types</returns>
        public async Task<List<EmployeeType>> GetAllEmployeeTypes()
        {
            using (var db = new Database())
                return await db.EmployeeTypes.ToListAsync();
        }
    }
}

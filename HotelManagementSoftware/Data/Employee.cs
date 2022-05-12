using System;
using System.Collections.Generic;

namespace HotelManagementSoftware.Data
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public EmployeeType? EmployeeType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string Address { get; set; }

        public string HashedPassword { get; set; }

        public List<HousekeepingRequest> OpenedHousekeepingRequests { get; set; } = new();
        public List<HousekeepingRequest> ClosedHousekeepingRequests { get; set; } = new();
        public List<MaintenanceRequest> OpenedMaintenanceRequests { get; set; } = new();
        public List<MaintenanceRequest> ClosedMaintenanceRequests { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();

        public Employee(string firstName,
                        string lastName,
                        string userName,
                        Gender gender,
                        DateTime birthDate,
                        string phoneNumber,
                        string? email,
                        string address,
                        string hashedPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Gender = gender;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            HashedPassword = hashedPassword;
        }
    }

    public class EmployeeType
    {
        public int EmployeeTypeId { get; set; }

        public string Name { get; set; }

        public List<Employee> Employees { get; set; } = new();

        public EmployeeType(string name)
        {
            Name = name;
        }
    }
}

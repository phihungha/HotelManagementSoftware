using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        public string Name { get; set; }

        public EmployeeType? EmployeeType { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<HousekeepingRequest> OpenedHousekeepingRequests { get; set; } = new();
        public List<HousekeepingRequest> ClosedHousekeepingRequests { get; set; } = new();
        public List<MaintenanceRequest> OpenedMaintenanceRequests { get; set; } = new();
        public List<MaintenanceRequest> ClosedMaintenanceRequests { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
    }

    public class EmployeeType
    {
        public int EmployeeTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; } = new();
    }
}

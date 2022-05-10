using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class HousekeepingRequest
    {
        public int HousekeepingRequestId { get; set; }

        public int? OpenEmployeeId { get; set; }
        public Employee? OpenEmployee { get; set; }

        public int? CloseEmployeeId { get; set; }
        public Employee? CloseEmployee { get; set; }

        public Room? Room { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime CloseTime { get; set; }

        public string Note { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        public Room? Room { get; set; }

        public Customer? Customer { get; set; }

        public Employee? Employee { get; set; }

        public Order? Order { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public DateTime PayTime { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; }

        public int? ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
    }
}

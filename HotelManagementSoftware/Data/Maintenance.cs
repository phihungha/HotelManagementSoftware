using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class MaintenanceRequest
    {
        public int MaintenanceRequestId { get; set; }

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

        public List<MaintenanceItem> MaintenanceItems { get; set; } = new();
    }

    public class MaintenanceItem
    {
        public int MaintenanceItemId { get; set; }

        public int MaintenanceRequestId { get; set; }

        public MaintenanceRequest MaintenanceRequest { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

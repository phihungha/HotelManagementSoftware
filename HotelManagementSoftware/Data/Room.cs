using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class Room
    {
        public int RoomId { get; set; }

        public RoomType? RoomType { get; set; }

        [Required]
        public int Floor { get; set; }

        public string Note { get; set; }

        [Required]
        public string Status { get; set; }
    }

    public class RoomType
    {
        public int RoomTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int BedNumber { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal FullDayRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal HalfDayRate { get; set; }

        public List<Room> Rooms { get; set; } = new();
    }
}

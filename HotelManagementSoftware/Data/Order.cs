using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSoftware.Data
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime PayTime { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Amount { get; set; }

        public OrderStatus Status { get; set; }

        public int? ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public Order(DateTime creationTime, DateTime payTime, decimal amount, OrderStatus status)
        {
            CreationTime = creationTime;
            PayTime = payTime;
            Amount = amount;
            Status = status;
        }
    }

    public enum OrderStatus
    {
        PENDING,
        PAID
    }
}

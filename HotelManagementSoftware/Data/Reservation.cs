using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSoftware.Data
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime DepartureTime { get; set; }

        public Room? Room { get; set; }

        public Customer? Customer { get; set; }

        public Employee? Employee { get; set; }

        public Order? Order { get; set; }

        public ReservationStatus Status { get; set; }

        public Reservation(DateTime arrivalTime, DateTime departureTime, ReservationStatus status)
        {
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Status = status;
        }
    }

    public enum ReservationStatus
    {
        Reserved,
        CheckedIn,
        CheckedOut,
        Cancelled
    }

    public class ReservationCancelFeePercent
    {
        [Key]
        public DateTime DayBeforeArrival { get; set; }
        
        public double PercentOfTotal { get; set; }
    }
}

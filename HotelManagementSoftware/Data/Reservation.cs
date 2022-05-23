using System;
using System.Collections.Generic;
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

        public ReservationStatus Status { get; set; }

        public List<Order> Orders { get; } = new();

        public Reservation(DateTime arrivalTime, DateTime departureTime, ReservationStatus status)
        {
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Status = status;
        }

        public Reservation(DateTime arrivalTime,
                           DateTime departureTime,
                           Room room,
                           Customer customer,
                           Employee employee,
                           ReservationStatus status)
            : this(arrivalTime, departureTime, status)
        {
            Room = room;
            Customer = customer;
            Employee = employee;
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
        public int DayNumberBeforeArrival { get; set; }
        
        public int PercentOfTotal { get; set; }
    }
}

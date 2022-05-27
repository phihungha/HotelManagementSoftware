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

        public int NumberOfPeople { get; set; }

        public Room? Room { get; set; }

        public Customer? Customer { get; set; }

        public Employee? Employee { get; set; }

        public ReservationStatus Status { get; set; }

        public Order? Order { get; set; }

        public Reservation(DateTime arrivalTime, DateTime departureTime, int numberOfPeople, ReservationStatus status)
        {
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Status = status;
            NumberOfPeople = numberOfPeople;
        }

        public Reservation(DateTime arrivalTime,
                           DateTime departureTime,
                           int numberOfPeople,
                           Room room,
                           Customer customer,
                           Employee employee,
                           ReservationStatus status = ReservationStatus.Reserved)
            : this(arrivalTime, departureTime, numberOfPeople, status)
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

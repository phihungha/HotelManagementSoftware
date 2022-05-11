﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSoftware.Data
{
    public class Room
    {
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        public RoomType? RoomType { get; set; }

        public int Floor { get; set; }

        public string? Note { get; set; }

        public RoomStatus Status { get; set; }

        public Room(int roomNumber, int floor, RoomStatus status)
        {
            Floor = floor;
            Status = status;
            RoomNumber = roomNumber;
        }
    }

    public enum RoomStatus
    {
        EMPTY,
        IN_USE,
        IN_WORK,
        CLOSED
    }

    public class RoomType
    {
        public int RoomTypeId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int NumberOfPeople { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal FullDayRate { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal HalfDayRate { get; set; }

        public List<Room> Rooms { get; set; } = new();

        public RoomType(string name, int numberOfPeople, decimal fullDayRate, decimal halfDayRate)
        {
            Name = name;
            NumberOfPeople = numberOfPeople;
            FullDayRate = fullDayRate;
            HalfDayRate = halfDayRate;
        }
    }
}

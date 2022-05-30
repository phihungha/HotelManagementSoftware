﻿using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class RoomBusiness
    {
        /// <summary>
        /// Get usable rooms in provided stay period, room type, and floor.
        /// </summary>
        /// <param name="roomType">Room type name</param>
        /// <param name="floorNumber">Floor number</param>
        /// <param name="arrivalTime">Arrival time</param>
        /// <param name="departureTime">Departure time</param>
        /// <returns></returns>
        public async Task<List<Room>> GetUsableRooms(string roomType,
                                                     int floorNumber,
                                                     DateTime arrivalTime,
                                                     DateTime departureTime)
        {
            using (var db = new Database())
            {
                List<Room> rooms = await db.Rooms
                    .Include(i => i.RoomType)
                    .Include(i => i.Reservations)
                    .Where(i => i.Floor == floorNumber)
                    .Where(i => i.RoomType != null && i.RoomType.Name == roomType)
                    .ToListAsync();

                return rooms.Where(i => !i.Reservations.Any(
                            r => ReservationBusiness.CheckStayPeriodCollision(
                                    arrivalTime,
                                    r.ArrivalTime,
                                    departureTime,
                                    r.DepartureTime)
                            )
                        ).ToList();
            }
        }

        /// <summary>
        /// Get all rooms that satisfy specified criteria.
        /// <param name="floorNumber">Floor number</paramref>
        /// /// <param name="roomType">Room type name</paramref>
        /// <param name="status">Status</paramref>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetRooms(int? floorNumber = null, string? roomType = null, RoomStatus? status = null)
        {
            using (var db = new Database())
            {
                var request = db.Rooms.Include(i => i.RoomType);
                var filteredRequest = request.Where(i => true);

                if (floorNumber != null)
                    filteredRequest = filteredRequest.Where(i => i.Floor == floorNumber);

                if (roomType != null)
                    filteredRequest = filteredRequest.Where(
                        i => i.RoomType != null && i.RoomType.Name == roomType);

                if (status != null)
                    filteredRequest = filteredRequest.Where(i => i.Status == status);

                return await filteredRequest.ToListAsync();
            }
        }

        /// <summary>
        /// Add a new room.
        /// </summary>
        /// <param name="room">Room info</param>
        public async Task AddRoom(Room room)
        {
            ValidateRoom(room);
            using (var db = new Database())
            {
                if (room.RoomType == null)
                    throw new ArgumentException("Room type cannot be empty");
                db.Attach(room.RoomType);
                db.Add(room);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a room.
        /// </summary>
        /// <param name="room">New room's info</param>
        public async Task EditRoom(Room room)
        {
            ValidateRoom(room);
            using (var db = new Database())
            {
                db.Update(room);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Remove a room.
        /// </summary>
        /// <param name="room">Room to remove</param>
        public async Task RemoveRoom(Room room)
        {
            using (var db = new Database())
            {
                db.Remove(room);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Validate room's info before adding or updating.
        /// </summary>
        /// <param name="room">Room to validate</param>
        public void ValidateRoom(Room room)
        {
            if (room.RoomNumber < 0)
                throw new ArgumentException("Room number cannot be negative");
            if (room.RoomType == null)
                throw new ArgumentException("Room type cannot be null");
            if (room.Floor < 0)
                throw new ArgumentException("Floor number cannot be negative");
        }
    }

    public class RoomTypeBusiness
    {
        public async Task<RoomType?> GetRoomTypeById(int id)
        {
            using (var db = new Database())
            {
                return await db.RoomTypes
                    .Include(i => i.Rooms)
                    .FirstOrDefaultAsync(i => i.RoomTypeId == id);
            }
        }

        /// <summary>
        /// Get room types that satisfy specified criteria.
        /// Associated rooms are not included since this method is only used
        /// in list screens.
        /// </summary>
        /// <param name="nameSearchTerm">Search term for name</param>
        /// <param name="capacity">Maximum number of people</param>
        /// <param name="fromRate">Min rate</param>
        /// <param name="toRate">Max rate</param>
        /// <param name="descriptionSearchTerm">Search term for description</param>
        /// <returns></returns>
        public async Task<List<RoomType>> GetRoomTypes(
            string? nameSearchTerm = null,
            int? capacity = null,
            decimal? fromRate = null,
            decimal? toRate = null,
            string? descriptionSearchTerm = null)
        {
            using (var db = new Database())
            {
                var filteredRequest = db.RoomTypes.Where(i => true);

                if (nameSearchTerm != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Name.Contains(nameSearchTerm));

                if (descriptionSearchTerm != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Description != null && 
                                    i.Description.Contains(descriptionSearchTerm));

                if (capacity != null)
                    filteredRequest = filteredRequest.Where(i => i.Capacity == capacity);

                if (fromRate != null)
                    filteredRequest = filteredRequest.Where(i => i.Rate >= fromRate);

                if (toRate != null)
                    filteredRequest = filteredRequest.Where(i => i.Rate <= toRate);

                return await filteredRequest.ToListAsync();
            }
        }

        /// <summary>
        /// Add a new room type.
        /// </summary>
        /// <param name="roomType">Room type</param>
        public async Task AddRoomType(RoomType roomType)
        {
            ValidateRoomType(roomType);
            using (var db = new Database())
            {
                db.Add(roomType);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a room type.
        /// </summary>
        /// <param name="roomType">Updated room type</param>
        public async Task EditRoomType(RoomType roomType)
        {
            ValidateRoomType(roomType);
            using (var db = new Database())
            {
                db.Update(roomType);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Remove a room type.
        /// </summary>
        /// <param name="roomType">Room type to remove</param>
        public async Task RemoveRoomType(RoomType roomType)
        {
            using (var db = new Database())
            {
                db.Remove(roomType);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Validate a room type's info before adding or updating.
        /// </summary>
        /// <param name="roomType">Room type to validate</param>
        /// <exception cref="ArgumentException">Validation error</exception>
        public void ValidateRoomType(RoomType roomType)
        {
            if (roomType.Name == "")
                throw new ArgumentException("Name cannot be empty");
            if (roomType.Capacity < 1)
                throw new ArgumentException("Capacity cannot be smaller than 1");
            if (roomType.Rate <= 1)
                throw new ArgumentException("Rate cannot be less than 1");
        }
    }
}

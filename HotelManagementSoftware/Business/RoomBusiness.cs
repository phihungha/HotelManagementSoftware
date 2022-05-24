using HotelManagementSoftware.Data;
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
        /// Get all rooms.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetAllRooms()
        {
            using (var db = new Database())
            {
                return await db.Rooms.Include(i => i.RoomType).ToListAsync();
            }
        }

        /// <summary>
        /// Get all rooms that satisfy specified criteria.
        /// <param name="floorNumber">Floor number</paramref>
        /// <param name="status">Status</paramref>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetRooms(int floorNumber, RoomStatus status)
        {
            using (var db = new Database())
            {
                return await db.Rooms.Include(i => i.RoomType)
                    .Where(i => i.Floor == floorNumber)
                    .Where(i => i.Status == status)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Add a new room.
        /// </summary>
        /// <param name="room">Room info</param>
        public async void AddRoom(Room room)
        {
            ValidateRoom(room);
            using (var db = new Database())
            {
                if (room.RoomType != null)
                    db.Attach(room.RoomType);
                db.Add(room);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a room.
        /// </summary>
        /// <param name="room">New room's info</param>
        public async void EditRoom(Room room)
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
        public async void RemoveRoom(Room room)
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
        public async void AddRoomType(RoomType roomType)
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
        public async void EditRoomType(RoomType roomType)
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
        public async void RemoveRoomType(RoomType roomType)
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

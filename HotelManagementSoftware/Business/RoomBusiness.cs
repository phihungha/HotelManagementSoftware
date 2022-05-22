using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Add a new room.
        /// </summary>
        /// <param name="room">Room info</param>
        public async void AddRoom(Room room)
        {
            using (var db = new Database())
            {
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
    }

    public class RoomTypeBusiness
    {
        /// <summary>
        /// Get all room types.
        /// </summary>
        /// <returns>List of room types</returns>
        public async Task<List<RoomType>> GetAllRoomTypes()
        {
            using (var db = new Database())
            {
                return await db.RoomTypes.Include(i => i.Rooms).ToListAsync();
            }
        }

        /// <summary>
        /// Add a new room type
        /// </summary>
        /// <param name="roomType">Room type's info</param>
        public async void AddRoomType(RoomType roomType)
        {
            using (var db = new Database())
            {
                db.Add(roomType);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a room type.
        /// </summary>
        /// <param name="roomType">New room type's info</param>
        public async void EditRoom(RoomType roomType)
        {
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
        public async void RemoveRoom(RoomType roomType)
        {
            using (var db = new Database())
            {
                db.Remove(roomType);
                await db.SaveChangesAsync();
            }
        }

    }
}

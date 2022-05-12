using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class ReservationBusiness
    {
        public async void CreateNewReservation(Reservation reservation, bool checkIn)
        {

            using (var db = new Database())
            {
                Reservation? existingReservation = await db.Reservations
                    .Include(i => i.Room)
                    .FirstOrDefaultAsync(
                    i => CheckTimeCollision(i.ArrivalTime, i.DepartureTime,
                    reservation.ArrivalTime, reservation.DepartureTime));
                if (existingReservation == null)
                {
                    if (checkIn)
                        reservation.Status = ReservationStatus.CheckedIn;
                    else
                        reservation.Status = ReservationStatus.Reserved;
                    db.Add(reservation);
                    await db.SaveChangesAsync();
                }    
            }
        }

        public async void EditReservation(Reservation reservation)
        {
            using (var db = new Database())
            {
                Reservation? existingReservation = await db.Reservations
                    .Include(i => i.Room)
                    .FirstOrDefaultAsync(
                    i => CheckTimeCollision(i.ArrivalTime, i.DepartureTime,
                    reservation.ArrivalTime, reservation.DepartureTime));
                if (existingReservation == null)
                {
                    db.Update(reservation);
                    await db.SaveChangesAsync();
                }
            }
        }

        public bool CheckTimeCollision(DateTime currentArrivalTime,
                                       DateTime currentDepartureTime,
                                       DateTime newArrivalTime,
                                       DateTime newDepartureTime)
        {
            if (newDepartureTime <= currentDepartureTime && 
                newDepartureTime >= currentArrivalTime)
                return true;

            if (newArrivalTime >= currentArrivalTime &&
                newArrivalTime <= currentDepartureTime)
                return true;

            return false;
        }
    }
}

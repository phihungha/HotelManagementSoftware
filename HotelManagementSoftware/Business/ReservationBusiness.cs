using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class ReservationBusiness
    {
        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>List of reservations</returns>
        public async Task<List<Reservation>> GetAllReservations()
        {
            using (var db = new Database())
            {
                return await db.Reservations
                    .Include(i => i.Customer)
                    .Include(i => i.Orders)
                    .Include(i => i.Room)
                    .ThenInclude(room => room.RoomType)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Get reservations that arrive today.
        /// </summary>
        /// <returns>List of reservations</returns>
        public async Task<List<Reservation>> GetArriveTodayReservation()
        {
            using (var db = new Database())
            {
                return await db.Reservations
                    .Include(i => i.Customer)
                    .Include(i => i.Orders)
                    .Include(i => i.Room)
                    .ThenInclude(room => room.RoomType)
                    .Where(i => i.ArrivalTime.Date == DateTime.Now.Date)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Get reservations that depart today.
        /// </summary>
        /// <returns>List of reservations</returns>
        public async Task<List<Reservation>> GetDepartTodayReservation()
        {
            using (var db = new Database())
            {
                return await db.Reservations
                    .Include(i => i.Customer)
                    .Include(i => i.Orders)
                    .Include(i => i.Room)
                    .ThenInclude(room => room.RoomType)
                    .Where(i => i.DepartureTime.Date == DateTime.Now.Date)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Place a new reservation.
        /// </summary>
        /// <param name="reservation">Reservation info</param>
        /// <param name="checkIn">Checkin this reservation right away</param>
        /// <exception cref="ArgumentException">Problems with the reservation's info</exception>
        public async void CreateReservation(Reservation reservation, bool checkIn)
        {
            ValidateReservation(reservation);
            using (var db = new Database())
            {
                if (await CheckCollidedReservation(db, reservation))
                    throw new ArgumentException("Another reservation exists in this reservation's stay period");

                if (reservation.Room?.Status != RoomStatus.Usable)
                    throw new ArgumentException("This room cannot be used now");

                if (checkIn)
                    reservation.Status = ReservationStatus.CheckedIn;
                else
                    reservation.Status = ReservationStatus.Reserved;

                decimal totalRentFee = GetTotalRentFee(reservation);
                var order = new Order(DateTime.Now, totalRentFee, OrderStatus.Pending);
                reservation.Orders.Add(order);

                db.Add(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a reservation.
        /// </summary>
        /// <param name="reservation">New reservation info</param>
        public async void EditReservation(Reservation reservation)
        {
            ValidateReservation(reservation);
            using (var db = new Database())
            {
                if (await CheckCollidedReservation(db, reservation))
                    throw new ArgumentException("Another reservation exists in this reservation's stay period");

                if (reservation.Room?.Status != RoomStatus.Usable)
                    throw new ArgumentException("This room cannot be used now");

                decimal totalRentFee = GetTotalRentFee(reservation);
                var order = new Order(DateTime.Now, totalRentFee, OrderStatus.Pending);
                reservation.Orders[-1].Status = OrderStatus.Cancelled;
                reservation.Orders.Add(order);

                db.Update(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Cancel a reservation.
        /// </summary>
        /// <param name="reservation"></param>
        public async void CancelReservation(Reservation reservation)
        {
            using (var db = new Database())
            {
                reservation.Status = ReservationStatus.Cancelled;

                decimal cancelFee = await GetCancelFee(db, reservation);
                var order = new Order(DateTime.Now, cancelFee, OrderStatus.Paid);
                reservation.Orders.Add(order);

                db.Update(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Validate reservation's information before adding or updating.
        /// </summary>
        /// <param name="reservation">Reservation</param>
        /// <exception cref="ArgumentException">Validation error</exception>
        public void ValidateReservation(Reservation reservation)
        {
            if (reservation.ArrivalTime >= reservation.DepartureTime)
                throw new ArgumentException("Arrival time cannot be ahead of departure time");
            if (reservation.Room == null)
                throw new ArgumentException("Room cannot be empty");
            if (reservation.Customer == null)
                throw new ArgumentException("Customer cannot be empty");
            if (reservation.Employee == null)
                throw new ArgumentException("Employee cannot be empty");
        }

        /// <summary>
        /// Check in a reservation.
        /// </summary>
        /// <param name="reservation">Reservation to check in</param>
        /// <exception cref="ArgumentException">Reservation has been checked in before or has been canceled</exception>
        public async void CheckIn(Reservation reservation)
        {
            using (var db = new Database())
            {
                if (reservation.Status != ReservationStatus.Reserved)
                    throw new ArgumentException("Reservation has been checked in before or has been canceled");

                if (reservation.ArrivalTime.Date != DateTime.Now.Date)
                    throw new ArgumentException("Today is not arrival date");

                reservation.Status = ReservationStatus.CheckedIn;

                db.Update(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Check out a reservation
        /// </summary>
        /// <param name="reservation">Reservation to check out</param>
        /// <exception cref="ArgumentException">Reservation has not been checked in or has been canceled</exception>
        public async void CheckOut(Reservation reservation)
        {
            using (var db = new Database())
            {
                if (reservation.Status != ReservationStatus.CheckedIn)
                    throw new ArgumentException("Reservation has not been checked in or has been canceled");

                if (reservation.DepartureTime.Date != DateTime.Now.Date)
                    throw new ArgumentException("Today is not departure date");

                reservation.Status = ReservationStatus.CheckedOut;

                reservation.Orders[-1].Status = OrderStatus.Paid;

                db.Update(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get total room rent fee.
        /// </summary>
        /// <param name="reservation">Reservation info</param>
        /// <returns>Total rent fee</returns>
        /// <exception cref="ArgumentNullException">Reservation's room or room type is null</exception>
        private decimal GetTotalRentFee(Reservation reservation)
        {
            if (reservation?.Room?.RoomType?.Rate == null)
                throw new ArgumentException("Reservation's room or room type is null");
            decimal roomRate = reservation.Room.RoomType.Rate;

            TimeSpan stayPeriod = reservation.DepartureTime - reservation.ArrivalTime;
            int stayDayNum = (int)Math.Ceiling(stayPeriod.TotalDays);
            return stayDayNum * roomRate;
        }

        /// <summary>
        /// Get reservation cancel fee.
        /// The cancel fee is the total rent fee * percent
        /// Percent is based on the number of days the cancel happens before arrival date.
        /// </summary>
        /// <param name="db">Database context of parent method</param>
        /// <param name="arrivalDate">Arrival date of the reservation</param>
        /// <param name="totalRentFee">Total expected rent fee of the reservation</param>
        /// <returns>Cancel fee</returns>
        public async Task<decimal> GetCancelFee(Database db, Reservation reservation)
        {

            int dayNumberBeforeArrival = (DateTime.Now - reservation.ArrivalTime).Days;
            ReservationCancelFeePercent? cancelFeePercent 
                = await db.ReservationCancelFeePercents
                        .FirstOrDefaultAsync(
                    i => i.DayNumberBeforeArrival == dayNumberBeforeArrival
                    );

            if (cancelFeePercent == null)
                return 0;

            if (reservation.Room?.RoomType?.Rate == null)
                throw new ArgumentException("Reservation's room or room type is null");

            decimal totalRentFee = reservation.Room.RoomType.Rate;
            return totalRentFee * cancelFeePercent.PercentOfTotal / 100;
        }

        public async Task<bool> CheckCollidedReservation(Database db, Reservation newReservation)
        {
            Reservation? collidedReservation = 
                await db.Reservations.Include(i => i.Room)
                                     .FirstOrDefaultAsync(
                        i => CheckStayPeriodCollision(newReservation, i)
                    );
            if (collidedReservation == null)
                return false;
            return true;
        }

        /// <summary>
        /// Check if the stay period of a reservation collided with another reservation.
        /// </summary>
        /// <param name="reservation">New reservation to check</param>
        /// <param name="existingReservation">Existing reservation to check with</param>
        /// <returns>True if the stay period collided</returns>
        public bool CheckStayPeriodCollision(Reservation reservation, Reservation existingReservation)
        {
            if (reservation.DepartureTime <= existingReservation.DepartureTime && 
                reservation.DepartureTime >= existingReservation.ArrivalTime)
                return true;

            if (reservation.ArrivalTime >= existingReservation.ArrivalTime &&
                reservation.ArrivalTime <= existingReservation.DepartureTime)
                return true;

            return false;
        }
    }
}

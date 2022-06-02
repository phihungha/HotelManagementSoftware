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
        /// Contains info of upcoming arrivals.
        /// </summary>
        public class UpcomingArrival
        {
            public Reservation Reservation { get; }

            public Customer? Customer => Reservation.Customer;

            public TimeSpan RemainingTime { get; set; }

            public UpcomingArrival(Reservation reservation, TimeSpan remainingTime)
            {
                Reservation = reservation;
                RemainingTime = remainingTime;
            }
        }

        /// <summary>
        /// Contains info of upcoming departures.
        /// </summary>
        public class UpcomingDeparture
        {
            public Reservation Reservation { get; }

            public Customer? Customer => Reservation.Customer;

            public TimeSpan RemainingTime { get; set; }

            public UpcomingDeparture(Reservation reservation, TimeSpan remainingTime)
            {
                Reservation = reservation;
                RemainingTime = remainingTime;
            }
        }

        /// <summary>
        /// Get number of reservations in reserved status.
        /// </summary>
        /// <returns>Result</returns>
        public async Task<int> GetReservedReservationNumber()
        {
            using (var db = new Database())
            {
                return await db.Reservations
                    .Where(i => i.Status == ReservationStatus.Reserved)
                    .CountAsync();
            }
        }

        /// <summary>
        /// Ger reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Reservation</returns>
        public async Task<Reservation?> GetReservationById(int id)
        {
            using (var db = new Database())
            {
                return await db.Reservations
                    .Include(i => i.Customer)
                    .Include(i => i.Order)
                    .Include(i => i.Room)
                    .ThenInclude(room => room.RoomType)
                    .FirstOrDefaultAsync(i => i.ReservationId == id);
            }
        }

        /// <summary>
        /// Get reservations that satisfy specified criteria.
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="customerName">Customer name</param>
        /// <param name="roomNumber">Room number</param>
        /// <param name="roomType">Room type name</param>
        /// <param name="employeeName">Employee name</param>
        /// <param name="fromArrivalTime">Min arrival time</param>
        /// <param name="toArrivalTime">Max arrival time</param>
        /// <param name="fromDepartureTime">Min departure time</param>
        /// <param name="toDepartureTime">To departure time</param>
        /// <param name="fromTotalRentFee">Min total rent fee</param>
        /// <param name="toTotalRentFee">Max total rent fee</param>
        /// <returns>List of reservations</returns>
        public async Task<List<Reservation>> GetReservations(
            ReservationStatus? status = null,
            string? customerName = null,
            int? roomNumber = null,
            string? roomType = null,
            string? employeeName = null,
            DateTime? fromArrivalTime = null,
            DateTime? toArrivalTime = null,
            DateTime? fromDepartureTime = null,
            DateTime? toDepartureTime = null,
            decimal? fromTotalRentFee = null,
            decimal? toTotalRentFee = null)
        {
            using (var db = new Database())
            {
                var request = db.Reservations
                        .Include(i => i.Customer)
                        .Include(i => i.Order)
                        .Include(i => i.Room)
                        .ThenInclude(room => room.RoomType);

                var filteredRequest = request.Where(i => true);

                if (status != null)
                    filteredRequest = filteredRequest.Where(i => i.Status == status);

                if (customerName != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Customer != null && i.Customer.Name == customerName);

                if (roomNumber != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Room != null && i.Room.RoomNumber == roomNumber);

                if (roomType != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Room != null && i.Room.RoomType != null &&
                                    i.Room.RoomType.Name == roomType);

                if (employeeName != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Employee != null && i.Employee.Name == employeeName);

                if (fromArrivalTime != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.ArrivalTime >= fromArrivalTime);

                if (toArrivalTime != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.ArrivalTime <= toArrivalTime);

                if (fromDepartureTime != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.DepartureTime >= fromDepartureTime);

                if (toDepartureTime != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.DepartureTime <= toDepartureTime);

                if (fromTotalRentFee != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Order != null &&
                                    i.Order.Amount >= fromTotalRentFee);

                if (toTotalRentFee != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Order != null &&
                                    i.Order.Amount <= toTotalRentFee);

                return await filteredRequest.ToListAsync();
            }
        }

        /// <summary>
        /// Get reservations that arrive in 24 hours.
        /// </summary>
        /// <param name="customerName">Name of customer</param>
        /// <param name="customerIdNumber">Personal ID of customer</param>
        /// <returns>List of reservations</returns>
        public async Task<List<UpcomingArrival>> GetUpcomingArrivals(
            string? customerName = null, 
            string? customerIdNumber = null)
        {
            using (var db = new Database())
            {
                var filteredRequest = db.Reservations
                            .Include(i => i.Customer)
                            .Include(i => i.Order)
                            .Include(i => i.Room)
                            .ThenInclude(room => room.RoomType)
                            .Where(i => i.ArrivalTime <= DateTime.Now.AddDays(1))
                            .Where(i => i.Status == ReservationStatus.Reserved);

                if (customerName != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Customer != null && i.Customer.Name == customerName);

                if (customerIdNumber != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Customer != null && i.Customer.IdNumber == customerIdNumber);

                return await filteredRequest
                    .Select(i => new UpcomingArrival(
                                   i, i.ArrivalTime - DateTime.Now))
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Get reservations that depart in 24 hours.
        /// </summary>
        /// <param name="customerName">Name of customer</param>
        /// <param name="customerIdNumber">Personal ID of customer</param>
        /// <returns>List of reservations</returns>
        public async Task<List<UpcomingDeparture>> GetUpcomingDepartures(
            string? customerName = null,
            string? customerIdNumber = null)
        {
            using (var db = new Database())
            {
                var filteredRequest = db.Reservations
                            .Include(i => i.Customer)
                            .Include(i => i.Order)
                            .Include(i => i.Room)
                            .ThenInclude(room => room.RoomType)
                            .Where(i => i.DepartureTime <= DateTime.Now.AddDays(1))
                            .Where(i => i.Status == ReservationStatus.CheckedIn);

                if (customerName != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Customer != null && i.Customer.Name == customerName);

                if (customerIdNumber != null)
                    filteredRequest = filteredRequest
                        .Where(i => i.Customer != null && i.Customer.IdNumber == customerIdNumber);

                return await filteredRequest
                    .Select(i => new UpcomingDeparture(
                                   i, i.DepartureTime - DateTime.Now))
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Place a new reservation.
        /// </summary>
        /// <param name="reservation">Reservation info</param>
        /// <param name="checkIn">Checkin this reservation right away</param>
        /// <exception cref="ArgumentException">Problems with the reservation's info</exception>
        public async Task CreateReservation(Reservation reservation, bool checkIn)
        {
            ValidateReservation(reservation);
            using (var db = new Database())
            {
                if (await CheckCollidedReservation(db, reservation))
                    throw new ArgumentException("Another reservation exists in this reservation's stay period");

                if (reservation.Room?.Status != RoomStatus.Usable)
                    throw new ArgumentException("This room cannot be used now");

                if (reservation.NumberOfPeople > reservation.Room?.RoomType?.Capacity)
                    throw new ArgumentException("Number of people is higher than room's capacity");

                if (reservation.Room == null)
                    throw new ArgumentException("Room cannot be empty");

                if (reservation.Customer == null)
                    throw new ArgumentException("Customer cannot be empty");

                if (reservation.Employee == null)
                    throw new ArgumentException("Employee cannot be empty");

                if (checkIn)
                    reservation.Status = ReservationStatus.CheckedIn;
                else
                    reservation.Status = ReservationStatus.Reserved;

                decimal totalRentFee = GetTotalRentFee(reservation);
                reservation.Order = new Order(DateTime.Now, totalRentFee, OrderStatus.Pending);

                db.Attach(reservation.Customer);
                db.Attach(reservation.Room);
                db.Attach(reservation.Employee);
                db.Add(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit a reservation.
        /// </summary>
        /// <param name="reservation">Updated reservation info</param>
        public async Task EditReservation(Reservation reservation)
        {
            ValidateReservation(reservation);
            using (var db = new Database())
            {
                if (await CheckCollidedReservation(db, reservation))
                    throw new ArgumentException("Another reservation exists in this reservation's stay period");

                if (reservation.Room?.Status != RoomStatus.Usable)
                    throw new ArgumentException("This room cannot be used now");

                if (reservation.Order == null)
                    throw new ArgumentException("Order cannot be null when editing reservation");

                reservation.Order.CreationTime = DateTime.Now;
                reservation.Order.Amount = GetTotalRentFee(reservation);

                db.Update(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Cancel a reservation.
        /// </summary>
        /// <param name="reservation">Reservation info</param>
        public async Task CancelReservation(Reservation reservation)
        {
            using (var db = new Database())
            {
                if (reservation.Status != ReservationStatus.Reserved)
                    throw new ArgumentException("Cannot cancel reservation if it has been checked in or cancelled");

                if (reservation.Order == null)
                    throw new ArgumentException("Order cannot be null when cancelling reservation");

                reservation.Status = ReservationStatus.Cancelled;

                reservation.Order.PayTime = DateTime.Now;
                reservation.Order.Amount = await GetCancelFee(db, reservation);
                reservation.Order.Status = OrderStatus.Paid;

                db.Update(reservation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Check in a reservation.
        /// </summary>
        /// <param name="reservation">Reservation to check in</param>
        /// <exception cref="ArgumentException">Reservation has been checked in before or has been canceled</exception>
        public async Task CheckIn(Reservation reservation)
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
        public async Task CheckOut(Reservation reservation)
        {
            using (var db = new Database())
            {
                if (reservation.Status != ReservationStatus.CheckedIn)
                    throw new ArgumentException("Reservation has not been checked in or has been canceled");

                if (reservation.DepartureTime.Date != DateTime.Now.Date)
                    throw new ArgumentException("Today is not departure date");

                reservation.Status = ReservationStatus.CheckedOut;

                if (reservation.Order == null)
                    throw new ArgumentException("Order cannot be null");

                reservation.Order.PayTime = DateTime.Now;
                reservation.Order.Status = OrderStatus.Paid;

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
            if (reservation.Employee.EmployeeType != EmployeeType.Receptionist
                && reservation.Employee.EmployeeType != EmployeeType.Manager)
                throw new ArgumentException("Reservation needs to be created by a receptionist or manager");
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
            int dayNumberBeforeArrival = (reservation.ArrivalTime - DateTime.Now).Days;
            ReservationCancelFeePercent? cancelFeePercent 
                = await db.ReservationCancelFeePercents
                    .FirstOrDefaultAsync(i => i.DayNumberBeforeArrival == dayNumberBeforeArrival);

            if (cancelFeePercent == null)
                return 0;

            if (reservation.Room?.RoomType?.Rate == null)
                throw new ArgumentException("Reservation's room or room type is null");

            decimal totalRentFee = reservation.Room.RoomType.Rate;
            return totalRentFee * cancelFeePercent.PercentOfTotal / 100;
        }

        public async Task<bool> CheckCollidedReservation(Database db, Reservation newReservation)
        {
            List<Reservation> collidedReservation =
                await db.Reservations.Include(i => i.Room)
                            .Where(i => i.Room == newReservation.Room)
                            .ToListAsync();
            return collidedReservation.Any(
                            i => CheckStayPeriodCollision(
                                newReservation.ArrivalTime,
                                i.ArrivalTime,
                                newReservation.DepartureTime,
                                i.DepartureTime)
                            );
        }

        /// <summary>
        /// Check if the stay period of a reservation collided with another reservation.
        /// </summary>
        /// <param name="arrivalTime">Arrival time of new reservation</param>
        /// <param name="departureTime">Departure time of new reservation</param>
        /// <param name="existingArrivalTime">Arrival time of existing reservation</param>
        /// <param name="existingDepartureTime">Departure time of existing reservation</param>
        /// <returns>True if the stay period collided</returns>
        public static bool CheckStayPeriodCollision(DateTime arrivalTime,
                                                    DateTime existingArrivalTime,
                                                    DateTime departureTime,
                                                    DateTime existingDepartureTime)
        {
            if (departureTime <= existingDepartureTime &&
                departureTime >= existingArrivalTime)
                return true;

            if (arrivalTime >= existingArrivalTime &&
                arrivalTime <= existingDepartureTime)
                return true;

            return false;
        }
    }

    public class ReservationCancelFeePercentBusiness
    {
        /// <summary>
        /// Add reservation cancel fee percent.
        /// </summary>
        /// <param name="percent">Percent info</param>
        public async Task Add(ReservationCancelFeePercent percent)
        {
            ValidatePercent(percent);
            using (var db = new Database())
            {
                db.ReservationCancelFeePercents.Add(percent);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit reservation cancel fee percent.
        /// </summary>
        /// <param name="percent">Updated percent info</param>
        public async Task Edit(ReservationCancelFeePercent percent)
        {
            ValidatePercent(percent);
            using (var db = new Database())
            {
                db.ReservationCancelFeePercents.Update(percent);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete reservation cancel fee percent.
        /// </summary>
        /// <param name="percent">Percent info</param>
        public async Task Delete(ReservationCancelFeePercent percent)
        {
            ValidatePercent(percent);
            using (var db = new Database())
            {
                db.ReservationCancelFeePercents.Remove(percent);
                await db.SaveChangesAsync();
            }
        }

        public void ValidatePercent(ReservationCancelFeePercent percent)
        {
            if (percent.PercentOfTotal <= 0)
                throw new ArgumentException("Percent cannot be <= 0");
            if (percent.DayNumberBeforeArrival < 0)
                throw new ArgumentException("Day number cannot be smaller than 0");
        }
    }
}

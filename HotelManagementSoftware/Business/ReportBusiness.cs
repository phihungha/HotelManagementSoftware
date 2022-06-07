using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Business
{
    public class ReportBusiness
    {
        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;

        // Average revenue per room sold in each day (ADR)
        public Dictionary<int, decimal> DailyADR { get; private set; } = new();
        // Number of available rooms in each day
        public Dictionary<int, int> DailyAvailableRoomNumber { get; private set; } = new();
        // Occupancy rate in each day
        public Dictionary<int, double> DailyOccupancyRate { get; private set; } = new();
        // Total revenue from paid orders in each day
        public Dictionary<int, decimal> DailyRevenue { get; private set; } = new();
        // Average revenue per available rooms in eacy day
        public Dictionary<int, decimal> DailyRevPAR { get; private set; } = new();

        // ADR for this month
        public decimal MonthADR { get; private set; }
        // Average number of available rooms for this month
        public double MonthAverageAvailableRoomNumber { get; private set; }
        // Average occupancy rate for this month
        public double MonthAverageOccupancyRate { get; private set; }
        // Average revenue for this month
        public decimal MonthAverageRevenue { get; private set; }
        // Total revenue for this month
        public decimal MonthTotalRevenue { get; private set; }
        // RevPAR for this month
        public decimal MonthRevPAR { get; private set; }

        /// <summary>
        /// Get average revenue per room sold for each day.
        /// </summary>
        /// <returns>ADR for each day</returns>
        private async Task<Dictionary<int, decimal>> GetDailyADR()
        {
            using (var db = new Database())
            {
                return await db.Orders
                    .Where(i => i.CreationTime.Year == year 
                                && i.CreationTime.Month == month)
                    .GroupBy(i => i.CreationTime.Day)
                    .Select(g => new {
                        Day = g.Key,
                        AverageRate = g.Sum(i => i.Amount) 
                            / g.Select(i => i.Reservation.Room.RoomId).Distinct().Count()
                    })
                    .ToDictionaryAsync(i => i.Day, i => i.AverageRate);
            }
        }

        /// <summary>
        /// Get available room number of each day.
        /// </summary>
        /// <returns>Available room number for each day</returns>
        private async Task<Dictionary<int, int>> GetDailyAvailableRoomNumber()
        {
            Dictionary<int, int> result = new();
            List<Reservation> reservations;
            int totalRoomNumber;

            using (var db = new Database())
            {
                totalRoomNumber = await db.Rooms.CountAsync();
                reservations = await db.Reservations
                    .Where(i => (
                        i.ArrivalTime.Month == month && i.ArrivalTime.Year == year)
                        || (i.DepartureTime.Month == month && i.DepartureTime.Year == year))
                    .ToListAsync();
            }

            int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= numberOfDaysInMonth; day++)
                result.Add(day, totalRoomNumber);

            foreach (Reservation reservation in reservations)
            {
                DateTime arrivalTime = reservation.ArrivalTime;
                DateTime departureTime = reservation.DepartureTime;

                int minDayInMonth = arrivalTime.Month != month ? 1 : arrivalTime.Day;
                int maxDayInMonth = departureTime.Month != month ? 1 : departureTime.Day;

                for (int day = minDayInMonth; day <= maxDayInMonth; day++)
                    result[day]--;
            }

            return result;
        }

        /// <summary>
        /// Get total revenue for each day.
        /// </summary>
        /// <returns>Total revenue for each day</returns>
        private async Task<Dictionary<int, decimal>> GetDailyRevenue()
        {
            using (var db = new Database())
            {
                return await db.Orders
                    .Where(i => i.PayTime != null 
                                && ((DateTime)i.PayTime).Month == month 
                                && ((DateTime)i.PayTime).Year == year)
                    .GroupBy(i => ((DateTime)i.PayTime).Day)
                    .Select(g => new
                    {
                        Day = g.Key,
                        Revenue = g.Sum(i => i.Amount)
                    })
                    .ToDictionaryAsync(i => i.Day, i => i.Revenue);
            }
        }

        /// <summary>
        /// Get revenue per available room (RevPAR) for each day.
        /// </summary>
        /// <returns>RevPAR</returns>
        private Dictionary<int, decimal> GetDailyRevPAR(
            Dictionary<int, decimal> dailyRevenue,
            Dictionary<int, int> dailyAvailableRoomNumber)
        {
            Dictionary<int, decimal> result = new();
            foreach (int day in dailyRevenue.Keys)
                result[day] = dailyRevenue[day] / dailyAvailableRoomNumber[day];
            return result;
        }

        /// <summary>
        /// Get occupancy rate for each day.
        /// </summary>
        /// <returns>Occupancy rate</returns>
        private Dictionary<int, double> GetDailyOccupancyRate(
            Dictionary<int, decimal> dailyRevPAR,
            Dictionary<int, decimal> dailyADR)
        {
            Dictionary<int, double> result = new();
            foreach (int day in dailyRevPAR.Keys)
                result[day] = (double)(dailyRevPAR[day] / dailyADR[day]);
            return result;
        }
        
        /// <summary>
        /// Calculate stats for the entire month.
        /// </summary>
        public void CalculateMonthStats()
        {
            MonthADR = DailyADR.Average(i => i.Value);
            MonthAverageAvailableRoomNumber = DailyAvailableRoomNumber.Average(i => i.Value);
            MonthAverageOccupancyRate = DailyOccupancyRate.Average(i => i.Value);
            MonthAverageRevenue = DailyRevenue.Average(i => i.Value);
            MonthTotalRevenue = DailyRevenue.Sum(i => i.Value);
            MonthRevPAR = DailyRevPAR.Average(i => i.Value);
        }

        /// <summary>
        /// Generate report statistics for given year and month.
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        public async Task LoadReport(int year, int month)
        {
            this.year = year;
            this.month = month;

            DailyADR = await GetDailyADR();
            DailyAvailableRoomNumber = await GetDailyAvailableRoomNumber();
            DailyRevenue = await GetDailyRevenue();
            DailyRevPAR = GetDailyRevPAR(DailyRevenue, DailyAvailableRoomNumber);
            DailyOccupancyRate = GetDailyOccupancyRate(DailyRevPAR, DailyADR);
            CalculateMonthStats();
        }
    }
}

using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;

namespace HotelManagementSoftware.ViewModels
{
    public class DashboardVM : ObservableObject
    {
        private ReservationBusiness reservationBusiness;

        private Timer currentTimeTimer = new Timer()
        {
            Interval = 1,
            AutoReset = true,
            Enabled = true
        };

        public ObservableCollection<Reservation> ArrivalsToday { get; } = new();
        public ObservableCollection<Reservation> DeparturesToday { get; } = new();

        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get => currentTime;
            set => SetProperty(ref currentTime, value);
        }

        private int arrivalNumber = 0;
        public int ArrivalNumber
        {
            get => arrivalNumber;
            set => SetProperty(ref arrivalNumber, value);
        }

        private int departureNumber = 0;
        public int DepartureNumber
        {
            get => departureNumber;
            set => SetProperty(ref departureNumber, value);
        }

        private int reservationsNumber = 0;
        public int ReservationNumber
        {
            get => reservationsNumber;
            set => SetProperty(ref reservationsNumber, value);
        }

        public DashboardVM(ReservationBusiness reservationBusiness)
        {
            this.reservationBusiness = reservationBusiness;
            currentTimeTimer.Elapsed += CurrentTimeTimer_Elapsed;
            LoadData();
        }

        private void CurrentTimeTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        private async void LoadData()
        {
            List<Reservation> arrivalsToday = await reservationBusiness.GetArriveTodayReservations();
            arrivalsToday.ForEach(i => ArrivalsToday.Add(i));
            ArrivalNumber = arrivalsToday.Count();

            List<Reservation> departuresToday = await reservationBusiness.GetDepartTodayReservations();
            departuresToday.ForEach(i => DeparturesToday.Add(i));
            ArrivalNumber = departuresToday.Count();
        }

    }
}

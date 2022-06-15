using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.Business;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.ComponentModel;


namespace HotelManagementSoftware.ViewModels
{
    public enum SearchOption
    {
        [Description("Customer Name")]
        CustomerName,
        [Description("Room type name")]
        Roomtype,
        [Description("Employee Name")]
        Employee
    }
    public class ArrivalsVM : ObservableValidator
    {
        private  ReservationBusiness reservationBusiness;
        private ReservationStatus status;
        public ObservableCollection<Reservation> Arrival { get; set;}
        public ReservationStatus Status
        {
            get => status;
            set => SetProperty(ref status, value, true);
        }

        public async void GetAllReservation()
        {
            if (reservationBusiness != null)
            {
                List<Reservation> reservations = await reservationBusiness.GetReservations();
                Arrival.Clear();
                reservations.ForEach(roomtype =>
                {
                    Arrival.Add(roomtype);
                });

            }
        }

        public ArrivalsVM(ReservationBusiness? reservationBusiness)
        {
            this.reservationBusiness = reservationBusiness;
            Arrival = new ObservableCollection<Reservation>();
            GetAllReservation();


        }

   
        public async void LoadArrivals()
        {
            Arrival.Clear();
            List<Reservation> reservations = await reservationBusiness.GetReservations();
            reservations.ForEach(i => Arrival.Add(i));
        }

       
    }
}

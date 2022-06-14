using HandyControl.Controls;
using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class ReservationsVM : ObservableValidator
    {
        public ObservableCollection<Reservation> Reservations { get; private set; }
        public Reservation SelectedReservations { get; set; }
        public ReservationBusiness? reservationBusiness;
        public TimeFilter ArrivalTimeFilter { get; private set; }
        public TimeFilter DepartureTimeFilter { get; private set; }
        private ReservationStatus status;
        private SearchOption option;
        private string textFilter;
        public string TextFilter
        {
            get => textFilter;
            set => SetProperty(ref textFilter, value, true);
        }
        public SearchOption Option
        {
            get => option;
            set => SetProperty(ref option, value, true);
        }
        public ReservationStatus Status
        {
            get => status;
            set => SetProperty(ref status, value, true);
        }
        public ReservationsVM(ReservationBusiness? reservationBusiness)
        {
            this.reservationBusiness = reservationBusiness;
            Reservations = new ObservableCollection<Reservation>();
            GetAllReservation();
            SearchCommand = new AsyncRelayCommand(Search);
        }
        #region command
        public ICommand CommandAdd { get; }
        public ICommand CommandDelete { get; }
        public ICommand CommandEdit { get; }
        public void executeAddAction()
        {

        }
        public void executeDeleteAction()
        {

        }
        public void executeEditAction()
        {

        }
        #endregion
        public class TimeFilter
        {
            public bool Enable { get; set; }
            public DateTime? low { get; set; }
            public DateTime? high { get; set; }
        }
        public enum SearchOption
        {
            [Description("Customer Name")]
            CustomerName,
            [Description("Room type name")]
            Roomtype,
            [Description("Employee Name")]
            Employee
        }
        public async void GetAllReservation()
        {
            if (reservationBusiness != null)
            {
                List<Reservation> reservations = await reservationBusiness.GetReservations();
                Reservations.Clear();
                reservations.ForEach(roomtype =>
                {
                    Reservations.Add(roomtype);
                });

            }
        }
        public ICommand SearchCommand { get; }
        public async Task Search()
        {
            if (ArrivalTimeFilter.Enable == false)
            {
                ArrivalTimeFilter.high = null;
                ArrivalTimeFilter.low = null;
            }
            if (DepartureTimeFilter.Enable == false)
            {
                DepartureTimeFilter.high = null;
                DepartureTimeFilter.low = null;
            }
            if (TextFilter == "") TextFilter = null;
            List<Reservation> reservations = new();
            switch (option)
            {
                case SearchOption.CustomerName:
                    reservations = await reservationBusiness.GetReservations(Status, TextFilter, null, null, null, ArrivalTimeFilter.low, ArrivalTimeFilter.high, DepartureTimeFilter.low, DepartureTimeFilter.high, null, null);
                    break;
                case SearchOption.Roomtype:
                    reservations = await reservationBusiness.GetReservations(Status, null, null, TextFilter, null, ArrivalTimeFilter.low, ArrivalTimeFilter.high, DepartureTimeFilter.low, DepartureTimeFilter.high, null, null);
                    break;
                case SearchOption.Employee:
                    reservations = await reservationBusiness.GetReservations(Status, null, null, null, TextFilter, ArrivalTimeFilter.low, ArrivalTimeFilter.high, DepartureTimeFilter.low, DepartureTimeFilter.high, null, null);
                    break;
            }
            Reservations.Clear();
            reservations.ForEach(roomtype =>
            {
                Reservations.Add(roomtype);
            });
        }
    }
}

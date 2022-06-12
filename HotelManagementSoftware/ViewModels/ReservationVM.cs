using HandyControl.Controls;
using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class ReservationsVM: ObservableValidator
    {
        public ObservableCollection<Reservation> Reservations { get; private set; }
        public Reservation SelectedReservations { get; set; }
        public ReservationBusiness reservationBusiness;
        public TimeFilter ArrivalTimeFilter { get; private set; }
        public TimeFilter DepartureTimeFilter { get; private set; }
        public ReservationStatus[] status { get; private set; }
        public ReservationStatus selectedstatus { get; private set; }
        public SearchOption[] SearchOptions { get; private set; }
        public SearchOption SelectedSearchOption { get; private set; }
        public string TextFilter { get; set; }

        public ReservationsVM()
        {
            Reservations = new ObservableCollection<Reservation>();
            CommandAdd = new RelayCommand(executeAddAction);
            CommandDelete = new RelayCommand(executeDeleteAction);
            CommandEdit = new RelayCommand(executeEditAction);
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
            public int Enable { get; set; }
            public DateTime low { get; set; }
            public DateTime high { get; set; }
        }
        public enum SearchOptionId
        {
            Name,
            CustomerID,
            ReservationID,
            PhoneNumber
        }
        public struct SearchOption
        {
            public SearchOptionId SearchOptionId { get; set; }
        }
        public async void GetAllReservation()
        {
            if (reservationBusiness != null)
            {
                List<Reservation> reservations = await reservationBusiness.GetReservations(null, null, null, null, null, null, null, null, null, null, null);
                Reservations.Clear();
                reservations.ForEach(roomtype =>
                {
                    Reservations.Add(roomtype);
                });

            }
        }
    }
}

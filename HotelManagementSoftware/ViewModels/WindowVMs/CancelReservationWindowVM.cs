using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class CancelReservationWindowVM : ObservableValidator
    {
        private int reservationId;
        public decimal cancelFee;
        public decimal CancelFee
        {
            get => cancelFee;
            set
            {
                SetProperty(ref cancelFee, value);
            }
        }
        private ReservationBusiness? reservationBusiness;
        public CancelReservationWindowVM(ReservationBusiness? reservationBusiness)
        {
            this.reservationBusiness = reservationBusiness;
        }
        public async void LoadReservationFromId(int reservationId)
        {
            Reservation? reservation = await reservationBusiness.GetReservationById(reservationId);
            CancelFee = await reservationBusiness.GetCancelFee(reservation);
            this.reservationId = reservationId;
        }
        public async Task Cancel()
        {
            Reservation? reservation = await reservationBusiness.GetReservationById(reservationId);
            reservationBusiness.CancelReservation(reservation);
        }
    }
}

using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HotelManagementSoftware.Data;


namespace HotelManagementSoftware.ViewModels
{
    public class ArrivalsVM : ObservableValidator
    {
        public ObservableCollection<Reservation> Arrival { get; set; }

        public ArrivalsVM()
        {
            Arrival = new ObservableCollection<Reservation>();
            addArrival();
        }
        private void addArrival()
        {
            Arrival.Add(new Reservation(
                   new DateTime(2020, 2, 13), new DateTime(2020, 2, 15), 2, ReservationStatus.Reserved 
                   ));
        }
    }
}

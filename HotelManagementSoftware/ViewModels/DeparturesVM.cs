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
    public class DeparturesVM : ObservableValidator
    {
        public ObservableCollection<Reservation> Departure { get; set; }

        public DeparturesVM()
        {
            Departure = new ObservableCollection<Reservation>();
            addArrival();
        }
        private void addArrival()
        {
            Departure.Add(new Reservation(
                   new DateTime(2020, 2, 13), new DateTime(2020, 2, 15), 2, ReservationStatus.Reserved
                   ));
        }

       
    }
}

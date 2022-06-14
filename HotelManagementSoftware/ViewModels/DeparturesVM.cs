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
    public class DeparturesVM : ObservableValidator
    {
        public ObservableCollection<Reservation> Departure { get; set; }
        private ReservationBusiness reservationBusiness;
        private CustomerBusiness customerBusiness;

        private string searchTerm = "";
        private DeparturesSearchBy searchBy = DeparturesSearchBy.Name;

       
        public enum DeparturesSearchBy
        {
            [Description("Name")]
            Name,
            [Description("Identity number")]
            IdNumber,
            [Description("Phone number")]
            PhoneNumber
        }
   
        public DeparturesVM()
        {
            Departure = new ObservableCollection<Reservation>();
            //SearchCommand = new AsyncRelayCommand(Search);

        }


        public async void LoadDepartures()
        {
            Departure.Clear();
            List<Reservation> reservations = await reservationBusiness.GetReservations();
            reservations.ForEach(i => Departure.Add(i));

        }
    }
}


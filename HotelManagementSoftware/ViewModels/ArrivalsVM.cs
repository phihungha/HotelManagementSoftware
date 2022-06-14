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
    public enum ArrivalSearchBy
    {
        [Description("Name")]
        Name,
        [Description("Identity number")]
        IdNumber,
        [Description("Phone number")]
        PhoneNumber
    }
    public class ArrivalsVM : ObservableValidator
    {
        private  ReservationBusiness reservationBusiness;
        private CustomerBusiness customerBusiness;

        private string searchTerm = "";
        private ArrivalSearchBy searchBy = ArrivalSearchBy.Name;

        public ObservableCollection<Reservation> Arrival { get; set; }

        //public string SearchTerm
        //{
        //    get => searchTerm;
        //    set => SetProperty(ref searchTerm, value);
        //}
        //public ArrivalSearchBy SearchBy
        //{
        //    get => searchBy;
        //    set => SetProperty(ref searchBy, value);
        //}
        

        //public ICommand SearchCommand { get; }


        public ArrivalsVM()
        {
            Arrival = new ObservableCollection<Reservation>();
            //SearchCommand = new AsyncRelayCommand(Search);
            
        }

   
        public async void LoadArrivals()
        {
            Arrival.Clear();
            List<Reservation> reservations = await reservationBusiness.GetReservations();
            reservations.ForEach(i => Arrival.Add(i));
        }

        //private async Task Search()
        //{
        //    Arrival.Clear();

        //    if (SearchTerm == "")
        //    {
        //        LoadArrivals();
        //        return;
        //    }


        //    List<Customer> customers = new();
        //    Customer? customer = null;
        //    switch (SearchBy)
        //    {
        //        case ArrivalSearchBy.Name:
        //            (await customerBusiness.GetCustomersByName(SearchTerm))
        //                .ForEach(i => Arrival.Add(i));
        //            break;
        //        case ArrivalSearchBy.IdNumber:
        //            customer = await customerBusiness.GetCustomerByIdNumber(SearchTerm);
        //            break;
        //        case ArrivalSearchBy.PhoneNumber:
        //            customer = await customerBusiness.GetCustomerByPhoneNumber(SearchTerm);
        //            break;
        //    }

        //    if (customer != null)
        //        Arrival.Add(customer);
        //}
    }
}

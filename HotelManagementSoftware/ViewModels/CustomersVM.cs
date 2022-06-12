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
    public enum SearchBy
    {
        [DescriptionAttribute("Name")]
        Name,
        [DescriptionAttribute("Identity number")]
        IdNumber,
        [DescriptionAttribute("Phone number")]
        PhoneNumber
    }

    public class CustomersVM : ObservableValidator
    {
        private CustomerBusiness customerBusiness;

        private string searchTerm = "";
        private SearchBy searchBy = SearchBy.Name;

        public ObservableCollection<Customer> Customers { get; } = new();

        public string SearchTerm
        {
            get => searchTerm;
            set => SetProperty(ref searchTerm, value);
        }

        public SearchBy SearchBy
        {
            get => searchBy;
            set => SetProperty(ref searchBy, value);
        }

        public ICommand AddCustomerCommand { get; }

        public ICommand SearchCommand { get; }

        public CustomersVM(CustomerBusiness customerBusiness)
        {
            this.customerBusiness = customerBusiness;

            AddCustomerCommand = new RelayCommand(OpenAddCustomerDialog);
            SearchCommand = new AsyncRelayCommand(Search);

            LoadCustomers();
        }

        public async void LoadCustomers()
        {
            Customers.Clear();
            List<Customer> customers = await customerBusiness.GetCustomers();
            customers.ForEach(i => Customers.Add(i));
        }

        private void OpenAddCustomerDialog()
        {

        }

        private async Task Search()
        {
            Customers.Clear();

            if (SearchTerm == "")
            {
                LoadCustomers();
                return;
            }

            List<Customer> customers = new();
            Customer? customer = null;

            switch (SearchBy)
            {
                case SearchBy.Name:
                    (await customerBusiness.GetCustomersByName(SearchTerm))
                        .ForEach(i => Customers.Add(i));
                    break;
                case SearchBy.IdNumber:
                    customer = await customerBusiness.GetCustomerByIdNumber(SearchTerm);
                    break;
                case SearchBy.PhoneNumber:
                    customer = await customerBusiness.GetCustomerByPhoneNumber(SearchTerm);
                    break;
            }

            if (customer != null)
                Customers.Add(customer);
        }
    }
}

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
    public class CustomersVM : ObservableValidator
    {

        public ObservableCollection<Customer> Customers { get; set; }

        public CustomersVM()
        {
            Customers = new ObservableCollection<Customer>();
            addCustomers();
        }

        private void addCustomers()
        {
            Customers.Add(new Customer("Nguyễn Văn A",
                                                         new DateTime(1975, 4, 2),
                                                         IdNumberType.Cmnd,
                                                         "123456789",
                                                         Gender.Male,
                                                         "0344250406",
                                                         "110 Nguyễn Huệ",
                                                         "Quận 1",
                                                         "TP. HCM",
                                                         PaymentMethod.Cash));
        }

    }
}

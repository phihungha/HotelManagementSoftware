using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HotelManagementSoftware.ViewModels
{
    public class EmployeesVM : ObservableValidator
    {
        public ObservableCollection<Employee> Employees { get; set; }
 
        public EmployeesVM()
        {
            Employees = new ObservableCollection<Employee>();

            Employees.Add(new Employee("f1", "l1",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "phone1", "email1",
                "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));

            Employees.Add(new Employee("f1", "l1",
             "userName1", Gender.Male,
             new DateTime(2020, 2, 13),
             "phone1", "email1",
             "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));
            Employees.Add(new Employee("firstName1", "lastName1",
              "userName1", Gender.Male,
              new DateTime(2020, 1, 1),
              "phone1", "email1",
              "address1", "hashPass1"));

            Employees.Add(new Employee("f1", "l1",
             "userName1", Gender.Male,
             new DateTime(2020, 2, 13),
             "phone1", "email1",
             "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));
            Employees.Add(new Employee("firstName1", "lastName1",
              "userName1", Gender.Male,
              new DateTime(2020, 1, 1),
              "phone1", "email1",
              "address1", "hashPass1"));

            Employees.Add(new Employee("f1", "l1",
             "userName1", Gender.Male,
             new DateTime(2020, 2, 13),
             "phone1", "email1",
             "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));

            Employees.Add(new Employee("firstName1", "lastName1",
               "userName1", Gender.Male,
               new DateTime(2020, 1, 1),
               "phone1", "email1",
               "address1", "hashPass1"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.ViewModels.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class EmployeesVM : ObservableValidator
    {
        public ObservableCollection<Employee> Employees { get; set; }

        #region ctor
        public EmployeesVM()
        {
            Employees = new ObservableCollection<Employee>();
            addEmployees();

            CommandAddNewEmployee = new RelayCommand(executeAddEmployeeAction);
            CommandDeleteEmployee = new RelayCommand(executeDeleteEmployeeAction);
            CommandSearch = new RelayCommand(executeSearchEmployeeAction);
            CommandFilterEmployee = new RelayCommand(executeFilterEmployeeAction);
        }

        private void addEmployees()
        {
            Employees.Add(new Employee("name",
                   "userName1", Gender.Male,
                   new DateTime(2020, 2, 13),
                   "cmnd", "phone",
                   "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
               "userName1", Gender.Male,
               new DateTime(2020, 2, 13),
               "cmnd", "phone",
               "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
               "userName1", Gender.Male,
               new DateTime(2020, 2, 13),
               "cmnd", "phone",
               "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

            Employees.Add(new Employee("name",
                "userName1", Gender.Male,
                new DateTime(2020, 2, 13),
                "cmnd", "phone",
                "address1"));

        }
        #endregion

        #region command
        public ICommand CommandFilterEmployee { get; }
        public ICommand CommandDeleteEmployee { get; }
        public ICommand CommandAddNewEmployee { get; }
        public ICommand CommandSearch { get; }

        public void executeFilterEmployeeAction()
        {
            MessageBox.Show("Filter");
        }
        public void executeSearchEmployeeAction()
        {
            MessageBox.Show("Search");
        }
        public void executeDeleteEmployeeAction()
        {
            MessageBox.Show("Open confirm delete message box");
        }
        public void executeAddEmployeeAction()
        {
            MessageBox.Show("Open add employee edit window");
        }
        #endregion
    }
}

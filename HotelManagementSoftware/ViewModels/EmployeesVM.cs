using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels.WindowVMs;
using HotelManagementSoftware.Business;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagementSoftware.ViewModels
{
    public class EmployeesVM : ObservableValidator
    {
        private EmployeeBusiness employeeBusiness;
        public ObservableCollection<Employee> Employees { get; set; } = new();

        #region ctor
        public EmployeesVM()
        {
            Employees = new ObservableCollection<Employee>();

            //addEmployeesss();

            CommandAddNewEmployee = new RelayCommand(executeAddEmployeeAction);
            CommandDeleteEmployee = new RelayCommand(executeDeleteEmployeeAction);
            CommandSearch = new RelayCommand(executeSearchEmployeeAction);
            CommandFilterEmployee = new RelayCommand(executeFilterEmployeeAction);
        }
        public EmployeesVM(EmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;

            addEmployeesss();

            CommandAddNewEmployee = new RelayCommand(executeAddEmployeeAction);
            CommandDeleteEmployee = new RelayCommand(executeDeleteEmployeeAction);
            CommandSearch = new RelayCommand(executeSearchEmployeeAction);
            CommandFilterEmployee = new RelayCommand(executeFilterEmployeeAction);
        }

        private async void addEmployeesss()
        {
            List<Employee> employees = await employeeBusiness.GetAllEmloyees();
            employees.ForEach(employee =>
            {
                Employees.Add(employee);
            });

        }

        private void addEmployees()
        {
            Employees.Add(new Employee("name",
                   "userName1", EmployeeType.Receptionist, Gender.Male,
                   new DateTime(2020, 2, 13),
                   "cmnd", "phone",
                   "address1"));

            Employees.Add(new Employee("name",
                      "userName1", EmployeeType.Receptionist, Gender.Male,
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
            EmployeeEditWindow employeeEditWindow=new EmployeeEditWindow();
            employeeEditWindow.ShowDialog();
        }
        #endregion
    }
}

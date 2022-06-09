using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.Business;
using System.Collections.Generic;
using HotelManagementSoftware.ViewModels.WindowVMs;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagementSoftware.ViewModels
{
    public class EmployeesVM : ObservableValidator
    {
        private EmployeeBusiness? employeeBusiness;
        //public ICollectionView EmployeeCollection { get; set; }

        private String? textFilter;
        public String? TextFilter
        {
            get { return textFilter; }
            set
            {
                textFilter = value;
  

                if (!String.IsNullOrEmpty(textFilter))
                {
                    if (SelectedComboboxItem.Equals(ComboboxFilterItem.Name))
                    {
                        GetEmployeesFilterByName();
                    }
                    else if (SelectedComboboxItem.Equals(ComboboxFilterItem.IDCardNumbers))
                    {
                        GetEmployeesFilterByIDCard();
                    }
                    else if (SelectedComboboxItem.Equals(ComboboxFilterItem.PhoneNumbers))
                    {
                        GetEmployeesFilterByPhone();
                    }

                }
                else 
                {
                    GetAllEmployees();
                }
            }
        }

        public ComboboxFilterItem SelectedComboboxItem { get; set; }
        public ObservableCollection<ComboboxFilterItem> ComboboxFilterItems { get; set; } = new();
        public ObservableCollection<Employee> Employees { get; set; } = new();
        public Employee SelectedEmployee { get; set; }

        #region ctor
        public EmployeesVM()
        {
            initCommand();
        }
        public EmployeesVM(EmployeeBusiness? _employeeBusiness)
        {   
            employeeBusiness = _employeeBusiness;
            // EmployeeCollection = CollectionViewSource.GetDefaultView(Employees);
            setUpCombobox();
            //EmployeeCollection.Filter = FilterByName;
            GetAllEmployees();
            initCommand();
        }
        private void setUpCombobox()
        {
            ComboboxFilterItems.Add(ComboboxFilterItem.Name);
            ComboboxFilterItems.Add(ComboboxFilterItem.IDCardNumbers);
            ComboboxFilterItems.Add(ComboboxFilterItem.PhoneNumbers);
        }
        private void initCommand()
        {
            CommandEditEmployee = new RelayCommand(executeEditEmployeeAction);
            CommandAddNewEmployee = new RelayCommand(executeAddEmployeeAction);
            CommandDeleteEmployee = new RelayCommand(executeDeleteEmployeeAction);
            CommandSearch = new RelayCommand(executeSearchEmployeeAction);
            CommandFilterEmployee = new RelayCommand(executeFilterEmployeeAction);
        }
        public async void GetAllEmployees()
        {
            if (employeeBusiness != null)
            {
                List<Employee> employees = await employeeBusiness.GetAllEmloyees();
                Employees.Clear();
                employees.ForEach(employee =>
                {
                    Employees.Add(employee);
                });

            }
        }
        #endregion

        #region command
        public ICommand? CommandFilterEmployee { get; set; }
        public ICommand? CommandDeleteEmployee { get; set; }
        public ICommand? CommandEditEmployee { get; set; }
        public ICommand? CommandAddNewEmployee { get; set; }
        public ICommand? CommandSearch { get; set; }

        public void executeFilterEmployeeAction()
        {
           
        }
        public void executeSearchEmployeeAction()
        {

        }
        public void executeDeleteEmployeeAction()
        {
            DeleteSelectedEmployee();
        }
        public void executeEditEmployeeAction()
        {
            EmployeeEditWindow employeeEditWindow = new EmployeeEditWindow();
            EmployeeEditWindowVM vm = App.Current.Services.GetRequiredService<EmployeeEditWindowVM>();
            vm.EmployeesVM = this;
            vm.Title = "Edit employee information window";
            employeeEditWindow.DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(employeeEditWindow.Close);
            }

            employeeEditWindow.ShowDialog();
        }
        public void executeAddEmployeeAction()
        {
            EmployeeEditWindow employeeEditWindow = new EmployeeEditWindow();
            EmployeeEditWindowVM vm = App.Current.Services.GetRequiredService<EmployeeEditWindowVM>();
            vm.EmployeesVM = this;
            vm.Title = "Add employee window";
            employeeEditWindow.DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(employeeEditWindow.Close);
            }

            employeeEditWindow.ShowDialog();
        }
        #endregion
        private async void DeleteSelectedEmployee()
        {
            MessageBoxResult result = MessageBox.Show("Delete this employee","Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (employeeBusiness != null && SelectedEmployee != null)
                {
                    await employeeBusiness.DeleteEmployee(SelectedEmployee);
                }
                GetAllEmployees();
            }
          
        }
        private async void GetEmployeesFilterByName()
        {
            if (employeeBusiness != null && TextFilter != null)
            {
                List<Employee> employees = await employeeBusiness.GetEmployeesByName(TextFilter.Trim());
                Employees.Clear();
                employees.ForEach(item =>
                {
                    Employees.Add(item);
                });
            }
        }
        private async void GetEmployeesFilterByIDCard()
        {
            if (employeeBusiness != null && TextFilter != null)
            {
                Employee? employee = await employeeBusiness.GetEmployeeByCmndNumber(TextFilter.Trim());
                Employees.Clear();

                if (employee != null)
                {
                    Employees.Add(employee);
                }
            }
        }
        private async void GetEmployeesFilterByPhone()
        {
            if (employeeBusiness != null && TextFilter != null)
            {
                Employee? employee = await employeeBusiness.GetEmployeeByPhoneNumber(TextFilter.Trim());
                Employees.Clear();

                if (employee != null)
                {
                    Employees.Add(employee);
                }
                    
            }
        }
    }

    public enum ComboboxFilterItem
    {
        Name,
        IDCardNumbers,
        PhoneNumbers
    }
}

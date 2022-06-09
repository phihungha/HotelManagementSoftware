using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using Microsoft.Toolkit.Mvvm.Input;
using HotelManagementSoftware.Data;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using HotelManagementSoftware.Business;
using System.Collections.ObjectModel;
using System.Windows;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class EmployeeEditWindowVM : ObservableValidator
    {
        private EmployeesVM employeesVM;
        private Employee? currentSelected;
        public EmployeesVM EmployeesVM
        {
            get => employeesVM;
            set
            {
                SetProperty(ref employeesVM, value, true);
            }
        }
        public Employee? CurrentSelected
        {
            get => currentSelected;
            set
            {
                SetProperty(ref currentSelected, value, true);
                if (CurrentSelected != null)
                {
                    Name = CurrentSelected.Name;
                    UserName = CurrentSelected.UserName;
                    Gender = CurrentSelected.Gender;
                    EmployeeType = CurrentSelected.EmployeeType;
                    BirthDate = CurrentSelected.BirthDate;
                    Cmnd = CurrentSelected.Cmnd;
                    PhoneNumber = CurrentSelected.PhoneNumber;
                    Email = CurrentSelected.Email;
                    Address = CurrentSelected.Address;

                }

            }
        }

        private EmployeeBusiness? employeeBusiness;
        public EmployeeEditWindowType EmployeeEditWindowType { get; set; }
        public ObservableCollection<EmployeeType> EmployeeTypes { get; set; } = new();
        public ObservableCollection<Gender> Genders { get; set; } = new();
        public String Title { get; set; }
        #region private variables
        private string name;
        private string userName;
        private Gender gender;
        private EmployeeType employeeType;
        private DateTime birthDate;
        private string cmnd;
        private string phoneNumber;
        private string? email;
        private string address;
        private string password;
        #endregion

        #region Property Validation
        [Required(ErrorMessage = "Name cannot be empty")]
        [MinLength(2, ErrorMessage = "Name cannot be shorter than 2 character")]
        [MaxLength(100, ErrorMessage = "Name should be shorter than 100 character")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, true);
        }

        [Required(ErrorMessage = "Username cannot be empty")]
        [MinLength(5, ErrorMessage = "Username cannot be shorter than 5 character")]
        [MaxLength(100, ErrorMessage = "Username should be shorter than 100 character")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]+$", ErrorMessage = "Username should be a letter")]
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value, true);
        }

        [Required(ErrorMessage = "Gender cannot be empty")]
        public Gender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value, true);
        }

        [Required(ErrorMessage = "Employee type cannot be empty")]
        public EmployeeType EmployeeType
        {
            get => employeeType;
            set => SetProperty(ref employeeType, value, true);
        }

        [Required(ErrorMessage = "Date of birth cannot be empty")]
        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value, true);
        }

        [Required(ErrorMessage = "ID cannot be empty")]
        [RegularExpression(@"^(\d{9}|\d{12})$", ErrorMessage = "Invalid ID")]
        public string Cmnd
        {
            get => cmnd;
            set => SetProperty(ref cmnd, value, true);
        }

        [Required(ErrorMessage = "Phone cannot be empty")]
        [RegularExpression(@"^(03|05|07|08|09|01[2|6|8|9])([0-9]{8})\b$", ErrorMessage = "Invalid phone")]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value, true);
        }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email")]
        public string? Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value, true);
        }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value, true);
        }
        #endregion

        #region ctor
        public EmployeeEditWindowVM(EmployeeBusiness? employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
            setUpCombobox();
            CommandCancel = new RelayCommand(cancel);
            CommandUpdate = new RelayCommand(updateEmployeeAction);
        }

        private void setUpCombobox()
        {
            EmployeeTypes.Add(EmployeeType.MaintenanceManager);
            EmployeeTypes.Add(EmployeeType.HousekeepingManager);
            EmployeeTypes.Add(EmployeeType.Receptionist);
            EmployeeTypes.Add(EmployeeType.Manager);

            Genders.Add(Gender.Male);
            Genders.Add(Gender.Female);
        }
        #endregion

        #region command
        public ICommand? CommandUpdate { get; }
        public ICommand? CommandCancel { get; }

        public void updateEmployeeAction()
        {
            if (EmployeeEditWindowType.Equals(EmployeeEditWindowType.Add))
            {
                AddEmployee();
            }
            else
            {
                EditEmployee();
            }
        }
        private async void EditEmployee()
        {
            try
            {
                ValidateAllProperties();
                if (CurrentSelected == null) return;
                Employee employee = CurrentSelected;
                employee.Name = Name;
                employee.UserName = UserName;
                employee.Gender = Gender;
                employee.EmployeeType = EmployeeType;
                employee.BirthDate = BirthDate;
                employee.Cmnd = Cmnd;
                employee.PhoneNumber = PhoneNumber;
                employee.Email = Email;
                employee.Address = Address;

                if (employeeBusiness != null && employee != null)
                {
                    await employeeBusiness.EditEmployee(employee);
                    EmployeesVM.GetAllEmployees();
                    CloseAction();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private async void AddEmployee()
        {
            try
            {
                ValidateAllProperties();
                Employee employee = new Employee(Name, UserName, EmployeeType, Gender, BirthDate, Cmnd, PhoneNumber, Address, Email);

                if (employeeBusiness != null && employee != null && Password != null)
                {
                    await employeeBusiness.CreateEmployee(employee, Password);
                    EmployeesVM.GetAllEmployees();
                    CloseAction();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void cancel()
        {
            CloseAction();
        }
        #endregion
        public Action CloseAction { get; set; }
    }

    public enum EmployeeEditWindowType
    {
        Add, Edit
    }
}

using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using HotelManagementSoftware.UI.Windows;
using HotelManagementSoftware.ViewModels.WindowVMs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Windows;

using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels
{
    public class MaintenanceVM : ObservableValidator
    {
        private MaintenanceBusiness? maintenanceBusiness;
        public ObservableCollection<MaintenanceRequest> MaintenanceRequestLists { get; set; } = new();
        public MaintenanceRequest SelectedItemMaintenanceRequest { get; set; }

        private String? textFilter;
        public String? TextFilter
        {
            get { return textFilter; }
            set
            {
                textFilter = value;

                if (!String.IsNullOrEmpty(textFilter))
                {
                    GetAllItemByRoom();
                }
                else
                {
                    GetAllItem();
                }
            }
        }

        #region ctor
        public MaintenanceVM(MaintenanceBusiness? maintenanceBusiness)
        {
            this.maintenanceBusiness = maintenanceBusiness;
            GetAllItem();
            initCommand();
        }
        private void initCommand()
        {
            CommandAddNewIssue = new RelayCommand(executeAddIssueAction);
            CommandSearch = new RelayCommand(executeSearchIssueAction);
            CommandEditNewIssue = new RelayCommand(executeEditIssueAction);
        }

        #endregion

        #region command
        public ICommand? CommandEditNewIssue { get; set; }
        public ICommand? CommandAddNewIssue { get; set; }
        public ICommand? CommandSearch { get; set; }
        public void executeSearchIssueAction()
        {
            
        }
        public void executeEditIssueAction()
        {
            MaintenanceEditWindow window = new MaintenanceEditWindow();
            MaintenanceEditWindowVM vm = App.Current.Services.GetRequiredService<MaintenanceEditWindowVM>();
            vm.MaintenanceVM = this;

            vm.MaintenanceRequestType = MaintenanceRequestType.Edit;
            vm.CurrentRequestId = SelectedItemMaintenanceRequest.MaintenanceRequestId;

            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(window.Close);
            }

            if (SelectedItemMaintenanceRequest.Status.Equals(MaintenanceRequestStatus.Closed))
            {
                vm.IsEnabled = false;
            }
            else
            {
                vm.IsEnabled = true;
            }

            window.DataContext = vm;
            window.ShowDialog();

        }
        public void executeAddIssueAction()
        {
            MaintenanceEditWindow window = new MaintenanceEditWindow();
            MaintenanceEditWindowVM vm = App.Current.Services.GetRequiredService<MaintenanceEditWindowVM>();
            vm.MaintenanceVM = this;

            vm.MaintenanceRequestType = MaintenanceRequestType.Add;
            vm.CurrentRequestId = null;

            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(window.Close);
            }
            window.DataContext = vm;

            window.ShowDialog();
        }
        #endregion

        private async void GetAllItemByRoom()
        {
            int room;
            bool canConvert = Int32.TryParse(TextFilter, out room);
            if (maintenanceBusiness != null && canConvert)
            {
                List<MaintenanceRequest> list = await maintenanceBusiness.GetMaintenanceRequests(roomNumber: room);
                MaintenanceRequestLists.Clear();
                list.ForEach(item =>
                {
                    MaintenanceRequestLists.Add(item);
                });
            }
        }
        public async void GetAllItem()
        {
            if (maintenanceBusiness != null)
            {
                List<MaintenanceRequest> list = await maintenanceBusiness.GetMaintenanceRequests();
                MaintenanceRequestLists.Clear();
                list.ForEach(item =>
                {
                    MaintenanceRequestLists.Add(item);
                });
            }
        }
    }
}

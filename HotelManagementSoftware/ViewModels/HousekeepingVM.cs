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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelManagementSoftware.ViewModels
{
    public class HousekeepingVM : ObservableValidator
    {
        private HousekeepingBusiness? housekeepingBusiness;
        public ObservableCollection<HousekeepingRequest> HouseKeepingLists { get; set; } = new();
        public HousekeepingRequest SelectedItemHouseKeepingRequest { get; set; }

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
        public HousekeepingVM(HousekeepingBusiness? housekeepingBusiness)
        {
            this.housekeepingBusiness = housekeepingBusiness;
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
            HousekeepingEditWindow window = new HousekeepingEditWindow();
            HousekeepingEditWindowVM vm = App.Current.Services.GetRequiredService<HousekeepingEditWindowVM>();
            vm.HousekeepingVM = this;

            vm.Title = "Edit housekeeping request window";
            vm.HousekeepingRequestType = HousekeepingRequestType.Edit;
            window.DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(window.Close);
            }

            vm.VisibilityCbx = Visibility.Hidden;
            vm.VisibilityTextbox = Visibility.Visible;
            if (SelectedItemHouseKeepingRequest.Status.Equals(HousekeepingRequestStatus.Closed))
            {
                vm.IsEnabled = false;
            } else
            {
                vm.IsEnabled = true;
            }


            window.ShowDialog();
        }
        public void executeAddIssueAction()
        {
            HousekeepingEditWindow window = new HousekeepingEditWindow();
            HousekeepingEditWindowVM vm = App.Current.Services.GetRequiredService<HousekeepingEditWindowVM>();

            vm.Title = "Add housekeeping request window";
            vm.HousekeepingRequestType = HousekeepingRequestType.Add;
            window.DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(window.Close);
            }

            vm.VisibilityCbx = Visibility.Visible;
            vm.VisibilityTextbox = Visibility.Hidden;
            vm.IsEnabled = true;

            window.ShowDialog();
        }
        #endregion

        private async void GetAllItemByRoom()
        {
            int room;
            bool canConvert = Int32.TryParse(TextFilter, out room);
            if (housekeepingBusiness != null && canConvert)
            {
                List<HousekeepingRequest> list = await housekeepingBusiness.GetHousekeepingRequests(roomNumber: room);
                HouseKeepingLists.Clear();
                list.ForEach(item =>
                {
                    HouseKeepingLists.Add(item);
                });
            }
        }
        public async void GetAllItem()
        {
            if (housekeepingBusiness != null)
            {
                List<HousekeepingRequest> list = await housekeepingBusiness.GetHousekeepingRequests();
                HouseKeepingLists.Clear();
                list.ForEach(item =>
                {
                    HouseKeepingLists.Add(item);
                });
            }
        }
    }
}

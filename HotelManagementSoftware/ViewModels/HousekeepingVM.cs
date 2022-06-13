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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelManagementSoftware.ViewModels
{
    public enum HouseKeepingSearchBy
    {
        [Description("Room number")]
        Room
    }

    public class HousekeepingVM : ObservableValidator
    {
        private HouseKeepingSearchBy searchBy = HouseKeepingSearchBy.Room;
        public HouseKeepingSearchBy SearchBy
        {
            get => searchBy;
            set => SetProperty(ref searchBy, value);
        }

        private HousekeepingBusiness housekeepingBusiness;
        public ObservableCollection<HousekeepingRequest> HouseKeepingLists { get; set; } = new();

        private string textFilter = "";
        public string TextFilter
        {
            get { return textFilter; }
            set => SetProperty(ref textFilter, value);
        }
        public HousekeepingVM(HousekeepingBusiness housekeepingBusiness)
        {
            this.housekeepingBusiness = housekeepingBusiness;
            GetAllItem();

            CommandSearch = new AsyncRelayCommand(Search);
        }

        public ICommand? CommandSearch { get; set; }
        private async Task Search()
        {
            if (textFilter == "")
                GetAllItem();
            else
            {
                switch (SearchBy)
                {
                    case HouseKeepingSearchBy.Room:
                        await GetAllItemByRoom();
                        break;
                }
            }
        }
        //public void executeEditIssueAction()
        //{
        //    HousekeepingEditWindow window = new HousekeepingEditWindow();
        //    HousekeepingEditWindowVM vm = App.Current.Services.GetRequiredService<HousekeepingEditWindowVM>();
        //    vm.HousekeepingVM = this;
        //    vm.HousekeepingRequestType = HousekeepingRequestType.Edit;
        //    vm.CurrentRequestId = SelectedItemHouseKeepingRequest.HousekeepingRequestId;
        //    if (vm.CloseAction == null)
        //    {
        //        vm.CloseAction = new Action(window.Close);
        //    }

        //    if (SelectedItemHouseKeepingRequest.Status.Equals(HousekeepingRequestStatus.Closed))
        //    {
        //        vm.IsEnabled = false;
        //    } else
        //    {
        //        vm.IsEnabled = true;
        //    }
        //    window.DataContext = vm;
        //    window.ShowDialog();
        //}
        //public void executeAddIssueAction()
        //{
        //    HousekeepingEditWindow window = new HousekeepingEditWindow();
        //    HousekeepingEditWindowVM vm = App.Current.Services.GetRequiredService<HousekeepingEditWindowVM>();
        //    vm.HousekeepingVM = this;
        //    vm.HousekeepingRequestType = HousekeepingRequestType.Add;
        //    vm.CurrentRequestId = null;
        //    if (vm.CloseAction == null)
        //    {
        //        vm.CloseAction = new Action(window.Close);
        //    }
        //    window.DataContext = vm;
        //    window.ShowDialog();
        //}
 
        public async Task GetAllItemByRoom()
        {
            int room;
            bool canConvert = Int32.TryParse(TextFilter.Trim(), out room);
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

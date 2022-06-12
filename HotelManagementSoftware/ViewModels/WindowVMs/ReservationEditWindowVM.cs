using HandyControl.Controls;
using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSoftware.ViewModels.WindowVMs
{
    public class ReservationEditWindowVM
    {
        private ReservationBusiness? reservationBusiness;
        public Room SelectedRoom { get; set; }
        public ReservationEditWindowType reservationEditWindowType { get; set; }
        private ReservationsVM reservationsVM;
        public ReservationsVM ReservationsVM
        {
            get => reservationsVM;
            set
            {
                SetProperty(ref reservationsVM, value);
                if (ReservationsVM != null)
                {
                    if (ReservationsVM.SelectedReservations != null)
                    {
                        CMND = ReservationsVM.SelectedReservations.Customer.IdNumber;
                        Name = ReservationsVM.SelectedReservations.Customer.Name;
                        Gender = ReservationsVM.SelectedReservations.Customer.Gender;
                        PhoneNumber = ReservationsVM.SelectedReservations.Customer.PhoneNumber;
                        Email = ReservationsVM.SelectedReservations.Customer.Email;
                        BirthDate = ReservationsVM.SelectedReservations.Customer.BirthDate;
                        Address = ReservationsVM.SelectedReservations.Customer.Address;
                        RoomNumber = ReservationsVM.SelectedReservations.Room.RoomNumber;
                        RoomType = ReservationsVM.SelectedReservations.Room.RoomType;
                        Note = ReservationsVM.SelectedReservations.Room.Note;
                        Floor = ReservationsVM.SelectedReservations.Room.Floor;
                        TotalPayment = ReservationsVM.reservationBusiness.GetTotalRentFee(ReservationsVM.SelectedReservations);
                        ArrivalTime = ReservationsVM.SelectedReservations.ArrivalTime;
                        DepartureTime = ReservationsVM.SelectedReservations.DepartureTime;
                        SelectedPaymentMethod = ReservationsVM.SelectedReservations.Customer.PaymentMethod;
                        CardNumber = ReservationsVM.SelectedReservations.Customer.CardNumber;
                    }
                }
            }
        }
        //Guest info
        public string CMND { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }

        //Room info
        public int RoomNumber { get; set; }
        public RoomType? RoomType { get; set; }
        public string? Note { get; set; }
        public int Floor { get; set; }
        public int TotalPayment { get; set; }
        // Stay info
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int NumOfDay { get; set; }
        public int Person { get; set; }

        //Payment info
        public PaymentMethod[] PaymentMethod { get; set; }
        public PaymentMethod SelectedPaymentMethod { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpireDate { get; set; }
        public ReservationEditWindowVM()
        {
            CommandChooseRoom = new RelayCommand(executeChooseRoom);
            CommandChooseCustomer = new RelayCommand(executeChooseCustomer);
            CommandChooseRoomType = new RelayCommand(executeChooseRoomType);
            CommandCancel = new RelayCommand(executeCancel);
            CommandSave = new RelayCommand(executeSave);
        }
        #region command
        public ICommand CommandChooseRoom { get; }
        public ICommand CommandChooseCustomer { get; }
        public ICommand CommandChooseRoomType { get; }
        public ICommand CommandSave { get; }
        public ICommand CommandCancel { get; }

        public void executeSave()
        {
            if (reservationEditWindowType == ReservationEditWindowType.Edit)
            {
                Reservation reservation = ReservationsVM.SelectedReservations;
                reservation.NumberOfPeople = Person;
                reservation.Customer.Email = Email;
                reservation.Customer.IdNumber = CMND;
                reservation.Customer.PhoneNumber = PhoneNumber;
                reservation.Customer.Address = Address;
                reservation.Customer.Name = Name;
                reservation.Customer.Gender = Gender;
                reservation.Customer.BirthDate = BirthDate;

                reservation.Room.RoomType = RoomType;
                reservation.Room.RoomNumber = RoomNumber;
                reservation.Room.Floor = Floor;
                reservation.Room.Note = Note;

                reservation.ArrivalTime = ArrivalTime;
                reservation.DepartureTime = DepartureTime;
                reservation.NumberOfPeople = Person;

                reservation.Customer.CardNumber = CardNumber;
                reservation.Customer.PaymentMethod = SelectedPaymentMethod;

                reservationBusiness.EditReservation(reservation);

            }
            else
            {

                
            }
            ReservationsVM.GetAllReservation();
            CloseAction();
        }
        public void executeCancel()
        {
            CloseAction();
        }
        public void executeChooseRoom()
        {

        }
        public void executeChooseCustomer()
        {

        }
        public void executeChooseRoomType()
        {

        }
        public Action CloseAction { get; set; }
        #endregion
        public enum ReservationEditWindowType
        {
            Add, Edit
        }
    }
}

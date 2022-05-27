using HotelManagementSoftware.Business;
using HotelManagementSoftware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Tests
{
    public static class TestDataGenerator
    {
        /// <summary>
        /// Generate test data. Uncomment the data that needs to be generated.
        /// </summary>
        public static async void GenerateTestData()
        {
            // await GenerateEmployees();
            // await GenerateRooms();
            // await GenerateCustomers();
            // await GenerateReservations();
        }

        private static async Task GenerateEmployees()
        {
            EmployeeBusiness employeeBusiness = new EmployeeBusiness();
            await employeeBusiness.CreateEmployee(new Employee("Nguyễn Lễ Tân",
                                                            "letan",
                                                            EmployeeType.Receptionist,
                                                            Gender.Male,
                                                            new DateTime(1985, 10, 10),
                                                            "123456789",
                                                            "0344250402",
                                                            "120 Lê Hồng Phong",
                                                            "letan@gmail.com"), "1234letan");
            await employeeBusiness.CreateEmployee(new Employee("Hà Quản Lý",
                                                         "quanly",
                                                         EmployeeType.Manager,
                                                         Gender.Female,
                                                         new DateTime(1968, 5, 10),
                                                         "987654321",
                                                         "0344250403",
                                                         "182/2 Lê Thành Phương",
                                                         "quanly@gmail.com"), "1234quanly");
            await employeeBusiness.CreateEmployee(new Employee("Lê Dọn Phòng",
                                                         "donphong",
                                                         EmployeeType.HousekeepingManager,
                                                         Gender.Male,
                                                         new DateTime(1976, 2, 1),
                                                         "123456789123",
                                                         "0344250404",
                                                         "5 Nguyễn Du",
                                                         "donphong@gmail.com"), "1234donphong");
            await employeeBusiness.CreateEmployee(new Employee("Vũ Sửa Phòng",
                                                         "suaphong",
                                                         EmployeeType.MaintenanceManager,
                                                         Gender.Female,
                                                         new DateTime(1990, 4, 5),
                                                         "321987654321",
                                                         "0344250405",
                                                         "1 Nguyễn Khuyến",
                                                         "suaphong@gmail.com"), "1234suaphong");
        }

        private static async Task GenerateCustomers()
        {
            CountryBusiness countryBusiness = new CountryBusiness();

            Country vietnam = (await countryBusiness.GetAllCountries())
                .First(i => i.Name == "Vietnam");
            CustomerBusiness customerBusiness = new CustomerBusiness();
            await customerBusiness.CreateCustomer(new Customer("Nguyen Van A",
                                                         new DateTime(1975, 4, 2),
                                                         IdNumberType.Cmnd,
                                                         "123456789",
                                                         Gender.Male,
                                                         "0344250406",
                                                         "110 Nguyễn Huệ",
                                                         "Quận 1",
                                                         "TP. HCM",
                                                         vietnam,
                                                         PaymentMethod.Cash));

            Country usa = (await countryBusiness.GetAllCountries())
                .First(i => i.Name == "United States");
            await customerBusiness.CreateCustomer(new Customer("Mary John",
                                                         new DateTime(2000, 10, 23),
                                                         IdNumberType.Passport,
                                                         "12345",
                                                         Gender.Female,
                                                         "1-844-872-4681",
                                                         "110 Mary Street",
                                                         "Lincoln",
                                                         "Nebraska",
                                                         usa,
                                                         PaymentMethod.Visa,
                                                         "123456",
                                                         DateTime.Now.AddYears(5)));
        }

        private static async Task GenerateRooms()
        {
            RoomTypeBusiness roomTypeBusiness = new RoomTypeBusiness();
            await roomTypeBusiness.AddRoomType(new RoomType("Normal",
                                                      2,
                                                      500000,
                                                      "Standard single two-person bed"));
            await roomTypeBusiness.AddRoomType(new RoomType("Deluxe",
                                                      4,
                                                      1000000,
                                                      "Two two-person bed"));
            await roomTypeBusiness.AddRoomType(new RoomType("Presidential",
                                                      2,
                                                      3000000,
                                                      "Luxury room with two-person bed"));

            RoomBusiness roomBusiness = new RoomBusiness();
            List<RoomType> roomTypes = await roomTypeBusiness.GetRoomTypes();

            RoomType normal = roomTypes.First(i => i.Name == "Normal");
            await roomBusiness.AddRoom(new Room(101, normal, 1));
            await roomBusiness.AddRoom(new Room(102, normal, 1));
            await roomBusiness.AddRoom(new Room(103, normal, 1));
            await roomBusiness.AddRoom(new Room(201, normal, 2));
            await roomBusiness.AddRoom(new Room(202, normal, 2));

            RoomType deluxe = roomTypes.First(i => i.Name == "Deluxe");
            await roomBusiness.AddRoom(new Room(104, deluxe, 1));
            await roomBusiness.AddRoom(new Room(105, deluxe, 1));
            await roomBusiness.AddRoom(new Room(106, deluxe, 1));
            await roomBusiness.AddRoom(new Room(203, deluxe, 2));
            await roomBusiness.AddRoom(new Room(204, deluxe, 2));

            RoomType presidential = roomTypes.First(i => i.Name == "Presidential");
            await roomBusiness.AddRoom(new Room(205, presidential, 2));
            await roomBusiness.AddRoom(new Room(206, presidential, 2));
        }

        private static async Task GenerateReservations()
        {
            EmployeeBusiness employeeBusiness = new EmployeeBusiness();
            Employee currentEmployee = (await employeeBusiness.GetEmployeesByName("Nguyễn Lễ Tân"))[0];

            RoomBusiness roomBusiness = new RoomBusiness();
            CustomerBusiness customerBusiness = new CustomerBusiness();
            ReservationBusiness reservationBusiness = new ReservationBusiness();

            DateTime arrivalTime = new DateTime(2022, 8, 5, 17, 0, 0);
            DateTime departureTime = new DateTime(2022, 8, 7, 19, 0, 0);
            Customer customer = (await customerBusiness.GetCustomersByName("Nguyen Van A"))[0];
            Room room = (await roomBusiness.GetUsableRooms("Normal", 1, arrivalTime, departureTime))[0];
            await reservationBusiness.CreateReservation(new Reservation(arrivalTime,
                                                                  departureTime,
                                                                  1,
                                                                  room,
                                                                  customer,
                                                                  currentEmployee), false);

            arrivalTime = DateTime.Now;
            departureTime = arrivalTime.Date.AddDays(5);
            customer = (await customerBusiness.GetCustomersByName("Mary John"))[0];
            room = (await roomBusiness.GetUsableRooms("Deluxe", 2, arrivalTime, departureTime))[0];
            await reservationBusiness.CreateReservation(new Reservation(arrivalTime,
                                                                  departureTime,
                                                                  4,
                                                                  room,
                                                                  customer,
                                                                  currentEmployee), true);
        }
    }
}

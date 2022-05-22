﻿// <auto-generated />
using System;
using HotelManagementSoftware.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelManagementSoftware.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HotelManagementSoftware.Data.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdNumberType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CountryId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("EmployeeTypeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.EmployeeType", b =>
                {
                    b.Property<int>("EmployeeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeTypeId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeTypeId");

                    b.ToTable("EmployeeTypes");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.HousekeepingRequest", b =>
                {
                    b.Property<int>("HousekeepingRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HousekeepingRequestId"), 1L, 1);

                    b.Property<int?>("CloseEmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OpenEmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HousekeepingRequestId");

                    b.HasIndex("CloseEmployeeId");

                    b.HasIndex("OpenEmployeeId");

                    b.HasIndex("RoomId");

                    b.ToTable("HousekeepingRequests");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.MaintenanceItem", b =>
                {
                    b.Property<int>("MaintenanceItemId")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("MaintenanceItemId", "MaintenanceRequestId");

                    b.HasIndex("MaintenanceRequestId");

                    b.ToTable("MaintenanceItems");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.MaintenanceRequest", b =>
                {
                    b.Property<int>("MaintenanceRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaintenanceRequestId"), 1L, 1);

                    b.Property<int?>("CloseEmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OpenEmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaintenanceRequestId");

                    b.HasIndex("CloseEmployeeId");

                    b.HasIndex("OpenEmployeeId");

                    b.HasIndex("RoomId");

                    b.ToTable("MaintenanceRequests");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PayTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"), 1L, 1);

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.ReservationCancelFeePercent", b =>
                {
                    b.Property<int>("DayNumberBeforeArrival")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DayNumberBeforeArrival"), 1L, 1);

                    b.Property<int>("PercentOfTotal")
                        .HasColumnType("int");

                    b.HasKey("DayNumberBeforeArrival");

                    b.ToTable("ReservationCancelFeePercents");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"), 1L, 1);

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int?>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.RoomType", b =>
                {
                    b.Property<int>("RoomTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomTypeId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("RoomTypeId");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Customer", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.Country", "Country")
                        .WithMany("Reservations")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Employee", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.EmployeeType", "EmployeeType")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeTypeId");

                    b.Navigation("EmployeeType");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.HousekeepingRequest", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.Employee", "CloseEmployee")
                        .WithMany("ClosedHousekeepingRequests")
                        .HasForeignKey("CloseEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HotelManagementSoftware.Data.Employee", "OpenEmployee")
                        .WithMany("OpenedHousekeepingRequests")
                        .HasForeignKey("OpenEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HotelManagementSoftware.Data.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("CloseEmployee");

                    b.Navigation("OpenEmployee");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.MaintenanceItem", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.MaintenanceRequest", "MaintenanceRequest")
                        .WithMany("MaintenanceItems")
                        .HasForeignKey("MaintenanceRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaintenanceRequest");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.MaintenanceRequest", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.Employee", "CloseEmployee")
                        .WithMany("ClosedMaintenanceRequests")
                        .HasForeignKey("CloseEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HotelManagementSoftware.Data.Employee", "OpenEmployee")
                        .WithMany("OpenedMaintenanceRequests")
                        .HasForeignKey("OpenEmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HotelManagementSoftware.Data.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("CloseEmployee");

                    b.Navigation("OpenEmployee");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Order", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.Reservation", "Reservation")
                        .WithMany("Orders")
                        .HasForeignKey("ReservationId");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Reservation", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId");

                    b.HasOne("HotelManagementSoftware.Data.Employee", "Employee")
                        .WithMany("Reservations")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HotelManagementSoftware.Data.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Room", b =>
                {
                    b.HasOne("HotelManagementSoftware.Data.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Country", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Employee", b =>
                {
                    b.Navigation("ClosedHousekeepingRequests");

                    b.Navigation("ClosedMaintenanceRequests");

                    b.Navigation("OpenedHousekeepingRequests");

                    b.Navigation("OpenedMaintenanceRequests");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.EmployeeType", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.MaintenanceRequest", b =>
                {
                    b.Navigation("MaintenanceItems");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Reservation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelManagementSoftware.Data.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware.Data
{
    public class Database : DbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<PaymentMethod> PaymentMethods { get; set; }
        DbSet<IdNumberType> IdNumbers { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<HousekeepingRequest> HousekeepingRequests { get; set; }
        DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        DbSet<MaintenanceItem> MaintenanceItems { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeType> EmployeeTypes { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<RoomType> RoomTypes { get; set; }

        public Database()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Database=HotelDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaintenanceItem>()
                .HasKey(i => new { i.MaintenanceItemId, i.MaintenanceRequestId });

            modelBuilder.Entity<MaintenanceRequest>()
                .HasOne(i => i.OpenEmployee)
                .WithMany(i => i.OpenedMaintenanceRequests)
                .HasForeignKey(i => i.OpenEmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MaintenanceRequest>()
                .HasOne(i => i.CloseEmployee)
                .WithMany(i => i.ClosedMaintenanceRequests)
                .HasForeignKey(i => i.CloseEmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HousekeepingRequest>()
                .HasOne(i => i.OpenEmployee)
                .WithMany(i => i.OpenedHousekeepingRequests)
                .HasForeignKey(i => i.OpenEmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HousekeepingRequest>()
                .HasOne(i => i.CloseEmployee)
                .WithMany(i => i.ClosedHousekeepingRequests)
                .HasForeignKey(i => i.CloseEmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

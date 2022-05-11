using Microsoft.EntityFrameworkCore;

namespace HotelManagementSoftware.Data
{
    public class Database : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<HousekeepingRequest> HousekeepingRequests => Set<HousekeepingRequest>();
        public DbSet<MaintenanceRequest> MaintenanceRequests => Set<MaintenanceRequest>();
        public DbSet<MaintenanceItem> MaintenanceItems => Set<MaintenanceItem>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeType> EmployeeTypes => Set<EmployeeType>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();

        public Database()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Database=HotelDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .Property(i => i.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .Property(i => i.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Room>()
                .Property(i => i.Status)
                .HasConversion<string>();

            modelBuilder.Entity<MaintenanceRequest>()
                .Property(i => i.Status)
                .HasConversion<string>();

            modelBuilder.Entity<HousekeepingRequest>()
                .Property(i => i.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Customer>()
                .Property(i => i.PaymentMethod)
                .HasConversion<string>();

            modelBuilder.Entity<Customer>()
                .Property(i => i.IdNumberType)
                .HasConversion<string>();

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

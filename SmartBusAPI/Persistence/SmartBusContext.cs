using SmartBusAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartBusAPI.Persistence
{
    public class SmartBusContext : DbContext
    {
        public SmartBusContext(DbContextOptions<SmartBusContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<BusDriver> BusDrivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Bus)
                .WithMany(b => b.Students)
                .HasForeignKey(s => s.BusID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Parent>()
                .HasMany(p => p.Students)
                .WithOne(s => s.Parent)
                .HasForeignKey(s => s.ParentID);

            modelBuilder.Entity<Parent>()
                .HasMany(p => p.Notifications)
                .WithOne(n => n.Parent)
                .HasForeignKey(n => n.ParentID);

            modelBuilder.Entity<Administrator>()
                .HasMany(a => a.Buses)
                .WithOne(b => b.Monitor)
                .HasForeignKey(b => b.MonitorID);

            modelBuilder.Entity<Bus>()
                .HasOne(b => b.BusDriver)
                .WithOne(d => d.Bus)
                .HasForeignKey<BusDriver>(d => d.BusID);

            modelBuilder.Entity<Bus>()
                .HasOne(b => b.Monitor)
                .WithMany(a => a.Buses)
                .HasForeignKey(b => b.MonitorID);

            modelBuilder.Entity<Bus>()
                .HasMany(b => b.Notifications)
                .WithOne(n => n.Bus)
                .HasForeignKey(n => n.BusID);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Parent)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.ParentID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Bus)
                .WithMany(b => b.Notifications)
                .HasForeignKey(n => n.BusID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BusDriver>()
                .HasOne(d => d.Bus)
                .WithOne(b => b.BusDriver)
                .HasForeignKey<Bus>(b => b.DriverID);

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            List<Administrator> administrators = new()
            {
                new Administrator()
                {
                    ID = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "SmartBus_Admin@outlook.com",
                    Password = "f8a0f873783fd0f4e6ac1268f21403a19a794e75b625813be6f3b0c45fec4c19"
                }
            };

            modelBuilder.Entity<Administrator>().HasData(administrators);

            List<Bus> buses = new()
            {
                new Bus()
                {
                    ID = 1,
                    BusNumber = "29812",
                    CurrentLocation = "",
                    Capacity = 50,
                    MonitorID = 1,
                    DriverID = null
                },
                new Bus()
                {
                    ID = 2,
                    BusNumber = "91375",
                    CurrentLocation = "",
                    Capacity = 50,
                    MonitorID = 1,
                    DriverID = null
                },
            };

            List<BusDriver> busDrivers = new()
            {
                new BusDriver()
                {
                    ID = 1,
                    FirstName = "Mohammed",
                    LastName = "Ali",
                    Email = "Mohammed_Ali@outlook.com",
                    DriverID = "D-202319891",
                    PhoneNumber = "+971501234567",
                    Country = "Pakistan",
                    Password = "2ee1d2bb7f631dda22824cd8b8d44fa4f0e9aec6f0e14f2141605e9a37630c02",
                    BusID = 1
                },
                new BusDriver()
                {
                    ID = 2,
                    FirstName = "Siti",
                    LastName = "Nurhikmah",
                    Email = "Siti_Nurhikmah@outlook.com",
                    DriverID = "D-202319951",
                    PhoneNumber = "+971501234567",
                    Country = "Indonesia",
                    Password = "d23c73ec1cbc838b8eef44092f8072aea15c621d883d9ce4ff218b938f42cd93",
                    BusID = 2
                },
            };

            foreach (BusDriver busDriver in busDrivers)
            {
                Bus bus = buses.FirstOrDefault(b => b.ID == busDriver.BusID);
                if (bus != null)
                {
                    bus.DriverID = busDriver.ID;
                }
            }

            modelBuilder.Entity<Bus>().HasData(buses);
            modelBuilder.Entity<BusDriver>().HasData(busDrivers);
        }
    }
}
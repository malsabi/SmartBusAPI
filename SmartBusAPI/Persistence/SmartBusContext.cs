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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Bus)
                .WithMany(b => b.Students)
                .HasForeignKey(s => s.BusID);

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
                .WithMany(d => d.Buses)
                .HasForeignKey(b => b.DriverID);

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
                .HasForeignKey(n => n.ParentID);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Bus)
                .WithMany(b => b.Notifications)
                .HasForeignKey(n => n.BusID);

            modelBuilder.Entity<BusDriver>()
                .HasMany(d => d.Buses)
                .WithOne(b => b.BusDriver)
                .HasForeignKey(b => b.DriverID);
        }
    }
}
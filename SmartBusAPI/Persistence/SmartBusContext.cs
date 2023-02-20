namespace SmartBusAPI.Persistence
{
    public class SmartBusContext : DbContext
    {
        public SmartBusContext(DbContextOptions<SmartBusContext> options) : base(options)
        {
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusDriver> BusDrivers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            List<Bus> buses = new()
            {
                new Bus()
                {
                    ID = 1,
                    LicenseNumber = 29812,
                    CurrentLocation = "",
                    Capacity = 50,
                    IsInService = false
                },
                new Bus()
                {
                    ID = 2,
                    LicenseNumber = 91375,
                    CurrentLocation = "",
                    Capacity = 50,
                    IsInService = false
                }
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
                    DriverID = "D-2023199511",
                    PhoneNumber = "+971501234567",
                    Country = "Indonesia",
                    Password = "d23c73ec1cbc838b8eef44092f8072aea15c621d883d9ce4ff218b938f42cd93",
                    BusID = 2
                },
            };

            List<Parent> parents = new()
            {
                new Parent()
                {
                    ID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "JohnDoe@gmail.com",
                    PhoneNumber = "+971501234567",
                    Address = "Sharjah - United Arab Emirates",
                    Password = "63d65bfe030ff5cbaac27bb8c9215bf7b1c635b3a8ed7ee9ad45eccf9e4b2e2f"
                }
            };

            List<Student> students = new()
            {
                new Student()
                {
                    ID = 1,
                    Image = "",
                    FirstName = "Mikel",
                    LastName = "Doe",
                    Gender = "Male",
                    GradeLevel = 6,
                    Address = "Sharjah - United Arab Emirates",
                    BelongsToBusID = 1,
                    IsAtSchool = false,
                    IsOnBus = false,
                    IsAtHome = true,
                    ParentID = 1,
                }
            };

            modelBuilder.Entity<Administrator>().HasData(administrators);
            modelBuilder.Entity<Parent>().HasData(parents);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Bus>().HasData(buses);
            modelBuilder.Entity<BusDriver>().HasData(busDrivers);
        }
    }
}
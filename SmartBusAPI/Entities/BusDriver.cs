﻿namespace SmartBusAPI.Entities
{
    public class BusDriver
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DriverID { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        
        public int? BusID { get; set; }
    }
}
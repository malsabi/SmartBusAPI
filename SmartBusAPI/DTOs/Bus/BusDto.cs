﻿namespace SmartBusAPI.DTOs.Bus
{
    public class BusDto
    {
        public int ID { get; set; }
        public int LicenseNumber { get; set; }
        public string CurrentLocation { get; set; }
        public int Capacity { get; set; }
        public bool IsInService { get; set; }
    }
}
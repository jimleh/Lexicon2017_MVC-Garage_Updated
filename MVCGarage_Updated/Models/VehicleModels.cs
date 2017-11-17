using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage_Updated.Models
{
    public enum VehicleType
    {
        MC, Car, Truck, Bus
    }

    public class Vehicle
    {
        [Key, Display(Name="ID")]
        public int VehicleID { get; set; }
        [Required, Display(Name="Registration Number")]
        public string VehicleRegNum { get; set; }
        [Required, Display(Name = "Owner")]
        public string VehicleOwner { get; set; }
        [Required, Display(Name = "Type")]
        public VehicleType VehicleType { get; set; }
        [Required, Display(Name = "Size")]
        public int VehicleSize { get; set; }
        [Required, Display(Name="Date Parked")]
        public string VehicleDateParked { get; set; }
        [Required, Display(Name="Parking Spot")]
        public int VehicleParkingSpot { get; set; }
        [Display(Name = "Fee")]
        public double VehicleFee { get; set; }
        [Display(Name = "Date Checkout")]
        public string VehicleDateCheckout { get; set; }

        public Vehicle()
        {
            VehicleDateParked = DateTime.Now.ToString();
        }
    }

    public class MC : Vehicle
    {
        public MC()
        {
            this.VehicleType = VehicleType.MC;
            VehicleSize = 1;
        }
    }
    public class Car : Vehicle
    {
        public Car()
        {
            this.VehicleType = VehicleType.Car;
            VehicleSize = 1;
        }
    }
    public class Truck : Vehicle
    {
        public Truck()
        {
            this.VehicleType = VehicleType.Truck;
            VehicleSize = 2;
        }
    }
    public class Bus : Vehicle
    {
        public Bus()
        {
            this.VehicleType = VehicleType.Bus;
            VehicleSize = 3;
        }
    }

    public class VehicleViewModel
    {
        public int ID { get; set; }
        public string Reg { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public int Spot { get; set; }

        public VehicleViewModel()
        {
            Date = DateTime.Now.ToString();
        }
        public VehicleViewModel(Vehicle vehicle)
        {
            ID = vehicle.VehicleID;
            Reg = vehicle.VehicleRegNum;
            Type = vehicle.VehicleType.ToString();
            Date = vehicle.VehicleDateParked;
            Spot = vehicle.VehicleParkingSpot;
            Owner = vehicle.VehicleOwner;
        }
    }
}
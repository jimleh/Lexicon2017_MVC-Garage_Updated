using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage_Updated.Models
{
    public enum VehicleType
    {
        MC = 1, Car = 1, Truck = 2, Bus = 3
    }

    public class Vehicle
    {
        [Key, Display(Name="ID")]
        public int VehicleID { get; set; }
        [Required, Display(Name="Registration Number")]
        public string VehicleNum { get; set; }
        [Required, Display(Name = "Type")]
        public VehicleType VehicleType { get; set; }
        [Required, Display(Name = "Size")]
        public int VehicleSize { get; set; }
        [Required, Display(Name="Date")]
        public string VehicleDate { get; set; }

        public Vehicle()
        {
            VehicleDate = DateTime.Now.ToString();
        }
    }

    public class MC : Vehicle
    {
        public MC()
        {
            this.VehicleType = VehicleType.MC;
            VehicleSize = (int)this.VehicleType;
        }
    }
    public class Car : Vehicle
    {
        public Car()
        {
            this.VehicleType = VehicleType.Car;
            VehicleSize = (int)this.VehicleType;
        }
    }
    public class Truck : Vehicle
    {
        public Truck()
        {
            this.VehicleType = VehicleType.Truck;
            VehicleSize = (int)this.VehicleType;
        }
    }
    public class Bus : Vehicle
    {
        public Bus()
        {
            this.VehicleType = VehicleType.Bus;
            VehicleSize = (int)this.VehicleType;
        }
    }
}
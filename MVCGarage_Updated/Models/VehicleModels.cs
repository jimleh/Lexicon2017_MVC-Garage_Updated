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
        [Required]
        public int ID { get; set; }
        [Required]
        public string Reg { get; set; }
        [Required]
        public VehicleType Type { get; set; }
    }
}
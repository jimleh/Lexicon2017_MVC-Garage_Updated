using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGarage_Updated.Models
{
    public class GarageViewModel
    {
        public bool[,,] ParkingSpots { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; }
    }
}
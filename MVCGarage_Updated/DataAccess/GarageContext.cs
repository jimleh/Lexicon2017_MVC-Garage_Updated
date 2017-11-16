using MVCGarage_Updated.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCGarage_Updated.DataAccess
{
    public class GarageContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public GarageContext() : base("DefaultConnection") {}
    }
}
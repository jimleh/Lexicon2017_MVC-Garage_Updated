namespace MVCGarage_Updated.Migrations
{
    using MVCGarage_Updated.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCGarage_Updated.DataAccess.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCGarage_Updated.DataAccess.GarageContext context)
        {
            context.Vehicles.AddOrUpdate(
                v=>v.VehicleID,
                    new Bus { VehicleID = 1, VehicleNum = "ABC-123" },
                    new Truck { VehicleID = 2, VehicleNum = "DEF-456" },
                    new Car { VehicleID = 3, VehicleNum = "GHI-789" }
                );
        }
    }
}

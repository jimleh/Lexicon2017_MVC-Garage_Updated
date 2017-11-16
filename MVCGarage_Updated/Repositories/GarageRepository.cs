using MVCGarage_Updated.DataAccess;
using MVCGarage_Updated.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCGarage_Updated.Repositories
{
    public class GarageRepository
    {
        GarageContext context;
        public GarageRepository()
        {
            context = new GarageContext();
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return context.Vehicles;
        }

        public Vehicle GetVehicle(int id)
        {
            return context.Vehicles.FirstOrDefault(v => v.VehicleID == id);
        }

        public void AddVehicle(VehicleViewModel vm)
        {
            Vehicle vehicle = null;
            switch(vm.Type)
            {
                case VehicleType.MC:
                    vehicle = new MC { VehicleID = vm.ID, VehicleNum = vm.Reg };
                    break;
                case VehicleType.Car:
                    vehicle = new Car { VehicleID = vm.ID, VehicleNum = vm.Reg };
                    break;
                case VehicleType.Truck:
                    vehicle = new Truck { VehicleID = vm.ID, VehicleNum = vm.Reg };
                    break;
                case VehicleType.Bus:
                    vehicle = new Bus { VehicleID = vm.ID, VehicleNum = vm.Reg };
                    break;
                default:
                    vehicle = null;
                    break;
            }

            if(vehicle != null)
            {
                context.Vehicles.Add(vehicle);
                context.SaveChanges();
            }

        }
        public void RemoveVehicle(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
        }
        public void EditVehicle(Vehicle vehicle)
        {
            context.Entry(vehicle).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
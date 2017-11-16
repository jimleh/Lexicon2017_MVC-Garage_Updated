using MVCGarage_Updated.DataAccess;
using MVCGarage_Updated.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCGarage_Updated.Repositories
{
    public class GarageRepository
    {
        GarageContext context;
        public GarageRepository()
        {
            context = new GarageContext();
        }

        public IEnumerable<VehicleViewModel> GetAllVehicles()
        {
            var vehicles = new List<VehicleViewModel>();

            foreach(var vehicle in context.Vehicles)
            {
                vehicles.Add(new VehicleViewModel(vehicle));
            }

            return vehicles;
        }

        public Vehicle GetVehicle(int id)
        {
            return context.Vehicles.FirstOrDefault(v => v.VehicleID == id);
        }

        public IEnumerable<string> GetVehicleTypes()
        {
            var types = new List<string>();
            foreach(var type in Enum.GetValues(typeof(VehicleType)))
            {
                types.Add(type.ToString());
            }

            return types;
        }

        protected VehicleViewModel CreateViewModel(Vehicle vehicle)
        {
            return new VehicleViewModel(vehicle);
        }

        protected Vehicle ToVehicle(VehicleViewModel vm)
        {
            switch (vm.Type)
            {
                case "MC":
                    return new MC { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDate = vm.Date };
                case "Car":
                    return new Car { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDate = vm.Date };
                case "Truck":
                    return new Truck { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDate = vm.Date };
                case "Bus":
                    return new Bus { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDate = vm.Date };
                default:
                    return null;
            }
        }

        public void AddVehicle(VehicleViewModel vm)
        {
            Vehicle vehicle = ToVehicle(vm);

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
        public void EditVehicle(VehicleViewModel vm)
        {
            var vehicle = GetVehicle(vm.ID);
            var newVehicle = ToVehicle(vm);
            foreach(var prop in vehicle.GetType().GetProperties())
            {
                prop.SetValue(vehicle, prop.GetValue(newVehicle));
            }
            context.Entry(vehicle).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
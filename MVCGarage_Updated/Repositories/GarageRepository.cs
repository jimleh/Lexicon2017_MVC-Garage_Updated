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
        bool[, ,] parkingSpots;
        public GarageRepository()
        {
            context = new GarageContext();
            parkingSpots = new bool[2, 10, 10];
            InitParkingSpots();
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return context.Vehicles;
        }

        public IEnumerable<VehicleViewModel> GetAllVehiclesVM()
        {
            return GetAllVehicles().Select(v => new VehicleViewModel(v)).ToList();
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
                    return new MC { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDateParked = vm.Date, VehicleOwner = vm.Owner };
                case "Car":
                    return new Car { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDateParked = vm.Date, VehicleOwner = vm.Owner };
                case "Truck":
                    return new Truck { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDateParked = vm.Date, VehicleOwner = vm.Owner };
                case "Bus":
                    return new Bus { VehicleID = vm.ID, VehicleRegNum = vm.Reg, VehicleDateParked = vm.Date, VehicleOwner = vm.Owner };
                default:
                    return null;
            }
        }

        public void AddVehicle(VehicleViewModel vm)
        {
            Vehicle vehicle = ToVehicle(vm);
            vehicle.VehicleParkingSpot = GetFreeParkingSpot(vehicle.VehicleSize);

            if(vehicle != null)
            {
                context.Vehicles.Add(vehicle);
                context.SaveChanges();
            }
        }
        public void RemoveVehicle(Vehicle vehicle)
        {
            FreeUpParkingSpots(vehicle.VehicleParkingSpot, vehicle.VehicleSize);
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

        // Methods to handle parking spots
        protected void InitParkingSpots()
        {
            int index = 0;
            for (int i = 0; i < parkingSpots.GetLength(0); i++)
            {
                for (int j = 0; j < parkingSpots.GetLength(1); j++)
                {
                    for (int k = 0; k < parkingSpots.GetLength(2); k++)
                    {
                        index++;
                        var tmp = context.Vehicles.FirstOrDefault(v => v.VehicleParkingSpot == index);
                        if (tmp != null)
                        {
                            for (int l = 0; l < tmp.VehicleSize; l++)
                            {
                                parkingSpots[i, j, k + l] = true;
                            }
                        }
                    }
                }
            }
        }
        protected int GetFreeParkingSpot(int size)
        {
            int index = 1;

            for (int i = 0; i < parkingSpots.GetLength(0); i++)
            {
                for (int j = 0; j < parkingSpots.GetLength(1); j++)
                {
                    for (int k = 0; k < parkingSpots.GetLength(2); k++)
                    {
                        if (CheckIfVehicleCanFitInParkingSpot(i, j, k, size))
                        {
                            for (int l = 0; l < size; l++)
                            {
                                parkingSpots[i, j, k + l] = true;
                            }
                            return index;
                        }
                        index++;
                    }
                }
            }
            return -1;
        }
        protected bool CheckIfVehicleCanFitInParkingSpot(int x, int y, int start, int size)
        {
            for (int i = start; i < start + size; i++)
            {
                if (i >= parkingSpots.GetLength(2) || parkingSpots[x, y, i])
                {
                    return false;
                }
            }
            return true;
        }
        protected void FreeUpParkingSpots(int spot, int size)
        {
            int index = 1;
            for (int i = 0; i < parkingSpots.GetLength(0); i++)
            {
                for (int j = 0; j < parkingSpots.GetLength(1); j++)
                {
                    for (int k = 0; k < parkingSpots.GetLength(2); k++)
                    {
                        if(index == spot)
                        {
                            for(int l = k; l < size; l++)
                            {
                                parkingSpots[i, j, l] = false;
                            }
                        }
                        index++;
                    }
                }
            }
        }

        public GarageViewModel GetGarageViewModel()
        {
            return new GarageViewModel
            {
                ParkingSpots = parkingSpots,
                Vehicles = GetAllVehiclesVM().ToList()
            };
        }
    }
}
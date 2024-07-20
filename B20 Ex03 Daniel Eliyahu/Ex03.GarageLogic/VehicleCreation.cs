using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class VehicleCreation
    {
        private enum eSupportedVehicleTypes
        {
            FuelBasedMotorcycle = 1,
            ElectricMotorcycle = 2,
            FuelBasedCar = 3,
            ElectricCar = 4,
            FuelBasedTruck = 5
        }

        public static string[] GetArrayOfSupportedVehicleTypes()
        {
            string[] supportedVehicleTypes = Enum.GetNames(typeof(eSupportedVehicleTypes));
            return supportedVehicleTypes;
        }

        public static Dictionary<string, string> CreateVehicleDetailsDictionary(string i_VehicleLicense, int i_VehicleType)
        {
            Dictionary<string, string> vehicleDictionary = createVehicleDetailsDictionary(i_VehicleLicense);
            eSupportedVehicleTypes vehicleType = (eSupportedVehicleTypes)i_VehicleType;

            switch (vehicleType)
            {
                case eSupportedVehicleTypes.FuelBasedMotorcycle:
                    addEnergyEntries(vehicleDictionary, "Octane95", "7");
                    addMotorcycleKeys(vehicleDictionary);
                    break;
                case eSupportedVehicleTypes.ElectricMotorcycle:
                    addEnergyEntries(vehicleDictionary, "Electricity", "1.2");
                    addMotorcycleKeys(vehicleDictionary);
                    break;
                case eSupportedVehicleTypes.FuelBasedCar:
                    addEnergyEntries(vehicleDictionary, "Octane96", "60");
                    addCarKeys(vehicleDictionary);
                    break;
                case eSupportedVehicleTypes.ElectricCar:
                    addEnergyEntries(vehicleDictionary, "Electricity", "2.1");
                    addCarKeys(vehicleDictionary);
                    break;
                case eSupportedVehicleTypes.FuelBasedTruck:
                    addEnergyEntries(vehicleDictionary, "Soler", "120");
                    addTruckKeys(vehicleDictionary);
                    break;
            }

            return vehicleDictionary;
        }

        private static Dictionary<string, string> createVehicleDetailsDictionary(string i_VehicleLicense)
        {
            Dictionary<string, string> vehicleDictionary = new Dictionary<string, string>();
            vehicleDictionary.Add("License", i_VehicleLicense);

            List<string> keys = ClientTicket.GetClientTicketKeys();
            keys.AddRange(Vehicle.GetVehicleKeys());
            keys.AddRange(Wheel.GetWheelsKeys());

            foreach (string key in keys)
            {
                vehicleDictionary.Add(key, null);
            }

            return vehicleDictionary;
        }

        private static void addEnergyEntries(Dictionary<string, string> i_VehicleDictionary, string i_EnergyType, string i_MaximumEnergyAmount)
        {
            i_VehicleDictionary.Add("Energy type", i_EnergyType);

            if (i_EnergyType == "Electricity")
            {
                i_VehicleDictionary.Add("Remaining time of engine operation (in hours)", null);
                i_VehicleDictionary.Add("Maximum time of engine operation (in hours)", i_MaximumEnergyAmount);
            }
            else
            {
                i_VehicleDictionary.Add("Current amount of fuel (in liters)", null);
                i_VehicleDictionary.Add("Maximum amount of fuel (in liters)", i_MaximumEnergyAmount);
            }
        }

        private static void addMotorcycleKeys(Dictionary<string, string> i_VehicleDictionary)
        {
            List<string> motorcycleKeys = Motorcycle.GetMotorcycleKeys();

            for (int i = 0; i < motorcycleKeys.Count; i++)
            {
                i_VehicleDictionary.Add(motorcycleKeys[i], null);
            }
        }

        private static void addCarKeys(Dictionary<string, string> i_VehicleDictionary)
        {
            List<string> carKeys = Car.GetCarKeys();

            for (int i = 0; i < carKeys.Count; i++)
            {
                i_VehicleDictionary.Add(carKeys[i], null);
            }
        }

        private static void addTruckKeys(Dictionary<string, string> i_VehicleDictionary)
        {
            List<string> truckKeys = Truck.GetTruckKeys();

            for (int i = 0; i < truckKeys.Count; i++)
            {
                i_VehicleDictionary.Add(truckKeys[i], null);
            }
        }

        public static Vehicle CreateVehicle(int i_VehicleType, Dictionary<string, string> io_VehicleDictionary)
        {
            Vehicle newVehicleToReturn = null;
            eSupportedVehicleTypes vehicleType = (eSupportedVehicleTypes)i_VehicleType;

            switch (vehicleType)
            {
                case eSupportedVehicleTypes.FuelBasedMotorcycle:
                case eSupportedVehicleTypes.ElectricMotorcycle:
                    newVehicleToReturn = createMotorcycle(io_VehicleDictionary);
                    break;
                case eSupportedVehicleTypes.FuelBasedCar:
                case eSupportedVehicleTypes.ElectricCar:
                    newVehicleToReturn = createCar(io_VehicleDictionary);
                    break;
                case eSupportedVehicleTypes.FuelBasedTruck:
                    newVehicleToReturn = createTruck(io_VehicleDictionary);
                    break;
            }

            return newVehicleToReturn;
        }

        private static Motorcycle createMotorcycle(Dictionary<string, string> io_VehicleDictionary)
        {
            io_VehicleDictionary.Add("Number of wheels", "2");
            io_VehicleDictionary.Add("Wheels maximum air pressure", "32");
            return new Motorcycle(io_VehicleDictionary);
        }

        private static Car createCar(Dictionary<string, string> io_VehicleDictionary)
        {
            io_VehicleDictionary.Add("Number of wheels", "4");
            io_VehicleDictionary.Add("Wheels maximum air pressure", "32");
            return new Car(io_VehicleDictionary);
        }

        private static Truck createTruck(Dictionary<string, string> io_VehicleDictionary)
        {
            io_VehicleDictionary.Add("Number of wheels", "16");
            io_VehicleDictionary.Add("Wheels maximum air pressure", "28");
            return new Truck(io_VehicleDictionary);
        }
    }
}

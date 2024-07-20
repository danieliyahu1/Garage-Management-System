using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class GarageUserInterface
    {
        private static Garage s_MyGarage;

        // Costumer requests
        private enum eMainMenuOptions
        {
            InsertNewVehicle = 1,
            DisplayVehicles = 2,
            ChangeVehicleStatus = 3,
            InflateWheelsToMaximum = 4,
            FuelVehicle = 5,
            ChargeVehicle = 6,
            DisplayVehicleInformation = 7
        }
        
        private enum eDisplayFilters
        {
            None = 1,
            InRepair = 2,
            Repaired = 3,
            Paid = 4
        }

         private enum eFuelTypes
        {
            Soler = 1,
            Octane95 = 2,
            Octane96 = 3,
            Octane98 = 4
        }

        public static void Init()
        {
            // Creates new istance of garage
            s_MyGarage = new Garage();

            while (true)
            {
                // First, we get the client selected option
                eMainMenuOptions selectedMainMenuOption = getMainMenuOption();

                switch (selectedMainMenuOption)
                {
                    case eMainMenuOptions.InsertNewVehicle:
                        addNewVehicle();
                        break;
                    case eMainMenuOptions.DisplayVehicles:
                        displayLicenseOfVehiclesInTheGarage();
                        break;
                    case eMainMenuOptions.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eMainMenuOptions.InflateWheelsToMaximum:
                        increaseWheelPressure();
                        break;
                    case eMainMenuOptions.FuelVehicle:
                        fuelVehicle();
                        break;
                    case eMainMenuOptions.ChargeVehicle:
                        rechargeElectricVehicle();
                        break;
                    case eMainMenuOptions.DisplayVehicleInformation:
                        displayVehicleInformation();
                        break;
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        /// <summary>
        /// This method:
        /// 1. Asks from the client the relevant main menu option of the garage
        /// 2. Validates if the input is valid
        /// </summary>
        /// <returns>the chosen menu option</returns>
        private static eMainMenuOptions getMainMenuOption()
        {
            // The main menu message
            string mainMenuMessage = string.Format(
                @"Hello, Welcome to our garage !
Please choose one of the following main menu options by entering the relevant option number:
1. Insert new vehicle to our garage
2. Display list of vehicles license numbers that currently in the garage
3. Change certain vehicle's status
4. Inflate wheels air pressure to maximum
5. Refuel a fuel-based vehicle
6. Charge a electric-based vehicle
7. Display vehicle information");

            Console.WriteLine(mainMenuMessage);
            string userInput = Console.ReadLine();

            int numberOfMenuOption;
            bool isValidMenuOption = int.TryParse(userInput, out numberOfMenuOption);

            // Validation of the user input
            while (!isValidMenuOption || numberOfMenuOption < 1 || numberOfMenuOption > 7)
            {
                Console.WriteLine("Your input is invalid, please try again.");
                userInput = Console.ReadLine();

                isValidMenuOption = int.TryParse(userInput, out numberOfMenuOption);
            }

            return (eMainMenuOptions) numberOfMenuOption;
        }

        private static void addNewVehicle()
        {
            // Asks for thr vehicle license
            Console.WriteLine("Please insert the vehicle license");
            string vehicleLicense = Console.ReadLine();

            // Check if the vehicle already in the garage, if so - change the state of the vehicle to "In Repair"
            bool isVehicleAlreadyInTheGarage = s_MyGarage.IsVehicleInTheGarage(vehicleLicense, 1);

            if (isVehicleAlreadyInTheGarage)
            {
                Console.WriteLine("Your vehicle is already in the garage.");
            }
            else
            {
                int vehicleType = getVehicleType();

                //For each new vehicle - dictionary is created
                Dictionary<string, string> vehicleDictionary = VehicleCreation.CreateVehicleDetailsDictionary(vehicleLicense, vehicleType);
                insertUserValuesToDictionary(vehicleDictionary);

                try
                {
                    Vehicle ownerVehicle = VehicleCreation.CreateVehicle(vehicleType, vehicleDictionary);
                    s_MyGarage.InsertNewVehicle(ownerVehicle);
                }
                catch (Exception e)
                {
                    Console.WriteLine("One or more of the detailes were invalid, and returned this error message:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static int getVehicleType()
        {
            StringBuilder supportedVehicleTypes = new StringBuilder();
            string[] supportedVehicleTypesArray = VehicleCreation.GetArrayOfSupportedVehicleTypes();

            for (int i = 1; i <= supportedVehicleTypesArray.Length; i++)
            {
                supportedVehicleTypes.Append(string.Format($"{i}. {supportedVehicleTypesArray[i - 1]}{Environment.NewLine}"));
            }
                
            Console.WriteLine("Please choose one of the following vehicle type options by entering the relevant option number:");
            Console.Write(supportedVehicleTypes);
            string userInput = Console.ReadLine();

            int VehicleTypeNumber;
            bool isValidType = int.TryParse(userInput, out VehicleTypeNumber);

            while (!isValidType || VehicleTypeNumber < 1 || VehicleTypeNumber > supportedVehicleTypesArray.Length)
            {
                Console.WriteLine("Your input is invalid, please try again.");
                userInput = Console.ReadLine();

                isValidType = int.TryParse(userInput, out VehicleTypeNumber);
            }

            return VehicleTypeNumber;
        }

        private static void insertUserValuesToDictionary(Dictionary<string, string> io_VehicleDictionary)
        {
            Dictionary<string, string> copyOfVehicleDictionary = new Dictionary<string, string>();

            foreach (string currentKey in io_VehicleDictionary.Keys)
            {
                copyOfVehicleDictionary.Add(currentKey, null);
            }

            foreach (string currentKey in copyOfVehicleDictionary.Keys)
            {
                if (currentKey != "License" && currentKey != "Energy type" && currentKey != "Maximum time of engine operation (in hours)" && currentKey != "Maximum amount of fuel (in liters)")
                {
                    Console.WriteLine(string.Format($"Please insert {currentKey}:"));
                    io_VehicleDictionary[currentKey] = Console.ReadLine();
                }
            }
        }

        private static void displayLicenseOfVehiclesInTheGarage()
        {
            List<string> licenseNumbersToDisplay = new List<string>();
            string filterOptionsMessage =
                string.Format(
                    @"1. None.
2. In repair.
3. Repaired.
4. Paid.");

            Console.WriteLine("Please choose a filter for displaying license numbers:");
            Console.WriteLine(filterOptionsMessage);
            eDisplayFilters filterForDisplaying = (eDisplayFilters)getFilterForDisplaying(filterOptionsMessage);

            switch (filterForDisplaying)
            {
                case eDisplayFilters.None:
                    licenseNumbersToDisplay = s_MyGarage.GetListOfLicenses("None");
                    break;
                case eDisplayFilters.InRepair:
                    licenseNumbersToDisplay = s_MyGarage.GetListOfLicenses("In repair");
                    break;
                case eDisplayFilters.Repaired:
                    licenseNumbersToDisplay = s_MyGarage.GetListOfLicenses("Repaired");
                    break;
                case eDisplayFilters.Paid:
                    licenseNumbersToDisplay = s_MyGarage.GetListOfLicenses("Paid");
                    break;
            }

            int numberOfLicenseNumbersToDisplay = licenseNumbersToDisplay.Count;

            if (numberOfLicenseNumbersToDisplay == 0)
            {
                Console.WriteLine("No license numbers to display.");
            }
            else
            {
                Console.WriteLine("List of licenses (according to your filter option):");
                for (int i = 0; i < numberOfLicenseNumbersToDisplay; i++)
                {
                    Console.WriteLine(licenseNumbersToDisplay[i]);
                }
            }
        }

        private static int getFilterForDisplaying(string i_FilterOptionsMessage)
        {
            string filterForDisplayingString = Console.ReadLine();
            bool isFilterForDisplayingValid = int.TryParse(filterForDisplayingString, out int filterForDisplaying);

            while (!isFilterForDisplayingValid || filterForDisplaying < 1 || filterForDisplaying > 5)
            {
                if (!isFilterForDisplayingValid)
                {
                    Console.WriteLine("Invalid input, please try again");
                }
                else
                {
                    Console.WriteLine("Your choice does not exist, please try again");
                }

                filterForDisplayingString = Console.ReadLine();
                isFilterForDisplayingValid = int.TryParse(filterForDisplayingString, out filterForDisplaying);
            }

            return filterForDisplaying;
        }

        private static void changeVehicleStatus()
        {
            Console.WriteLine("Please insert your license number:");
            string licenseNumber = Console.ReadLine();
            int newStatus = getVehicleStatus();
            bool carExists = s_MyGarage.IsVehicleInTheGarage(licenseNumber, newStatus);

            if (carExists)
            {
                Console.WriteLine("The new status was updated successfully.");
            }
            else
            {
                Console.WriteLine("The vehicle does not exist in the garage.");
            }
        }

        private static int getVehicleStatus()
        {
            string changeVehicleStatusOptions =
                string.Format(
                    @"Please insert new status of the vehicle as follows:
1. In repair.
2. Repaired.
3. Paid.");
            Console.WriteLine(changeVehicleStatusOptions);

            string newStatusString = Console.ReadLine();
            int newStatus;
            bool isValidNumber = int.TryParse(newStatusString, out newStatus);

            while (!isValidNumber || newStatus < 1 || newStatus > 3)
            {
                Console.WriteLine("You inserted invalid input, please try again.");

                newStatusString = Console.ReadLine();
                isValidNumber = int.TryParse(newStatusString, out newStatus);
            }

            return newStatus;
        }

        private static void increaseWheelPressure()
        {
            Console.WriteLine("Please insert your vehicle license:");
            string vehiclleLicense = Console.ReadLine();

            if (!s_MyGarage.IsVehicleInTheGarage(vehiclleLicense))
            {
                Console.WriteLine("The vehicle does not exist in the garage.");
            }
            else
            {
                try
                {
                    s_MyGarage.FillAirPressureToMaximum(vehiclleLicense);
                    Console.WriteLine("Wheels air pressure inflated to maximum.");
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void fuelVehicle()
        {
            Console.WriteLine("Please enter your vehicle license:");
            string vehicleLicense = Console.ReadLine();

            if (!s_MyGarage.IsVehicleInTheGarage(vehicleLicense))
            {
                Console.WriteLine("The vehicle does not exist in the garage.");
            }
            else
            {
                eFuelTypes fuelTypeOption = (eFuelTypes)getFuelTypeOption();

                Console.WriteLine("Please insert amount (in liters) to fill:");
                string amountToFill = Console.ReadLine();

                try
                {
                    switch (fuelTypeOption)
                    {
                        case eFuelTypes.Soler:
                            s_MyGarage.RefillVehicleEnergy(vehicleLicense, "Soler", amountToFill);
                            break;
                        case eFuelTypes.Octane95:
                            s_MyGarage.RefillVehicleEnergy(vehicleLicense, "Octane95", amountToFill);
                            break;
                        case eFuelTypes.Octane96:
                            s_MyGarage.RefillVehicleEnergy(vehicleLicense, "Octane96", amountToFill);
                            break;
                        case eFuelTypes.Octane98:
                            s_MyGarage.RefillVehicleEnergy(vehicleLicense, "Octane98", amountToFill);
                            break;
                    }
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static int getFuelTypeOption()
        {
            Console.WriteLine("Please insert fuel type as follows: ");
            string fuelTypes =
                string.Format(
                    @"1. Soler.
2. Octane 95.
3. Octane 96.
4. Octane 98.");
            Console.WriteLine(fuelTypes);

            string fuelTypeOptionString = Console.ReadLine();
            bool isValidNumber = int.TryParse(fuelTypeOptionString, out int fuelTypeOption);

            while (!isValidNumber || fuelTypeOption < 1 || fuelTypeOption > 4)
            {
                Console.WriteLine("You inserted invalid input, please try again.");

                fuelTypeOptionString = Console.ReadLine();
                isValidNumber = int.TryParse(fuelTypeOptionString, out fuelTypeOption);
            }

            return fuelTypeOption;
        }

        private static void rechargeElectricVehicle()
        {
            Console.WriteLine("Please enter your vehicle license:");
            string vehicleLicense = Console.ReadLine();

            if (!s_MyGarage.IsVehicleInTheGarage(vehicleLicense))
            {
                Console.WriteLine("The vehicle does not exist in the garage.");
            }
            else
            {
                Console.WriteLine("Please insert time of charging (in minutes):");
                string amountToFill = Console.ReadLine();

                try
                {
                    s_MyGarage.RefillVehicleEnergy(vehicleLicense, "Electricity", amountToFill);
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void displayVehicleInformation()
        {
            Console.WriteLine("Please insert vehicle license:");
            string vehicleLicense = Console.ReadLine();
            string vehicleData = s_MyGarage.GetVehicleInformation(vehicleLicense);

            if (s_MyGarage.IsVehicleInTheGarage(vehicleLicense))
            {
                Console.WriteLine(vehicleData);
            }
            else
            {
                Console.WriteLine("The vehicle doesn't exist in the garage.");
            }
        }
    }
}

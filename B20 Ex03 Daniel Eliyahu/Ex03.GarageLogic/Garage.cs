using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {

        // $G$ NTT-999 (-3) This kind of field should be readonly.
        private List<ClientTicket> m_Clients;

        public Garage()
        {
            m_Clients = new List<ClientTicket>();
        }

        public bool IsVehicleInTheGarage(string i_VehicleLicense, int i_NewStatusNumber)
        {
            int vehicleIndexInList = ClientTicketIndex(i_VehicleLicense);
            bool isVehicleAlreadyInList = vehicleIndexInList != -1;

            if (isVehicleAlreadyInList)
            {
                m_Clients[vehicleIndexInList].ChangeVehicleStatus(i_NewStatusNumber);
            }

            return isVehicleAlreadyInList;
        }

        public bool IsVehicleInTheGarage(string i_VehicleLicense)
        {
            int vehicleIndexInList = ClientTicketIndex(i_VehicleLicense);
            
            return vehicleIndexInList != -1;
        }

        private int ClientTicketIndex(string i_VehicleLicense)
        {
            int clientTicketIndex = -1;

            for (int i = 0; i < m_Clients.Count; i++)
            {
                if (m_Clients[i].OwnerVehicle.VehicleDictionary["License"].Equals(i_VehicleLicense))
                {
                    clientTicketIndex = i;
                    break;
                }
            }

            return clientTicketIndex;
        }

        public void InsertNewVehicle(Vehicle i_OwnerVehicle)
        {
            m_Clients.Add(new ClientTicket(i_OwnerVehicle));
        }

        private int getCostumerVehicleIndexInList(string i_VehicleLicense)
        {
            int vehicleIndexInList = -1;

            for (int i = 0; i < m_Clients.Count; i++)
            {
                if (m_Clients[i].OwnerVehicle.License == i_VehicleLicense)
                {
                    vehicleIndexInList = i;
                    break;
                }
            }

            return vehicleIndexInList;
        }

        public List<string> GetListOfLicenses(string i_VehicleStatusFilter)
        {
            List<string> listOfLicenseNumbers = new List<string>();

            if (i_VehicleStatusFilter == "None")
            {
                for (int i = 0; i < m_Clients.Count; i++)
                {
                    listOfLicenseNumbers.Add(m_Clients[i].OwnerVehicle.License);
                }
            }
            else
            {
                for (int i = 0; i < m_Clients.Count; i++)
                {
                    if (i_VehicleStatusFilter == m_Clients[i].OwnerVehicle.VehicleDictionary["Vehicle status"])
                    {
                        listOfLicenseNumbers.Add(m_Clients[i].OwnerVehicle.License);
                    }
                }
            }

            return listOfLicenseNumbers;
        }

        /**
        public bool ChangeVehicleStatus(string i_LicenseNumber, int i_NewVehicleStatus)
        {
            int vehicleIndex = getCostumerVehicleIndexInList(i_LicenseNumber);
            bool vehicleStatusChanged = false;

            if (IsVehicleInTheGarage(i_LicenseNumber))
            {
                m_Clients[vehicleIndex].ChangeVehicleStatus(i_NewVehicleStatus);
                vehicleStatusChanged = true;
            }

            return vehicleStatusChanged;
        }
        */

        public void FillAirPressureToMaximum(string i_LicenseNumber)
        {
            int vehicleIndexInList = getCostumerVehicleIndexInList(i_LicenseNumber);
            List<Wheel> ownerVehicleWheels = m_Clients[vehicleIndexInList].OwnerVehicle.Wheels;

            for (int i = 0; i < ownerVehicleWheels.Count; i++)
            {
                ownerVehicleWheels[i].FillAirPressureInWheel(ownerVehicleWheels[i].AirPressureAvailableToFill());
            }

            m_Clients[vehicleIndexInList].OwnerVehicle.VehicleDictionary["Wheels current air pressure"] = ownerVehicleWheels[0].CurrentAirPressure.ToString();
        }

        public void RefillVehicleEnergy(string i_VehicleLicense, string i_EnergyType, string i_AmountToFill)
        {
            int vehicleIndex = getCostumerVehicleIndexInList(i_VehicleLicense);
            Vehicle ownerVehicle = m_Clients[vehicleIndex].OwnerVehicle;
            float amountToFill = Vehicle.FloatTypeValidation(i_AmountToFill);

            if (ownerVehicle.Engine.EnergyType.Equals(Engine.eEnergySource.Electricity))
            {
                ownerVehicle.Engine.Refill(i_EnergyType, amountToFill / 60);
                ownerVehicle.m_VehicleDictionary["Remaining time of engine operation (in hours)"] = ownerVehicle.Engine.CurrentEnergyAmount.ToString();
            }
            else
            {
                ownerVehicle.Engine.Refill(i_EnergyType, amountToFill);
                ownerVehicle.m_VehicleDictionary["Current amount of fuel (in liters)"] = ownerVehicle.Engine.CurrentEnergyAmount.ToString();
            }

            ownerVehicle.m_VehicleDictionary["Remaining energy percentage"] = ownerVehicle.Engine.GetRemainingEnergyPercentage().ToString();
        }

        public string GetVehicleInformation(string i_LicenseNumber)
        {
            int vehicleIndexInList = this.getCostumerVehicleIndexInList(i_LicenseNumber);
            StringBuilder vehiclesInformation = new StringBuilder();
            StringBuilder upperCaseFirstLetterCurrentKey = new StringBuilder();

            if (IsVehicleInTheGarage(i_LicenseNumber))
            {
                Dictionary<string, string> costumerVehicleDictionary = m_Clients[vehicleIndexInList].OwnerVehicle.VehicleDictionary;
                foreach (string currentKey in costumerVehicleDictionary.Keys)
                {
                    upperCaseFirstLetterCurrentKey.Append(char.ToUpper(currentKey[0]));
                    upperCaseFirstLetterCurrentKey.Append(currentKey.Substring(1));
                    vehiclesInformation.Append(string.Format("{0}: {1}{2}", upperCaseFirstLetterCurrentKey.ToString(), costumerVehicleDictionary[currentKey], Environment.NewLine));
                    upperCaseFirstLetterCurrentKey.Replace(upperCaseFirstLetterCurrentKey.ToString(), string.Empty);
                }
            }
            return vehiclesInformation.ToString();
        }
    }
}

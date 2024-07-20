using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ClientTicket
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired = 2,
            Paid = 3
        }

        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_OwnerVehicle;
        private eVehicleStatus m_VehicleStatus;

        public ClientTicket(Vehicle i_OwnerVehicle)
        {
            this.m_OwnerVehicle = i_OwnerVehicle;
            this.m_OwnerName = ownerNameValidation();
            this.m_OwnerPhoneNumber = ownerPhoneNumberValidation();
            this.m_VehicleStatus = eVehicleStatus.InRepair;
            this.m_OwnerVehicle.VehicleDictionary.Add("Vehicle status", "In repair");
        }

        public Vehicle OwnerVehicle
        {
            get { return this.m_OwnerVehicle; }
        }
        
        internal static List<string> GetClientTicketKeys()
        {
            List<string> clientTicketKeys = new List<string>() { "Owner name", "Owner phone number" };

            return clientTicketKeys;
        }

        internal void ChangeVehicleStatus(int i_VehicleStatus)
        {
            this.m_VehicleStatus = (eVehicleStatus)i_VehicleStatus;

            switch (this.m_VehicleStatus)
            {
                case eVehicleStatus.InRepair:
                    this.m_OwnerVehicle.VehicleDictionary["Vehicle status"] = "In repair";
                    break;
                case eVehicleStatus.Repaired:
                    this.m_OwnerVehicle.VehicleDictionary["Vehicle status"] = "Repaired";
                    break;
                case eVehicleStatus.Paid:
                    this.m_OwnerVehicle.VehicleDictionary["Vehicle status"] = "Paid";
                    break;
                default:
                    throw new ArgumentException("Invalid input");
            }
        }

        private string ownerNameValidation()
        {
            string ownerName = this.m_OwnerVehicle.VehicleDictionary["Owner name"];
            string ownerNameCapitalLetters = ownerName.ToUpper();

            if (ownerName == string.Empty)
            {
                throw new ArgumentException("Invalid owner name.");
            }

            for (int i = 0; i < ownerNameCapitalLetters.Length; i++)
            {
                char currentChar = ownerNameCapitalLetters[i];

                if ((currentChar < 'A' || currentChar > 'Z') && !(currentChar.Equals(' ')))
                {
                    throw new ArgumentException("Invalid owner name.");
                }
            }

            return ownerName;
        }

        private string ownerPhoneNumberValidation()
        {
            string ownerPhoneNumberString = this.m_OwnerVehicle.VehicleDictionary["Owner phone number"];

            if (ownerPhoneNumberString == string.Empty)
            {
                throw new ArgumentException("Invalid owner phone number.");
            }

            bool isOwnerPhoneNumberValid = int.TryParse(ownerPhoneNumberString, out int ownerPhoneNumber);

            if (!isOwnerPhoneNumberValid)
            {
                throw new ArgumentException("Invalid owner phone number.");
            }

            return ownerPhoneNumberString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_License;
        protected float m_RemainingEnergyPercentage;
        internal Engine m_Engine;
        internal List<Wheel> m_Wheels;
        internal Dictionary<string, string> m_VehicleDictionary;

        public Vehicle(Dictionary<string, string> i_VehicleDetails)
        {
            this.m_VehicleDictionary = i_VehicleDetails;
            this.m_ModelName = this.m_VehicleDictionary["Model name"];
            this.m_License = this.m_VehicleDictionary["License"];
            m_Engine = new Engine(this.m_VehicleDictionary);
            updateEngineInDictionary();
            m_RemainingEnergyPercentage = m_Engine.GetRemainingEnergyPercentage();
            m_VehicleDictionary.Add("Remaining energy percentage", m_RemainingEnergyPercentage.ToString());
            setWheels();
            updateWheelsInDictionary();
        }

        internal Dictionary<string, string> VehicleDictionary
        {
            get { return this.m_VehicleDictionary; }
        }

        internal string License
        {
            get { return this.m_License; }
        }

        internal List<Wheel> Wheels
        {
            get { return this.m_Wheels; }
        }

        internal Engine Engine
        {
            get { return this.m_Engine; }
        }

        private void setWheels()
        {
            int numberOfWheels = int.Parse(this.m_VehicleDictionary["Number of wheels"]);
            this.m_Wheels = new List<Wheel>(numberOfWheels);
            for (int i = 0; i < numberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(m_VehicleDictionary));
            }
        }

        internal static float FloatTypeValidation(string i_InputString)
        {
            bool isValidFloat = float.TryParse(i_InputString, out float returnValue);

            if (!isValidFloat)
            {
                throw new FormatException("Invalid input.");
            }

            return returnValue;
        }

        internal static int IntTypeValidation(string i_InputString)
        {
            bool isValidFloat = int.TryParse(i_InputString, out int returnValue);

            if (!isValidFloat)
            {
                throw new FormatException("Invalid input.");
            }

            return returnValue;
        }

        internal static bool BoolTypeValidation(string i_InputString)
        {
            i_InputString = i_InputString.ToUpper();
            bool returnValue = false;

            if (!i_InputString.Equals("FALSE") && !i_InputString.Equals("TRUE"))
            {
                throw new FormatException("Invalid boolean input.");
            }
            else
            {
                if (i_InputString == "TRUE")
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private void updateEngineInDictionary()
        {
            string energyType = this.m_VehicleDictionary["Energy type"];
            this.m_VehicleDictionary.Remove("Energy type");
            if (energyType == "Electricity")
            {
                this.m_VehicleDictionary.Remove("Maximum time of engine operation (in hours)");
            }
            else
            {
                this.m_VehicleDictionary.Remove("Maximum amount of fuel (in liters)");
            }
        }

        private void updateWheelsInDictionary()
        {
            m_VehicleDictionary.Remove("Wheels maximum air pressure");
        }

        internal static List<string> GetVehicleKeys()
        {
            List<string> VehicleKeys = new List<string>() {"Model name"};
            return VehicleKeys;
        }
    }
}

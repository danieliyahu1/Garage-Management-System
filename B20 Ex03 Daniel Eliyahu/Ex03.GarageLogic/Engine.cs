using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{

    // $G$ DSN-999 (-5) This class should have been abstract. - fuel engine and electric engine inherit from abstract engine
    internal class Engine
    {
        internal enum eEnergySource
        {
            Soler,
            Octane95,
            Octane96,
            Octane98,
            Electricity
        }


        // $G$ DSN-999 (-4) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        private eEnergySource m_EnergySource;
        private float m_CurrentEnergyAmount;
        private float m_MaximumEnergyAmount;

        public Engine(Dictionary<string, string> i_VehicleDetails)
        {
            this.m_EnergySource = (eEnergySource)Enum.Parse(typeof(eEnergySource), i_VehicleDetails["Energy type"]);
            float currentEnergyAmount;

            if (i_VehicleDetails["Energy type"] == "Electricity")
            {
                this.m_MaximumEnergyAmount = Vehicle.FloatTypeValidation(i_VehicleDetails["Maximum time of engine operation (in hours)"]);
                currentEnergyAmount = Vehicle.FloatTypeValidation(i_VehicleDetails["Remaining time of engine operation (in hours)"]);
            }
            else
            {
                this.m_MaximumEnergyAmount = Vehicle.FloatTypeValidation(i_VehicleDetails["Maximum amount of fuel (in liters)"]);
                currentEnergyAmount = Vehicle.FloatTypeValidation(i_VehicleDetails["Current amount of fuel (in liters)"]);
            }

            if (currentEnergyAmount > this.m_MaximumEnergyAmount)
            {
                throw new ValueOutOfRangeException(0, this.m_MaximumEnergyAmount);
            }
            else
            {
                this.m_CurrentEnergyAmount = currentEnergyAmount;
            }
        }

        internal float CurrentEnergyAmount
        {
            get { return this.m_CurrentEnergyAmount; }
        }

        internal eEnergySource EnergyType
        {
            get { return this.m_EnergySource; }
        }

        internal void Refill(string i_EnergyType, float i_EnergyAmountToFill)
        {
            eEnergySource energyType = (eEnergySource)Enum.Parse(typeof(eEnergySource), i_EnergyType);

            if (energyType.Equals(this.m_EnergySource))
            {
                if (this.m_CurrentEnergyAmount + i_EnergyAmountToFill > this.m_MaximumEnergyAmount)
                {
                    throw new ValueOutOfRangeException(0, this.m_MaximumEnergyAmount);
                }

                this.m_CurrentEnergyAmount += i_EnergyAmountToFill;
            }
            else
            {
                throw new ArgumentException("Invalid input.");
            }
        }

        internal float GetRemainingEnergyPercentage()
        {
            return (this.m_CurrentEnergyAmount / this.m_MaximumEnergyAmount) * 100;
        }
    }
}

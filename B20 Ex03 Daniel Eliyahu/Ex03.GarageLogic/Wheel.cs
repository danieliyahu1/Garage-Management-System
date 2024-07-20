using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private string m_ManufacturerName;


        // $G$ DSN-999 (-4) The "maximum air pressure" field should be readonly member of class wheel.
        private float m_MaxAirPressureRecommended;
        private float m_CurrentAirPressure;

        public Wheel(Dictionary<string, string> i_VehicleDetailesDictionary)
        {
            this.m_ManufacturerName = i_VehicleDetailesDictionary["Wheels manufacturer name"];
            this.m_MaxAirPressureRecommended = float.Parse(i_VehicleDetailesDictionary["Wheels maximum air pressure"]);
            float currentAirPressure = float.Parse(i_VehicleDetailesDictionary["Wheels current air pressure"]);

            if (currentAirPressure > m_MaxAirPressureRecommended)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressureRecommended);
            }
            else
            {
                this.m_CurrentAirPressure = currentAirPressure;
            }
        }

        public float CurrentAirPressure
        {
            get { return this.m_CurrentAirPressure; }
        }

        internal static List<string> GetWheelsKeys()
        {
            List<string> WheelsKeys = new List<string>() { "Wheels manufacturer name", "Wheels current air pressure" };

            return WheelsKeys;
        }

        internal float AirPressureAvailableToFill()
        {
            return this.m_MaxAirPressureRecommended - this.m_CurrentAirPressure;
        }

        internal void FillAirPressureInWheel(float i_AirToFill)
        {
            if (i_AirToFill > AirPressureAvailableToFill())
            {
                throw new ValueOutOfRangeException(0, this.m_MaxAirPressureRecommended);
            }

            this.m_CurrentAirPressure += i_AirToFill;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        internal enum eLicenseType
        {
            A,
            A1,
            AA,
            B
        }
        
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(Dictionary<string, string> i_MotorcycleDetails) : base(i_MotorcycleDetails)
        {
            m_LicenseType = LicenseTypeValidation(i_MotorcycleDetails);
            m_EngineVolume = Vehicle.IntTypeValidation(i_MotorcycleDetails["Engine volume"]);
        }

        internal static List<string> GetMotorcycleKeys()
        {
            List<string> motorcycleKeys = new List<string>() { "License type" , "Engine volume" };

            return motorcycleKeys;
        }

        private eLicenseType LicenseTypeValidation(Dictionary<string, string> i_MotorcycleDetails)
        {
            eLicenseType licenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_MotorcycleDetails["License type"]);
            
            if (!licenseType.Equals(eLicenseType.A) && !licenseType.Equals(eLicenseType.A1) && !licenseType.Equals(eLicenseType.AA) && !licenseType.Equals(eLicenseType.B))
            {
                throw new FormatException("Invalid license type.");
            }

            return licenseType;
        }
    }
}

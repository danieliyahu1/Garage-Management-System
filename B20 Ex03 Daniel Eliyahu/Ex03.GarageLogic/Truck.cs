using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private bool m_IsCarryHazardousMaterials;
        private float m_VolumeOfCargo;

        public Truck(Dictionary<string, string> i_TruckDetails) : base(i_TruckDetails)
        {
            m_IsCarryHazardousMaterials = Vehicle.BoolTypeValidation(i_TruckDetails["Does truck carry hazardous material?"]);
            m_VolumeOfCargo = Vehicle.FloatTypeValidation(i_TruckDetails["Volume of cargo"]);
        }

        internal static List<string> GetTruckKeys()
        {
            List<string> truckKeys = new List<string>(){ "Does truck carry hazardous material?", "Volume of cargo"};

            return truckKeys;
        }
    }
}

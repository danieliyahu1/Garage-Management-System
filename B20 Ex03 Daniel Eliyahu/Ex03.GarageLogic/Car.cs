using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public enum eColor
        {
            Gray,
            Red,
            White,
            Black
        }

        private eColor m_CarColor;
        private int m_NumberOfDoors;

        internal Car(Dictionary<string, string> i_CarDetails) : base(i_CarDetails)
        {
            this.m_CarColor = (eColor)Enum.Parse(typeof(eColor), i_CarDetails["Car color"]);
            int numberOfDoors = Vehicle.IntTypeValidation(i_CarDetails["Number of doors"]);

            if (numberOfDoors < 2 || numberOfDoors > 5)
            {
                throw new ValueOutOfRangeException(2, 5);
            }
            else
            {
               this. m_NumberOfDoors = numberOfDoors;
            }
        }

        internal static List<string> GetCarKeys()
        {
            List<string> carKeys = new List<string>() { "Car color", "Number of doors" };

            return carKeys;
        }
    }
}

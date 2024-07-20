using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format($"Invalid value - value is out of range. Maximum possible value: {i_MaxValue}, minimum possible value: {i_MinValue}"))
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
        }

        public float MinValue
        {
            get { return this.m_MinValue; }
        }

        public float MaxValue
        {
            get { return this.m_MaxValue; }
        }
    }
}

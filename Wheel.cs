namespace Ex03.GarageLogic
{
    public sealed class Wheel
    {
        internal string m_ManufacturerName;
        internal float m_CurrentAirPressure;

        internal Wheel()
        {
        }

        public string GetInfo()
        {
            return string.Format(@"Current air pressure: {0}
    Wheel manufacturer name: {1}
", CurrentAirPressure, ManufacturerName);
        }

        internal string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public void FillUpAir(float i_FillAir)
        {
            m_CurrentAirPressure += i_FillAir;
        }
    }
}

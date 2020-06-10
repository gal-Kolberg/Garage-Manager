namespace Ex03.GarageLogic
{
    public abstract class EnergyManager
    {
        internal float m_MaxEnergyCapacity;
        internal float m_CurrentEnergy;

        internal float MaxEnergyCapacity
        {
            get
            {
                return m_MaxEnergyCapacity;
            }

            set
            {
                m_MaxEnergyCapacity = value;
            }
        }

        internal EnergyManager(float i_CurrentEnergy, float i_MaxEnergyCapacity)
        {
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
            CurrentEnergy = i_CurrentEnergy;
        }

        public abstract string GetInfo();

        internal float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                if (m_CurrentEnergy <= m_MaxEnergyCapacity && m_CurrentEnergy >= 0)
                {
                    m_CurrentEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(m_MaxEnergyCapacity, 0, string.Format("The amount you entered exceeds the maximum amout, the minimum amout is: {0}, and the maximum is: {1}", 0, MaxEnergyCapacity));
                }
            }
        }

        internal float CurrentEnergyPercentage
        {
            get
            {
                return m_CurrentEnergy / m_MaxEnergyCapacity * 100;
            }
        }
    }
}

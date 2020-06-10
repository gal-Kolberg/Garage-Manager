using System;

namespace Ex03.GarageLogic
{
    public sealed class GarageInfo
    {
        internal string m_OwnerName;
        internal string m_OwnerPhone;
        public eVehicleStatus m_VehicleStatus;

        internal GarageInfo(string i_OwnerName, string i_OwnerPhone)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public string GetInfo()
        {
            return string.Format(@"Owner name: {0}
Vehicle status: {1}
", OwnerName, Enum.GetName(typeof(eVehicleStatus), VehicleStatus));
        }

        internal string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        internal string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }

            set
            {
                m_OwnerPhone = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }
    }
}

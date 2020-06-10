using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public static Vehicle CreateKindOfVehicle(eKindOfVehicle i_KindOfVehicle, List<QuestionAnswer> i_InitialMessages)
        {
            const int k_UserName = 0;
            const int k_UserPhone = 1;
            const int k_LicenseNumber = 2;
            Vehicle vehicle = null;

            switch (i_KindOfVehicle)
            {
                case eKindOfVehicle.ElectricCar:
                    {
                        vehicle = new ElectricCar(i_InitialMessages[k_UserName].Answer, i_InitialMessages[k_UserPhone].Answer, i_InitialMessages[k_LicenseNumber].Answer);
                        break;
                    }

                case eKindOfVehicle.ElectricMotorcycle:
                    {
                        vehicle = new ElectricMotorcycle(i_InitialMessages[k_UserName].Answer, i_InitialMessages[k_UserPhone].Answer, i_InitialMessages[k_LicenseNumber].Answer);
                        break;
                    }

                case eKindOfVehicle.GasCar:
                    {
                        vehicle = new GasCar(i_InitialMessages[k_UserName].Answer, i_InitialMessages[k_UserPhone].Answer, i_InitialMessages[k_LicenseNumber].Answer);
                        break;
                    }

                case eKindOfVehicle.GasMotorcycle:
                    {
                        vehicle = new GasMotorcycle(i_InitialMessages[k_UserName].Answer, i_InitialMessages[k_UserPhone].Answer, i_InitialMessages[k_LicenseNumber].Answer);
                        break;
                    }

                case eKindOfVehicle.Truck:
                    {
                        vehicle = new Truck(i_InitialMessages[k_UserName].Answer, i_InitialMessages[k_UserPhone].Answer, i_InitialMessages[k_LicenseNumber].Answer);
                        break;
                    }
            }

            return vehicle;
        }

        public static string[] GetEnumNameString(Type i_EnumType)
        {
            string[] enumNameString = null;

            if(i_EnumType == typeof(eVehicleStatus))
            {
                enumNameString = Enum.GetNames(typeof(eVehicleStatus));
            }
            else if(i_EnumType == typeof(eKindOfFuel))
            {
                enumNameString = Enum.GetNames(typeof(eKindOfFuel));
            }
            else if(i_EnumType == typeof(eKindOfVehicle))
            {
                enumNameString = Enum.GetNames(typeof(eKindOfVehicle));
            }
            else if (i_EnumType == typeof(eNumberOfDoors))
            {
                enumNameString = Enum.GetNames(typeof(eNumberOfDoors));
            }
            else if (i_EnumType == typeof(eCarColor))
            {
                enumNameString = Enum.GetNames(typeof(eCarColor));
            }
            else if (i_EnumType == typeof(eLicenseType))
            {
                enumNameString = Enum.GetNames(typeof(eLicenseType));
            }
            else
            {
                throw new FormatException("An unknown type was give");
            }

            return enumNameString;
        }
    }
}

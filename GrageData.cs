using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class GrageData
    {
        public List<Vehicle> m_DataBase;
        
        public GrageData()
        {
            m_DataBase = new List<Vehicle>();
        }

        public static void IsNumInRange(int i_MinValue, int i_MaxValue, int io_UserInput, string i_MsgToUser)
        {
            if (io_UserInput > i_MaxValue || io_UserInput < i_MinValue)
            {
                throw new ValueOutOfRangeException(i_MaxValue, i_MinValue, string.Format(i_MsgToUser + string.Format("is invalid, the minimum value is {0} ,and the maximum value is {1}", i_MinValue, i_MaxValue)));
            }
        }

        public static string GetEnumFormatedString(Type i_EnumType, out int o_EnumLength)
        {
            string formatedNames = string.Empty;
            string[] enumNamesArr = VehicleCreator.GetEnumNameString(i_EnumType);
            o_EnumLength = enumNamesArr.Length;
            int count = 1;

            foreach (string enumName in enumNamesArr)
            {
                formatedNames += string.Format("{0}) {1}{2}", count, enumName, Environment.NewLine);
                count++;
            }

            return formatedNames;
        }

        public List<QuestionAnswer> GetInitialMessage()
        {
            List<QuestionAnswer> initialMesseges = new List<QuestionAnswer>
            {
                new QuestionAnswer("Please enter your name: ", string.Empty),
                new QuestionAnswer("Please enter your phone number: ", string.Empty),
                new QuestionAnswer("Please enter your license number: ", string.Empty),
            };

            initialMesseges.Add(new QuestionAnswer(string.Format(@"Please Enter the kind of vehicle you are admiting to the garage. Press one of the following:
{0}", GetEnumFormatedString(typeof(eKindOfVehicle), out int size)), string.Empty));

            return initialMesseges;
        }

        public void ReadMessagesAs(List<QuestionAnswer> i_UserAnswers)
        {
            int lastVehicle = m_DataBase.Count - 1;

            m_DataBase[lastVehicle].ParseAnswers(i_UserAnswers);
        }

        public List<QuestionAnswer> ParseInitialUserInput(List<QuestionAnswer> i_UserInput)
        {
            const int k_PhoneNumberPlace = 1;
            const int k_LicenseNumberPlace = 2;
            const int k_VehicleListPlace = 3;
            const int k_MinValue = 1;
            int userVehicle;
            int k_MaxValue = Enum.GetNames(typeof(eKindOfVehicle)).Length;
            Vehicle vehicle;
            eKindOfVehicle kindOfVehicle = eKindOfVehicle.ElectricCar;

            userVehicle = int.Parse(i_UserInput[k_VehicleListPlace].Answer);
            validatePhoneNumber(i_UserInput[k_PhoneNumberPlace].Answer);
            validateLicenseNumber(i_UserInput[k_LicenseNumberPlace].Answer);
            IsVehicleExists(i_UserInput[k_LicenseNumberPlace].Answer);
            IsNumInRange(k_MinValue, k_MaxValue, userVehicle, "The vehicle kind you gave ");
            kindOfVehicle = (eKindOfVehicle)userVehicle;
            vehicle = VehicleCreator.CreateKindOfVehicle(kindOfVehicle, i_UserInput);
            m_DataBase.Add(vehicle);

            return vehicle.GetQuestions();
        }

        public void IsVehicleExists(string i_LicenseNumber)
        {
            bool vehicleFound = false;

            foreach (Vehicle vehicle in DataBase)
            {
                if (vehicle.LicenseNumber == i_LicenseNumber)
                {
                    vehicleFound = true;
                    break;
                }
            }

            if (vehicleFound == true)
            {
                throw new ArgumentException("The license number that was given is already in the garage. Please try agian.");
            }
        }

        public Vehicle FindByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicleToFind = null;

            foreach (Vehicle vehicle in DataBase)
            {
                if (vehicle.LicenseNumber == i_LicenseNumber)
                {
                    vehicleToFind = vehicle;
                    break;
                }
            }

            if (vehicleToFind == null)
            {
                throw new ArgumentException("License number was not found!");
            }

            return vehicleToFind;
        }

        private static void validateLicenseNumber(string i_UserInput)
        {
            foreach (char ch in i_UserInput)
            {
                if (char.IsLetterOrDigit(ch) == false)
                {
                    throw new ArgumentException("Lincense number consists only letters and digits. please try agian");
                }
            }
        }

        private static void validatePhoneNumber(string i_UserInput)
        {
            foreach (char ch in i_UserInput)
            {
                if (char.IsDigit(ch) == false)
                {
                    throw new ArgumentException("Phone number consists only digits. please try agian");
                }
            }
        }

        public List<Vehicle> DataBase
        {
            get
            {
                return m_DataBase;
            }
        }
    }
}

using System.Collections.Generic;
using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UserInterface
    {
        static readonly GrageData sr_GrageData = new GrageData();
        
        public static void OpenGarage()
        {
            const int k_MinValue = 1;
            const int k_MaxValue = 8;
            eUserOption userInput;

            do
            {
               Console.WriteLine(string.Format(@"===============================================================
Enter 1 to insert a new vehicle.
Enter 2 to display all of the cars by the same given status.
Enter 3 to change a vehicle status.
Enter 4 to fill up air pressure to max air pressure.
Enter 5 to fuel a gas vehicle.
Enter 6 to charge an electric vehicle.
Enter 7 to display full data according to license number.
Enter 8 to close the garage
==============================================================="));
                userInput = (eUserOption)getIntFromUser(k_MinValue, k_MaxValue, "Please enter a number between 1 and 8.");
                sendToRightMethod(userInput);
            } while (userInput != eUserOption.CloseGarage);
        }
        
        private static void sendToRightMethod(eUserOption i_UserInput)
        {
            switch(i_UserInput)
            {
                case eUserOption.NewVehicle:
                    {
                        addNewVehicle();
                        break;
                    }

                case eUserOption.DisplaySameStatus:
                    {
                        printByLicenseNumber();
                        break;
                    }

                case eUserOption.ChangeStatus:
                    {
                        changeVehicleStatus();
                        break;
                    }

                case eUserOption.FillAirPressure:
                    {
                        fillUpByLicense();
                        break;
                    }

                case eUserOption.FillUpFuel:
                    {
                        fuelVehicle();
                        break;
                    }

                case eUserOption.Charge:
                    {
                        chargeVehicle();
                        break;
                    }

                case eUserOption.DisplayAllData:
                    {
                        printAllData();
                        break;
                    }

                case eUserOption.CloseGarage:
                    {
                        break;
                    }
            }
        }
       
        private static void addNewVehicle()
        {
            List<QuestionAnswer> vehicleQuestionList;
            List<QuestionAnswer> initialMesseges = sr_GrageData.GetInitialMessage();

            foreach (QuestionAnswer question in initialMesseges)
            {
                Console.WriteLine(question.Question);
                question.Answer = Console.ReadLine();
            }

            try
            {
                vehicleQuestionList = sr_GrageData.ParseInitialUserInput(initialMesseges);
                answerVehicleQuestions(vehicleQuestionList);
                Console.WriteLine("Vehicle addes successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                addNewVehicle();
            }
        }

        private static void answerVehicleQuestions(List<QuestionAnswer> io_VehicleQuestionList)
        {
            foreach (QuestionAnswer question in io_VehicleQuestionList)
            {
                Console.WriteLine(question.Question);
                question.Answer = Console.ReadLine();
            }

            try
            {
                sr_GrageData.ReadMessagesAs(io_VehicleQuestionList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                answerVehicleQuestions(io_VehicleQuestionList);
            }
        }

        private static void printAllData()
        {
            string licenseNumber = getStringFromUser("Please enter your license number");
            Vehicle foundVehicle;

            try
            {
                foundVehicle = sr_GrageData.FindByLicenseNumber(licenseNumber);
                Console.WriteLine(foundVehicle.GetInfo());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private static void chargeVehicle()
        {
            const int k_MinNumToFill = 0;
            const int k_Minutes = 60;
            string licenseNumber = getStringFromUser("Please enter your license number");
            float amountToFill = getFloatFromUser(k_MinNumToFill, int.MaxValue, "Please enter how much time you would want to charge your vehicle(in minutes).");
            Vehicle foundVehicle;

            try
            {
                foundVehicle = sr_GrageData.FindByLicenseNumber(licenseNumber);
                foundVehicle.FillUpEnergy(amountToFill / k_Minutes);
                Console.WriteLine("The vehicle charged successfully.");
            }
            catch (ValueOutOfRangeException rangeEx)
            {
                Console.WriteLine(rangeEx.Message);
                chargeVehicle();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void fuelVehicle()
        {
            const int k_MinNumToFill = 0;
            const int k_MinFuel = 1;

            try
            {
                string formatedFuelKinds = GrageData.GetEnumFormatedString(typeof(eKindOfFuel), out int numberOfFuelKinds);
                string licenseNumber = getStringFromUser("Please enter your license number");
                int amountToFill = getIntFromUser(k_MinNumToFill, int.MaxValue, "Please enter how much gas you would like to fill up.");
                eKindOfFuel kindOfFuel = (eKindOfFuel)getIntFromUser(k_MinFuel, numberOfFuelKinds, string.Format("Please enter the kind of fuel you want to fill {0}{1}", Environment.NewLine, formatedFuelKinds));
                Vehicle foundVehicle = sr_GrageData.FindByLicenseNumber(licenseNumber);

                foundVehicle.FillUpEnergy(kindOfFuel, amountToFill);
                Console.WriteLine("The vehicle fueled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fuelVehicle();
            }
        }

        private static void fillUpByLicense()
        {
            string licenseNumber = getStringFromUser("Please enter your license number");
            Vehicle foundVehicle;

            try
            {
                foundVehicle = sr_GrageData.FindByLicenseNumber(licenseNumber);
                foundVehicle.FillUpAirPressure();
                Console.WriteLine("The vehicle filled up successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fillUpByLicense();
            }
        }

        private static void changeVehicleStatus()
        {
            const int k_MinStatusValue = 1;
            string licenseNumber = getStringFromUser("Please enter your license number");

            try
            {
                string formatedVehicleStatusStr = GrageData.GetEnumFormatedString(typeof(eVehicleStatus), out int numberOfVehicleStatus);
                eVehicleStatus vehicleStatus = (eVehicleStatus)getIntFromUser(k_MinStatusValue, numberOfVehicleStatus, string.Format("Please choose which of the following you want to update to:{0}{1}", Environment.NewLine, formatedVehicleStatusStr));
                Vehicle foundVehicle = sr_GrageData.FindByLicenseNumber(licenseNumber);

                foundVehicle.GarageInfo.VehicleStatus = vehicleStatus;
                Console.WriteLine("The status updated successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void printByLicenseNumber()
        {
            const int k_MinStatusValue = 1;
            const int k_PrintAll = 4;
            string formatedVehicleStatusStr = GrageData.GetEnumFormatedString(typeof(eVehicleStatus), out int numberOfVehicleStatus);
            int userChoice = getIntFromUser(k_MinStatusValue, k_PrintAll, string.Format("Please choose from which status would you like to see the license plates:{0}{1}4) All Vehicle{0}", Environment.NewLine, formatedVehicleStatusStr));

            if (userChoice == k_PrintAll)
            {
                printAllLicenseNumber();
            }
            else
            {
                printByStatus((eVehicleStatus)userChoice);
            }
        }

        private static void printAllLicenseNumber()
        {
            foreach (Vehicle vehicle in sr_GrageData.DataBase)
            {
                Console.WriteLine(vehicle.LicenseNumber);
            }
        }

        private static void printByStatus(eVehicleStatus i_UserChoice)
        {
            string listOfLicenseNumber = string.Empty;

            foreach (Vehicle vehicle in sr_GrageData.DataBase)
            {
                if (vehicle.GarageInfo.VehicleStatus == i_UserChoice)
                {
                    listOfLicenseNumber += string.Format("{0} {1}", vehicle.LicenseNumber, Environment.NewLine);
                }
            }

            if (listOfLicenseNumber == string.Empty) 
            {
                listOfLicenseNumber = string.Format("No vehicles were found in the choosen status.");
            }

            Console.WriteLine(listOfLicenseNumber);
        }

        private static float getFloatFromUser(float i_MinValue, float i_MaxValue, string i_MsgToUser)
        {
            bool validInput;
            string msg = string.Empty;

            Console.WriteLine(i_MsgToUser);
            validInput = float.TryParse(Console.ReadLine(), out float inputNumber);

            while (validInput == false || inputNumber > i_MaxValue || inputNumber < i_MinValue)
            {
                msg = string.Format("You have entered invalid number please try again (choose {0} to {1}:", i_MinValue, i_MaxValue);
                Console.WriteLine(msg);
                validInput = float.TryParse(Console.ReadLine(), out inputNumber);
            }

            return inputNumber;
        }

        private static int getIntFromUser(int i_MinValue, int i_MaxValue, string i_MsgToUser)
        {
            bool validInput;
            string msg = string.Empty;

            Console.WriteLine(i_MsgToUser);
            validInput = int.TryParse(Console.ReadLine(), out int inputNumber);

            while (validInput == false || inputNumber > i_MaxValue || inputNumber < i_MinValue)
            {
                msg = string.Format("You have entered invalid number please try again (choose {0} to {1}):", i_MinValue, i_MaxValue);
                Console.WriteLine(msg);
                validInput = int.TryParse(Console.ReadLine(), out inputNumber);
            }

            return inputNumber;
        }
        
        private static string getStringFromUser(string i_MsgToUser)
        {
            Console.WriteLine(i_MsgToUser);
            return Console.ReadLine();
        }

        enum eUserOption
        {
            NewVehicle = 1,
            DisplaySameStatus,
            ChangeStatus,
            FillAirPressure,
            FillUpFuel,
            Charge,
            DisplayAllData,
            CloseGarage,
        }
    }
}
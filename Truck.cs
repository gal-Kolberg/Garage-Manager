using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        internal static readonly int sr_NumberOFWheels = 16;
        internal static readonly int sr_MaxAirPressure = 28;
        internal static readonly eKindOfFuel sr_FuelKind = eKindOfFuel.Soler;
        internal static readonly int sr_MaxGasTank = 120;
        internal float m_CargoCapacity;
        internal bool m_DangerusSubstence;
        internal int m_QnACurrentEnergy;
        internal int m_QnACargoCapacity;
        internal int m_QnADangerusMaterials;

        internal Truck(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber) : base(i_OwnerName, i_OwnerPhone, i_LicenseNumber, sr_NumberOFWheels, null)
        {
        }

        public override string GetInfo()
        {
            string msg = base.GetInfo();

            msg += string.Format(@"{0}
Cargo capacity: {1:f}
Have dangerus substence: {2}
", EnergyManager.GetInfo(), CargoCapacity, m_DangerusSubstence.ToString());

            return msg;
        }

        public override void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            base.ParseAnswers(i_Answers);
            ParseWheelInfoAnswer(i_Answers, sr_MaxAirPressure);
            CargoCapacity = float.Parse(i_Answers[m_QnACargoCapacity].Answer);
            ValidateNumberValues(0, int.MaxValue, CargoCapacity, string.Format("The cargo capacity value is out of range (negative), min value: {0}.", 0));
            DangerusSubstence = isGivenInputBool(i_Answers[m_QnADangerusMaterials].Answer);
            float currentEnergyInput = float.Parse(i_Answers[m_QnACurrentEnergy].Answer);

            ValidateNumberValues(0, sr_MaxGasTank, currentEnergyInput, string.Format("The current amount of fuel value is out of range. min value: {0} , max value {1}.", 0, sr_MaxGasTank));
            EnergyManager = new FuelProperties(sr_MaxGasTank, currentEnergyInput, sr_FuelKind);
        }

        private bool isGivenInputBool(string i_UserInput)
        {
            bool dangerusSubstence = false;
            int k_MinVal = 0;
            int k_MaxVal = 1;
            int userInput = int.Parse(i_UserInput);

            if (userInput == 1)
            {
                dangerusSubstence = true;
            }
            else if (userInput == 0)
            {
                dangerusSubstence = false;
            }
            else
            {
                throw new ValueOutOfRangeException(k_MinVal, k_MaxVal, "expecting a bool value of 1 or 0 for dangerus substence.");
            }

            return dangerusSubstence;
        }

        public override List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = base.GetQuestions();
            QuestionAnswer fuelQuestion = FuelProperties.GetFuelQuestion();
            
            m_QnACargoCapacity = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer("Please enter the truck cargo capacity.", string.Empty));
            m_QnADangerusMaterials = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer("Please enter if the truck carries and dangerus materials, enter 1 for yes, else enter 0.", string.Empty));
            m_QnACurrentEnergy = messagesAndAnswers.Count;
            messagesAndAnswers.Add(fuelQuestion);

            return messagesAndAnswers;
        }

        public override void FillUpAirPressure()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                    wheel.FillUpAir(sr_MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public override void FillUpEnergy(eKindOfFuel i_GivenFuelKind, float i_AmountToFill)
        {
            if (EnergyManager is FuelProperties fuelProperties)
            {
                fuelProperties.Fuel(i_AmountToFill, i_GivenFuelKind, sr_MaxGasTank);
            }
            else
            {
                throw new ArgumentException("Cannot fill up fuel in an electric vehicle.");
            }
        }

        public override void FillUpEnergy(float i_TimeToFill)
        {
            throw new ArgumentException("You cannot charge gas vehicle.");
        }

        internal float CargoCapacity
        {
            get
            {
                return m_CargoCapacity;
            }

            set
            {
                m_CargoCapacity = value;
            }
        }

        internal bool DangerusSubstence
        {
            get
            {
                return m_DangerusSubstence;
            }

            set
            {
                m_DangerusSubstence = value;
            }
        }
    }
}

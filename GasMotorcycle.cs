using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public sealed class GasMotorcycle : Motorcycle
    {
        internal static readonly eKindOfFuel sr_FuelKind = eKindOfFuel.Octan95;
        internal static readonly int sr_MaxGasTank = 7;
        internal int m_QnACurrentEnergy;

        internal GasMotorcycle(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber) : base(i_OwnerName, i_OwnerPhone, i_LicenseNumber)
        {
        }

        public override string GetInfo()
        {
            string msg = base.GetInfo();

            msg += string.Format(@"
{0}
", EnergyManager.GetInfo());

            return msg;
        }

        public override void FillUpAirPressure()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.FillUpAir(sr_MaxAirPressure - wheel.CurrentAirPressure);

                if (wheel.m_CurrentAirPressure > sr_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(sr_MaxAirPressure, 0, string.Format("You have enter invalid amount to fill up the air pressure, the minimum value is: {0}, and the max value is: {1}", 0, sr_MaxAirPressure));
                }
            }
        }

        public override void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            base.ParseAnswers(i_Answers);
            float currentEnergyInput = float.Parse(i_Answers[m_QnACurrentEnergy].Answer);

            ValidateNumberValues(0, sr_MaxGasTank, currentEnergyInput, string.Format("The current amount of fuel value is out of range. min value: {0} , max value {1}.", 0, sr_MaxGasTank));
            EnergyManager = new FuelProperties(sr_MaxGasTank, currentEnergyInput, sr_FuelKind);
        }

        public override List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = base.GetQuestions();
            QuestionAnswer fuelQuestions = FuelProperties.GetFuelQuestion();

            m_QnACurrentEnergy = messagesAndAnswers.Count;
            messagesAndAnswers.Add(fuelQuestions);

            return messagesAndAnswers;
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
    }
}

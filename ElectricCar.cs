using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public sealed class ElectricCar : Car
    {
        internal static readonly float sr_MaxBatteryLife = 2.1f;

        internal int m_QnACurrentEnergy;

        internal ElectricCar(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber) : base(i_OwnerName, i_OwnerPhone, i_LicenseNumber)
        {
        }

        public override string GetInfo()
        {
            string msg = base.GetInfo();

            msg += string.Format(@"{0}
", EnergyManager.GetInfo());

            return msg;
        }

        public override void FillUpAirPressure()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.FillUpAir(sr_MaxAirPressure - wheel.CurrentAirPressure);

                if (wheel.CurrentAirPressure > sr_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(sr_MaxAirPressure, 0, string.Format("You have entered amount that exceeds the maximum amount, the minimum amount is: {0}, and the maximum amount is: {1}", 0, sr_MaxAirPressure));
                }
            }
        }

        public override void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            base.ParseAnswers(i_Answers);
            float currentEnergyInput = float.Parse(i_Answers[m_QnACurrentEnergy].Answer);

            EnergyManager = new ElectricProperties(currentEnergyInput, sr_MaxBatteryLife);
            ValidateNumberValues(0, sr_MaxBatteryLife, currentEnergyInput, string.Format("The current amount battery value is out of range. min value: {0} , max value {1}.", 0, sr_MaxBatteryLife));
        }

        public override List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = base.GetQuestions();
            QuestionAnswer electricQuestions = ElectricProperties.GetElectricQuestions();

            m_QnACurrentEnergy = messagesAndAnswers.Count;
            messagesAndAnswers.Add(electricQuestions);

            return messagesAndAnswers;
        }

        public override void FillUpEnergy(eKindOfFuel i_GivenFuelKind, float i_AmountToFill)
        {
            throw new ArgumentException("You cannot fill up gas in an electric vehicle.");
        }

        public override void FillUpEnergy(float i_TimeToFill)
        {
            if (EnergyManager is ElectricProperties electricProperties)
            {
                electricProperties.Charge(i_TimeToFill);
            }
            else
            {
                throw new ArgumentException("Cannot fill up electricity in a gas vehicle.");
            }
        }
    }
}

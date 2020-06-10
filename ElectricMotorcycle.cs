using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal sealed class ElectricMotorcycle : Motorcycle
    {
        internal static readonly float sr_MaxBatteryLife = 1.2f;
        internal int m_QnACurrentEnergy;

        internal ElectricMotorcycle(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber) : base(i_OwnerName, i_OwnerPhone, i_LicenseNumber)
        {
        }

        public override void FillUpAirPressure()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.FillUpAir(sr_MaxAirPressure - wheel.CurrentAirPressure);

                if (wheel.m_CurrentAirPressure > sr_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(sr_MaxAirPressure, 0, string.Format("The amount of air pressure you entered exceeds the maximum, the minimum amount is: {0}, and the maximum amount is: {1}", 0, sr_MaxAirPressure));
                }
            }
        }

        public override void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            base.ParseAnswers(i_Answers);
            float currentEnergyInput = float.Parse(i_Answers[m_QnACurrentEnergy].Answer);

            ValidateNumberValues(0, sr_MaxBatteryLife, currentEnergyInput, string.Format("The current amount battery value is out of range. min value: {0} , max value {1}.", 0, sr_MaxBatteryLife));
            EnergyManager = new ElectricProperties(currentEnergyInput, sr_MaxBatteryLife);
        }

        public override List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = base.GetQuestions();
            QuestionAnswer electricQuestions = ElectricProperties.GetElectricQuestions();

            m_QnACurrentEnergy = messagesAndAnswers.Count;
            messagesAndAnswers.Add(electricQuestions);

            return messagesAndAnswers;
        }

        public override string GetInfo()
        {
            string msg = base.GetInfo();

            msg += string.Format(@"
{0}
", EnergyManager.GetInfo());

            return msg;
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

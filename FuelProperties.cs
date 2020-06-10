using System;

namespace Ex03.GarageLogic
{
    internal class FuelProperties : EnergyManager
    {
        internal eKindOfFuel m_KindOfFuel;

        internal FuelProperties(float i_MaxFuelAmount, float i_CurrentFuelAmount, eKindOfFuel i_KindOfFuel)
            : base(i_CurrentFuelAmount, i_MaxFuelAmount)
        {
            m_KindOfFuel = i_KindOfFuel;
        }

        public override string GetInfo()
        {
            return string.Format(@"Amount Of Fuel:  {0}
Max Fuel Amount: {1}
Kind of fuel:    {2}
Current amount in percentages: {3:f2}%
", CurrentEnergy, MaxEnergyCapacity, Enum.GetName(typeof(eKindOfFuel), KindOfFuel), CurrentEnergyPercentage);
        }

        internal static QuestionAnswer GetFuelQuestion()
        {
            return new QuestionAnswer("Please enter the amount of fuel in vehicle.", string.Empty);
        }

        internal void Fuel(float i_Amount, eKindOfFuel i_KindOfFuel, int i_MaxGasTank)
        {
            if (i_KindOfFuel == m_KindOfFuel)
            {
                if (i_Amount + CurrentEnergy <= MaxEnergyCapacity)
                {
                    CurrentEnergy += i_Amount;
                }
                else
                {
                    throw new ValueOutOfRangeException(MaxEnergyCapacity, 0, string.Format("The amount of fuel that was given exceeds the limit, the minimun value is: {0}, and the maximum value is: {1}", 0, i_MaxGasTank));
                }
            }
            else
            {
                throw new ArgumentException("The given fuel kind does not match the vehicle fuel kind.");
            }
        }

        internal eKindOfFuel KindOfFuel
        {
            get
            {
                return m_KindOfFuel;
            }
        }
    }
}

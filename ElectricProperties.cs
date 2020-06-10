namespace Ex03.GarageLogic
{
    internal class ElectricProperties : EnergyManager
    {
        internal ElectricProperties(float i_CurrentBatteryTime, float i_MaxBatteryTime) : base(i_CurrentBatteryTime, i_MaxBatteryTime)
        {
        }

        internal static QuestionAnswer GetElectricQuestions()
        {
            return new QuestionAnswer("Please enter the current battery capacity (in hours).", string.Empty);
        }

        public override string GetInfo()
        {
            return string.Format(@"Max battery time:  {0}
Time left for battery: {1}
Percentage battery life: {2:f2}%
", MaxEnergyCapacity, CurrentEnergy, CurrentEnergyPercentage);
        }

        internal void Charge(float i_TimeToCharge)
        {
            if (i_TimeToCharge + CurrentEnergy <= MaxEnergyCapacity)
            {
                CurrentEnergy += i_TimeToCharge;
            }
            else
            {
                throw new ValueOutOfRangeException(MaxEnergyCapacity, 0, string.Format("The time to charge you entered exceeds the maximum amount, the minimum amount is: {0}, and the maximum is: {1}", 0, MaxEnergyCapacity));
            }
        }
    }
}

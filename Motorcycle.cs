using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        internal static readonly int sr_NumberOfWheels = 2;
        internal static readonly int sr_MaxAirPressure = 30;
        internal eLicenseType m_TypeOfLicense;
        internal int m_EngineVolume;
        protected int m_QnALicenseType;
        protected int m_QnAEngineVolume;

        internal Motorcycle(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber) : base(i_OwnerName, i_OwnerPhone, i_LicenseNumber, sr_NumberOfWheels, null)
        {
        }

        public override List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = base.GetQuestions();

            m_QnALicenseType = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer(string.Format(@"Please Enter the license type the motorcycle u are admiting to the garage requires. Press one of the following:
{0}", GrageData.GetEnumFormatedString(typeof(eLicenseType), out int size)), string.Empty));
            m_QnAEngineVolume = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer("Please enter the engine volume", string.Empty));

            return messagesAndAnswers;
        }

        public override void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            base.ParseAnswers(i_Answers);
            ParseWheelInfoAnswer(i_Answers, sr_MaxAirPressure);
            LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_Answers[m_QnALicenseType].Answer);
            ValidateEnum(typeof(eLicenseType), (int)LicenseType);
            EngineVolume = int.Parse(i_Answers[m_QnAEngineVolume].Answer);
            ValidateNumberValues(0, int.MaxValue, EngineVolume, string.Format("The engine volume value is out of range (negative), min value: {0}.", 0));
        }

        public override abstract void FillUpAirPressure();

        public override abstract void FillUpEnergy(eKindOfFuel i_GivenFuelKind, float i_AmountToFill);

        public override abstract void FillUpEnergy(float i_TimeToFill);

        public override string GetInfo()
        {
            string msg = base.GetInfo();

            msg += string.Format(@"
License type: {0}
EngineValume: {1}", Enum.GetName(typeof(eLicenseType), LicenseType), EngineVolume);

            return msg;
        }

        internal eLicenseType LicenseType
        {
            get
            {
                return m_TypeOfLicense;
            }

            set
            {
                m_TypeOfLicense = value;
            }
        }

        internal int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }

            set
            {
                m_EngineVolume = value;
            }
        }
    }
}

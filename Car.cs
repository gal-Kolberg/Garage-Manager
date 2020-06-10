using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        internal static readonly int sr_NumberOFWheels = 4;
        internal static readonly int sr_MaxAirPressure = 32;
        internal eNumberOfDoors m_NumberOfDoors;
        internal eCarColor m_CarColor;
        protected int m_QnANumberOfDoors;
        protected int m_QnACarColor;

        internal Car(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber) : base(i_OwnerName, i_OwnerPhone, i_LicenseNumber, sr_NumberOFWheels, null)
        {
        }

        public override string GetInfo()
        {
            string msg = base.GetInfo();

            msg += string.Format(@"
Number of door: {0}
Car color: {1}
", Enum.GetName(typeof(eNumberOfDoors), m_NumberOfDoors), Enum.GetName(typeof(eCarColor), m_CarColor));

            return msg;
        }

        public override List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = base.GetQuestions();

            m_QnANumberOfDoors = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer(string.Format(@"Please Enter the number of doors the car you are admiting to the garage has. Press one of the following:
{0}", GrageData.GetEnumFormatedString(typeof(eNumberOfDoors), out int size)), string.Empty));
            m_QnACarColor = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer(string.Format(@"Please Enter the color of the car you are admiting to the garage. Press one of the following:
{0}", GrageData.GetEnumFormatedString(typeof(eCarColor), out int size1)), string.Empty));

            return messagesAndAnswers;
        }

        public override void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            base.ParseAnswers(i_Answers);
            ParseWheelInfoAnswer(i_Answers, sr_MaxAirPressure);
            NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), i_Answers[m_QnANumberOfDoors].Answer);
            CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_Answers[m_QnACarColor].Answer);
            ValidateEnum(typeof(eNumberOfDoors), (int)NumberOfDoors);
            ValidateEnum(typeof(eCarColor), (int)CarColor);
        }

        public override abstract void FillUpAirPressure();

        public override abstract void FillUpEnergy(eKindOfFuel i_GivenFuelKind, float i_AmountToFill);

        public override abstract void FillUpEnergy(float i_TimeToFill);

        internal eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
            }
        }

        internal eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }
    }
}

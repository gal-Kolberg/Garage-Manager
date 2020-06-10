using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName; 
        protected string m_LicenseNumber;
        public EnergyManager m_EnergyManager;
        public Wheel[] m_Wheels;
        public GarageInfo m_GarageInfo;
        internal int m_QnAModelName;
        internal int m_QnAWheelManufacture;
        internal int m_QnACurrentAirPressure;

        internal Vehicle(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber, int i_NumberOfWheels, EnergyManager i_EnergyManager)
        {
            m_GarageInfo = new GarageInfo(i_OwnerName, i_OwnerPhone);
            m_Wheels = new Wheel[i_NumberOfWheels];
            initWheels();
            m_EnergyManager = i_EnergyManager;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyManager = i_EnergyManager;
        }

        public virtual void ParseAnswers(List<QuestionAnswer> i_Answers)
        {
            ModelName = i_Answers[m_QnAModelName].Answer;
        }

        public virtual void ParseWheelInfoAnswer(List<QuestionAnswer> i_Answers, int i_MaxAirPressure)
        {
            const int k_MinAirPressure = 0;
            int currentAirPressure = int.Parse(i_Answers[m_QnACurrentAirPressure].Answer);

            foreach (Wheel wheel in Wheels)
            {
                wheel.ManufacturerName = i_Answers[m_QnAWheelManufacture].Answer;

                if (i_MaxAirPressure > currentAirPressure && currentAirPressure > k_MinAirPressure)
                {
                    wheel.CurrentAirPressure = currentAirPressure;
                }
                else
                {
                    throw new ValueOutOfRangeException(i_MaxAirPressure, 0, string.Format("Air pressure that was given is out of range. Min value {0}, max value {1}", 0, i_MaxAirPressure));
                }
            }
        }

        public virtual List<QuestionAnswer> GetQuestions()
        {
            List<QuestionAnswer> messagesAndAnswers = new List<QuestionAnswer>();

            m_QnAModelName = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer("Please enter the model name.", string.Empty));
            m_QnAWheelManufacture = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer("Please enter the wheel manufacturer", string.Empty));
            m_QnACurrentAirPressure = messagesAndAnswers.Count;
            messagesAndAnswers.Add(new QuestionAnswer("Please enter the wheel current air pressure", string.Empty));

            return messagesAndAnswers;
        }

        private void initWheels()
        {
            for (int i = 0; i < m_Wheels.Length; i++) 
            {
                m_Wheels[i] = new Wheel();
            }
        }

        public virtual string GetInfo()
        {
            int count = 1;
            string countWheel = string.Empty;
            string msg = string.Format(@"License number: {0}
Model name: {1}
{2}
Wheel description: 
", LicenseNumber, ModelName, GarageInfo.GetInfo());

            foreach (Wheel wheel in m_Wheels)
            {
                countWheel = string.Format("Wheel number ({0}) :{1}    ", count, Environment.NewLine);
                msg += countWheel;
                msg += wheel.GetInfo();
                count++;
            }
            
            return msg;
        }

        internal void ValidateEnum(Type i_EnumType, int i_EnumValue)
        {
            if(Enum.IsDefined(i_EnumType, i_EnumValue) == false)
            {
                throw new FormatException(string.Format("The value {0}, that was given is an invalid argument for {1}", i_EnumValue, i_EnumType.Name.Substring(1)));
            }
        }

        protected internal void ValidateNumberValues(float i_MinValue, float i_MaxValue, float i_GivenValue, string i_Msg)
        {
            if (i_GivenValue > i_MaxValue || i_GivenValue < i_MinValue) 
            {
                throw new ValueOutOfRangeException(i_MinValue, i_MaxValue, i_Msg);
            }
        }

        public abstract void FillUpAirPressure();

        public abstract void FillUpEnergy(eKindOfFuel i_GivenFuelKind, float i_AmountToFill);

        public abstract void FillUpEnergy(float i_TimeToFill);

        public EnergyManager EnergyManager
        {
            get
            {
                return m_EnergyManager;
            }

            set
            {
                m_EnergyManager = value;
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        internal string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public GarageInfo GarageInfo
        {
            get
            {
                return m_GarageInfo;
            }

            set
            {
                m_GarageInfo = value;
            }
        }
    }
}

namespace Ex03.GarageLogic
{
    public class QuestionAnswer
    {
        private string m_Question = string.Empty;
        private string m_Answer = string.Empty;

        public QuestionAnswer(string i_Question, string i_Answer)
        {
            m_Question = i_Question;
            m_Answer = i_Answer;
        }

        public string Question
        {
            get
            {
                return m_Question;
            }

            set
            {
                m_Question = value;
            }
        }

        public string Answer
        {
            get
            {
                return m_Answer;
            }

            set
            {
                m_Answer = value;
            }
        }
    }
}

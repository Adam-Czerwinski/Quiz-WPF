using System;
using System.Data;

namespace Quiz.Source.Model
{
    public class Question
    {
        #region properties
        /// <summary>
        /// ID pytania w bazie danych
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Treść pytania
        /// </summary>
        public string QuestionContent { get; private set; }
        /// <summary>
        /// Odpowiedzi ABCD przechowywanie w tablicy stringów
        /// </summary>
        public string[] AnswersABCD { get; private set; }
        /// <summary>
        /// Poprawne odpowiedzi. Np CorrectAnswers[0] = "B", CorrectAnswers[1] = "C".
        /// Wielkość tablicy może być różna dla różnych pytań
        /// </summary>
        public string[] CorrectAnswers { get; private set; }
        #endregion

        public Question(int ID, string QuestionContent, string[] AnswersABCD, string[] CorrectAnswers)
        {
            this.ID = ID;
            this.QuestionContent = QuestionContent;

            this.AnswersABCD = new string[AnswersABCD.Length];
            AnswersABCD.CopyTo(this.AnswersABCD, 0);

            this.CorrectAnswers = CorrectAnswers;
        }
    }
}

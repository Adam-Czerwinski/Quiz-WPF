using MySql.Data.MySqlClient;
using Quiz.Source.Model;
using System;
using System.Collections.Generic;

namespace Quiz.Source.DataAccessLayer
{
    class QuizRepository
    {
        private static MySqlConnection connection = DBConnection.Instance.Connection;

        #region Queries
        private static string SELECT_ALL_TESTS_WITH_EXISTING_QUESTIONS(int minimumNumberOfQuestions)
        {
            return @"SELECT t.`ID_Test`, t.`Test Name`, t.`category`,t.`Time`
            FROM Test t
		        INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
                INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
                Group by 1
	        Having Count(*)>=" + minimumNumberOfQuestions + ";";
        }
        private static string SELECT_TEST_BY_ID(int ID)
        {
            return "SELECT t.`ID_Test`, t.`Test Name`, t.`category`,t.`Time` FROM test t WHERE t.`ID_Test`=" + ID + ";";
        }
        private static string SELECT_NUMBER_OF_QUESTIONS_BY_TEST_ID(int ID)
        {
            return @"SELECT Count(*)
                FROM Test t
                INNER JOIN `Test/Question` tq ON tq.ID_Test = t.ID_Test
                INNER JOIN `Question` q ON q.ID_Question = tq.ID_Question
                WHERE t.`ID_Test`=" + ID + ";";
        }
        private static string SELECT_ALL_QUESTIONS_BY_TEST_ID(int ID)
        {
            return @"SELECT q.`ID_Question`, q.`Question`, q.`Answer A`, q.`Answer B`, q.`Answer C`, q.`Answer D`, q.`Correct Answers`
                    FROM Test t
		                INNER JOIN `Test/Question` tq ON tq.ID_Test=t.ID_Test
                        INNER JOIN `Question` q ON q.ID_Question=tq.ID_Question
                    WHERE t.`ID_Test`=" + ID + ";";
        }
        #endregion

        /// <summary>
        /// Metoda zwracająca listę wszystkich tematów w bazie danych, które mają przynajmniej jedno pytanie
        /// </summary>
        public static Test[] GetTests(int minimumNumberOfQuestions = 1)
        {
            List<Test> tests = new List<Test>();
            int ID;
            string TestName;
            string Category;
            int Time;
            using (MySqlCommand command = new MySqlCommand(SELECT_ALL_TESTS_WITH_EXISTING_QUESTIONS(minimumNumberOfQuestions), connection))
            {
                connection.Open();

                //command.ExecuteReader() zwraca obiekt DataReader
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    ID = Convert.ToInt32(dataReader["ID_Test"]);
                    TestName = dataReader["Test Name"].ToString();
                    Category = dataReader["category"].ToString();
                    Time = Convert.ToInt32(dataReader["Time"]);
                    tests.Add(new Test(ID, TestName,Category,Time));
                }
                dataReader.Close();

                connection.Close();
            }

            return tests.ToArray();
        }

        /// <summary>
        /// Zwraca Test po ID testu
        /// </summary>
        /// <param name="ID"> ID testu w bazie danych</param>
        /// <returns></returns>
        public static Test GetTestByID(int ID)
        {
            Test test = null;
            string TestName;
            string Category;
            int Time;
            using (MySqlCommand command = new MySqlCommand(SELECT_TEST_BY_ID(ID), connection))
            {
                connection.Open();

                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    TestName = dataReader["Test Name"].ToString();
                    Category = dataReader["category"].ToString();
                    Time = Convert.ToInt32(dataReader["Time"]);
                    test = new Test(ID, TestName, Category,Time);
                }
                dataReader.Close();

                connection.Close();
            }

            return test;
        }

        /// <summary>
        /// Zwraca ilość pytań należących do danego testu po ID
        /// </summary>
        /// <param name="ID">ID testu w bazie danych</param>
        /// <returns></returns>
        public static int GetCountByTestID(int ID)
        {
            int howMany = 0;
            using (MySqlCommand command = new MySqlCommand(SELECT_NUMBER_OF_QUESTIONS_BY_TEST_ID(ID), connection))
            {
                connection.Open();

                //command.ExecuteReader() zwraca obiekt DataReader
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    howMany = Convert.ToInt32(dataReader[0]);
                dataReader.Close();

                connection.Close();
            }
            return howMany;
        }

        /// <summary>
        /// Zwraca pytania należące do testu po ID
        /// </summary>
        /// <param name="ID">ID testu w bazie danych</param>
        /// <returns></returns>
        public static Question[] GetQuestionsByTestID(int ID)
        {
            List<Question> questions = null;

            int IDQuestion;
            string QuestionContent;
            string[] AnswersABCD = new string[4];
            string[] correctAnswersSplit;
            string[] CorrectAnswers;

            using (MySqlCommand command = new MySqlCommand(SELECT_ALL_QUESTIONS_BY_TEST_ID(ID), connection))
            {
                connection.Open();

                MySqlDataReader dataReader = command.ExecuteReader();

                questions = new List<Question>();

                while (dataReader.Read())
                {
                    IDQuestion = Convert.ToInt32(dataReader["ID_Question"]);
                    QuestionContent = dataReader["Question"].ToString();

                    AnswersABCD[0] = dataReader["Answer A"].ToString();
                    AnswersABCD[1] = dataReader["Answer B"].ToString();
                    AnswersABCD[2] = dataReader["Answer C"].ToString();
                    AnswersABCD[3] = dataReader["Answer D"].ToString();

                    correctAnswersSplit = dataReader["Correct Answers"].ToString().Split(' ');
                    CorrectAnswers = new string[correctAnswersSplit.Length];
                    correctAnswersSplit.CopyTo(CorrectAnswers, 0);

                    questions.Add(new Question(IDQuestion, QuestionContent, AnswersABCD, CorrectAnswers));

                }
                dataReader.Close();

                connection.Close();
            }
            return questions.ToArray();
        }
    }
}

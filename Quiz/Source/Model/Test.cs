using System;
using System.Data;

namespace Quiz.Source.Model
{
    public class Test
    {
        /// <summary>
        /// ID testu w bazie danych
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Nazwa testu
        /// </summary>
        public string TestName { get; private set; }
        /// <summary>
        /// Kategoria testu
        /// </summary>
        public string Category { get; private set; }
        /// <summary>
        /// Czas na test w sekundach
        /// </summary>
        public int Time { get; private set; }

        public Test(int ID, string TestName, string Category, int Time)
        {
            this.ID = ID;
            this.TestName = TestName;
            this.Category = Category;
            this.Time = Time;
        }
    }
}

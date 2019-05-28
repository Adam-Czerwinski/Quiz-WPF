using System;

namespace Quiz.Source.Messaging
{
    public static class Notifications
    {
        public static readonly string ChangeMainViewToQuizSolve = Guid.NewGuid().ToString();
        public static readonly string ChangeMainViewToWelcome = Guid.NewGuid().ToString();
        public static readonly string SetUpThisQuiz = Guid.NewGuid().ToString();
    }
}

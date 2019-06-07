using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Quiz.Source.DataAccessLayer;
using Quiz.Source.Dialogs;
using Quiz.Source.Messaging;
using Quiz.Source.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using System.Timers;

namespace Quiz.Source.ViewModel
{

    public class QuizSolveViewModel : ViewModelBase
    {

        #region Members
        private enum NextQuestionContentValue { Następne, Zakończ }
        private Question[] _questions;
        //Zauważ, że index zaczyna się od 1! Po to, żeby aktualne pytanie wyświetlało się zaczynając od 1 a nie od 0
        private int _currentQuestionIndex = 1;
        private Question _currentQuestion;
        private bool _isAbleToGoPreviousQuestion;
        private string _nextQuestionOrEndQuiz;
        private int _score;
        private bool[][] _userAnswersABCDCheckBoxes;
        private string[][] _userAnswers;
        private IDialogService _dialogService;
        private int _timeLeft;
        private Timer _timer;
        #endregion

        #region Properties
        /// <summary>
        /// Ilość pytań dla danego testu
        /// </summary>
        public int NumberOfQuestions { get; private set; }
        /// <summary>
        /// Aktualne pytanie wyświetlanie w View
        /// </summary>
        public Question CurrentQuestion
        {
            get
            {
                return _currentQuestion;
            }
            private set
            {
                _currentQuestion = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Aktualny test
        /// </summary>
        public Test Test { get; private set; }
        /// <summary>
        /// Index aktualnego pytania w array. Jest domyślnie ustawione na 1!!!!1
        /// </summary>
        public int CurrentQuestionIndex
        {
            get
            {
                return _currentQuestionIndex;
            }
            private set
            {
                if (_currentQuestionIndex != value)
                {
                    _currentQuestionIndex = value;
                    RaisePropertyChanged();
                    CurrentAnswersABCDCheckBoxes = _userAnswersABCDCheckBoxes[value - 1];
                    if (_currentQuestionIndex <= 1)
                        IsAbleToGoPreviousQuestion = false;
                    else
                        IsAbleToGoPreviousQuestion = true;
                }

                if (_currentQuestionIndex == NumberOfQuestions)
                    NextQuestionOrEndQuiz = NextQuestionContentValue.Zakończ.ToString();
                else
                    NextQuestionOrEndQuiz = NextQuestionContentValue.Następne.ToString();

            }
        }
        /// <summary>
        /// Dotyczy tego żeby włączyć/wyłączyć przycisk "Poprzednie"
        /// </summary>
        public bool IsAbleToGoPreviousQuestion
        {
            get
            {
                return _isAbleToGoPreviousQuestion;
            }
            set
            {
                if (_isAbleToGoPreviousQuestion != value)
                {
                    _isAbleToGoPreviousQuestion = value;
                    RaisePropertyChanged();
                }
            }
        }
        /// <summary>
        /// Jeżeli ostatnie pytanie to ustawia na "Zakończ quiz", inaczej na "Następne"
        /// Aktualizowane jest to w CurrentQuestionIndex bo tylko od tego (CurrentQuestionIndex) propertiesu zależy stan poniższego propertiesu
        /// </summary>
        public string NextQuestionOrEndQuiz
        {
            get
            {
                return _nextQuestionOrEndQuiz;
            }
            set
            {
                if (!_nextQuestionOrEndQuiz.Equals(value))
                {
                    _nextQuestionOrEndQuiz = value;
                    RaisePropertyChanged();
                }
            }
        }
        /// <summary>
        /// Odpowiedzi ABCD (czy zaznaczone true or false) aktualnego pytania.
        /// </summary>
        public bool[] CurrentAnswersABCDCheckBoxes
        {
            get
            {
                return _userAnswersABCDCheckBoxes[_currentQuestionIndex - 1];
            }
            set
            {
                _userAnswersABCDCheckBoxes[_currentQuestionIndex - 1] = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Pozostały czas na rozwiązanie quizu wyrażony w sekundach
        /// </summary>
        public int TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public QuizSolveViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            //interwał co 1 sekundę
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            #region Zdarzenia
            Messenger.Default.Register<Test>(this, Notifications.SetUpThisQuiz, ReceiveChoosenTest);
            #endregion

            #region Komendy
            NextQuestionOrSubmitQuizCommand = new RelayCommand(NextQuestionOrSubmit);
            //Nie zależy od CanExecute, ponieważ przycisk jest wyłączany, jeżeli pytanie jest pierwsze.
            PreviousQuestionCommand = new RelayCommand(PreviousQuestion);

            //------------------W TRAKCIE
            //Wywołaj po potwierdzeniu wyjścia
            QuitQuizCommand = new RelayCommand(QuitQuiz);

            #endregion
        }

        /// <summary>
        /// Metoda wykonywująca się co określony czas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (TimeLeft > 0)
                TimeLeft -= 1;
            else if (TimeLeft == 0)
            {
                _timer.Stop();
                _timer.AutoReset = false;
                _timer.Enabled = false;
                string title = "Koniec czasu!";
                SubmitQuiz(title);
            }
        }

        /// <summary>
        /// Funkcja wykonująca się gdy otrzyma się wiadomość
        /// </summary>
        private void ReceiveChoosenTest(Test obj)
        {
            //pobierz pytania
            _questions = QuizRepository.GetQuestionsByTestID(obj.ID);
            NumberOfQuestions = _questions.Length;
            //ustaw aktualne pytanie na pierwsze pytanie
            CurrentQuestion = _questions[0];

            //Rezerwujemy dla każdej tablicy 4 elementy (odpowiedzi ABCD)
            _userAnswersABCDCheckBoxes = new bool[NumberOfQuestions][];
            for (int i = 0; i < NumberOfQuestions; i++)
                _userAnswersABCDCheckBoxes[i] = new bool[4];

            _userAnswers = new string[NumberOfQuestions][];
            //ustaw przycisk na "Zakończ" lub "Następne" w zależności od ilości pytań
            if (NumberOfQuestions > 1)
                _nextQuestionOrEndQuiz = NextQuestionContentValue.Następne.ToString();
            else
                _nextQuestionOrEndQuiz = NextQuestionContentValue.Zakończ.ToString();

            //ustaw temat na ten który otrzymałeś
            Test = obj;
            TimeLeft = Test.Time;
        }

        /// <summary>
        /// Wyjście z quizu do okna Welcome
        /// </summary>
        public ICommand QuitQuizCommand { get; private set; }
        private void QuitQuiz()
        {
            string message = "Czy chcesz wyjść z quizu?";
            string title = "Wyjście do menu głównego";

            //uruchom metodę asynchroniczną synchronicznie
            bool result = AsyncUtil.RunSync<bool>(() => _dialogService.ShowMessage(message, title, null, null, null));

            if (result)
                Messenger.Default.Send("notification", Notifications.ChangeMainViewToWelcome);
        }

        /// <summary>
        /// W zależności od numeru aktualnego pytania wykonuje komende odpowiadającą
        /// za pokazaniem następnego pytania lub zakończenie quizu
        /// </summary>
        public ICommand NextQuestionOrSubmitQuizCommand { get; private set; }
        private void NextQuestionOrSubmit()
        {
            if (_currentQuestionIndex != NumberOfQuestions)
                NextQuestion();
            else
                SubmitQuiz();
        }
        private void NextQuestion()
        {
            CurrentQuestion = _questions[CurrentQuestionIndex++];
        }
        private void SubmitQuiz(string title="Wynik")
        {
            GetUserAnswers();
            GetUserScore();

            string message = $"Uzyskano { _score} na {NumberOfQuestions} punktów";

            //tytuł jest w parametrach
            //uruchom metodę asynchroniczną synchronicznie
            AsyncUtil.RunSync(() => _dialogService.ShowMessage(message, title, null, () => Messenger.Default.Send("notification", Notifications.ChangeMainViewToWelcome)));
        }
        private void GetUserAnswers()
        {
            int ileZaznaczonychOdpowiedzi;
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                ileZaznaczonychOdpowiedzi = 0;
                //czy zaznaczona odpowiedz A
                if (_userAnswersABCDCheckBoxes[i][0])
                    ileZaznaczonychOdpowiedzi++;
                //czy zaznaczona odpowiedz B
                if (_userAnswersABCDCheckBoxes[i][1])
                    ileZaznaczonychOdpowiedzi++;
                //czy zaznaczona odpowiedz C
                if (_userAnswersABCDCheckBoxes[i][2])
                    ileZaznaczonychOdpowiedzi++;
                //czy zaznaczona odpowiedz D
                if (_userAnswersABCDCheckBoxes[i][3])
                    ileZaznaczonychOdpowiedzi++;

                _userAnswers[i] = new string[ileZaznaczonychOdpowiedzi];
            }

            int j, k;
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                j = 0;
                k = _userAnswers[i].Length;
                if (_userAnswersABCDCheckBoxes[i][0])
                {
                    _userAnswers[i][j++] = "A";
                    if (j - 1 == k)
                        continue;
                }
                if (_userAnswersABCDCheckBoxes[i][1])
                {
                    _userAnswers[i][j++] = "B";
                    if (j - 1 == k)
                        continue;
                }
                if (_userAnswersABCDCheckBoxes[i][2])
                {
                    _userAnswers[i][j++] = "C";
                    if (j - 1 == k)
                        continue;
                }
                if (_userAnswersABCDCheckBoxes[i][3])
                {
                    _userAnswers[i][j++] = "D";
                    if (j - 1 == k)
                        continue;
                }
            }

        }
        private void GetUserScore()
        {
            //Sprawdza odpowiedzi użytkownika z poprawnymi odpowiedziami
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                //jeżeli brak odpowiedzi
                if (_userAnswers[i] is null)
                    continue;

                //jezeli ilosc odpowiedzi jest różna
                if (_userAnswers[i].Length != _questions[i].CorrectAnswers.Length)
                    continue;

                for (int j = 0; j < _userAnswers[i].Length; j++)
                {
                    //jeżeli już pierwsza odpowiedź jest nieprawidłowa to odpowiedzi są złe.
                    //Odpowiedzi w bazie danych są posortowane rosnąco (np. jezeli poprawne są odpowiedzi C i A
                    //to będzie w CorrectAnswer przechowywane kolejno A,C.
                    //Tak samo działają tutaj _userAnswers
                    if (_userAnswers[i][j] != _questions[i].CorrectAnswers[j])
                        continue;
                }

                //jezeli pętla doszla do tego miejca to znaczy, że poprawna odpowiedź
                _score++;
            }
        }

        /// <summary>
        /// Akcja przycisku odpowiadającego za poprzednie pytanie
        /// </summary>
        public ICommand PreviousQuestionCommand { get; private set; }

        private void PreviousQuestion()
        {
            //Czemu taki SKOMPLIKOWANY ALGORYTM?
            //CurrentQuestionIndex ma domyślną wartość 1.
            //Po naciśnięciu przycisku "Następne" ustawiam CurrentQuestion = _questions[CurrentQuestionIndex++];
            //czyli najpierw ustawiam wartość 1(czyli następne pytanie w array) a potem inkrementuję.
            //klikając "Poprzednie", wartość CurrentQuestionIndex jest 2, więc zmniejszenie o 1 nic nie da.
            //Wracamy do poprzedniego pytania czyli do indexu 0 (poprzedni był 2)
            //I wracacamy do punktu wyjściowego zmniejszając CurrentQuestionIndex o 1
            CurrentQuestion = _questions[CurrentQuestionIndex - 2];
            CurrentQuestionIndex -= 1;
        }
    }
}


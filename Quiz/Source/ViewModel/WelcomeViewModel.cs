using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Quiz.Source.DataAccessLayer;
using Quiz.Source.Messaging;
using Quiz.Source.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Quiz.Source.ViewModel
{
    public class WelcomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Lista testów
        /// </summary>
        public Test[] Tests { get; private set; }

        /// <summary>
        /// Aktualnie wybrany test
        /// </summary>
        private Test _selectedTest;
        public Test SelectedTest
        {
            get
            {
                return _selectedTest;
            }
            set
            {
                if (_selectedTest != value)
                {
                    _selectedTest = value;
                    RaisePropertyChanged();

                    isSelected = true;
                    NumberOfQuestion = QuizRepository.GetCountByTestID(_selectedTest.ID).ToString();
                }
            }
        }
        /// <summary>
        /// Ilość pytań w aktualnie wybranym teście
        /// </summary>
        private string _numberOfQuestion;
        public string NumberOfQuestion
        {
            get
            {
                return _numberOfQuestion;
            }
            set
            {
                if (_numberOfQuestion != value)
                {
                    _numberOfQuestion = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Wartość bool sprawdzająca czy SelectedTest jest nullem
        /// </summary>
        private bool _isSelected;
        public bool isSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged();

                    //Wtedy sprawdź czy można wywołać przycisk
                    StartQuizCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public WelcomeViewModel()
        {
            //Przypisanie wywoływacza przycisku "Start"
            StartQuizCommand = new RelayCommand(SendQuiz, () => isSelected );

            //Ładuje testy
            Tests = QuizRepository.GetTests();
        }

        /// <summary>
        /// Akcja po naciśnięciu wybraniu testu i kliknięciu przycisku rozpoczynającego test
        /// </summary>
        public RelayCommand StartQuizCommand { get; private set; }

        /// <summary>
        /// Akcja dla StartQuizCommand
        /// </summary>
        private void SendQuiz()
        {
            //Wysłanie informacji o pokazaniu okna quizu.
            Messenger.Default.Send("notification", Notifications.ChangeMainViewToQuizSolve);
            //Wysłanie informacji wraz z wybranym testem.
            Messenger.Default.Send(SelectedTest, Notifications.SetUpThisQuiz);
        }

    }
}

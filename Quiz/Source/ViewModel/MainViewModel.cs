using System;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Quiz.Source.Messaging;
using Quiz.Source.Model;

namespace Quiz.Source.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private WelcomeViewModel _welcomeViewModel;
        private QuizSolveViewModel _quizSolveViewModel;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            private set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            //Na pocz�tku za�aduj WelcomeViewModel
            _welcomeViewModel = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            CurrentViewModel = _welcomeViewModel;

            //nas�uchuj zdarzenia. W wyniku czego zmie� CurrentViewModel na QuizSolveViewModel
            Messenger.Default.Register<string>(this, Notifications.ChangeMainViewToQuizSolve, obj =>
            {
                //Ta wiadomo�� jest wysy�ana z poziomu WelcomeViewModel
                //To oznacza, �e mo�na usun�� instancj� welcomeViewModel, �eby p�niej utworzy� now�
                //w wyniku czego b�dzie mniej problem�w (nie trzeba wraca� do domy�lnych warto�ci jak np. SelectedTest = null)
                _quizSolveViewModel = ServiceLocator.Current.GetInstance<QuizSolveViewModel>();
                CurrentViewModel = _quizSolveViewModel;
                SimpleIoc.Default.Unregister(_welcomeViewModel);
            });
            //nas�uchuj zdarzenia. W wyniku czego zmie� QuizSolveViewModel na CurrentViewModel
            Messenger.Default.Register<string>(this, Notifications.ChangeMainViewToWelcome, obj =>
            {
                //Podobnie jak wy�ej
                _welcomeViewModel = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
                CurrentViewModel = _welcomeViewModel;
                SimpleIoc.Default.Unregister(_quizSolveViewModel);
            });
        }
    }
}
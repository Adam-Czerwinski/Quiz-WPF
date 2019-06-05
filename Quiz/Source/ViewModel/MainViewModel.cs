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
            //Na pocz¹tku za³aduj WelcomeViewModel
            _welcomeViewModel = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            CurrentViewModel = _welcomeViewModel;

            //nas³uchuj zdarzenia. W wyniku czego zmieñ CurrentViewModel na QuizSolveViewModel
            Messenger.Default.Register<string>(this, Notifications.ChangeMainViewToQuizSolve, obj =>
            {
                //Ta wiadomoœæ jest wysy³ana z poziomu WelcomeViewModel
                //To oznacza, ¿e mo¿na usun¹æ instancjê welcomeViewModel, ¿eby póŸniej utworzyæ now¹
                //w wyniku czego bêdzie mniej problemów (nie trzeba wracaæ do domyœlnych wartoœci jak np. SelectedTest = null)
                _quizSolveViewModel = ServiceLocator.Current.GetInstance<QuizSolveViewModel>();
                CurrentViewModel = _quizSolveViewModel;
                SimpleIoc.Default.Unregister(_welcomeViewModel);
            });
            //nas³uchuj zdarzenia. W wyniku czego zmieñ CurrentViewModel na WelcomeViewModel
            Messenger.Default.Register<string>(this, Notifications.ChangeMainViewToWelcome, obj =>
            {
                //Podobnie jak wy¿ej
                _welcomeViewModel = ServiceLocator.Current.GetInstance<WelcomeViewModel>();
                CurrentViewModel = _welcomeViewModel;
                SimpleIoc.Default.Unregister(_quizSolveViewModel);
            });
        }
    }
}
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace Quiz.Source.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();
            SimpleIoc.Default.Register<QuizSolveViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public WelcomeViewModel Welcome
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            }
        }

        public QuizSolveViewModel Quiz
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizSolveViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
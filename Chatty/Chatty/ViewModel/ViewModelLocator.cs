/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Chatty"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Chatty.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SignUpViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();
            //SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<InviteViewModel>();
            SimpleIoc.Default.Register<NewGroupViewModel>();
            SimpleIoc.Default.Register<AddUserViewModel>();
        }

        public MainViewModel Main { get { return ServiceLocator.Current.GetInstance<MainViewModel>(); } }
        public LoginViewModel Login{ get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); } }
        public SignUpViewModel SignUp { get { return ServiceLocator.Current.GetInstance<SignUpViewModel>(); } }
        public ChatViewModel Chat { get { return ServiceLocator.Current.GetInstance<ChatViewModel>(); } }
        public InviteViewModel Invite { get { return ServiceLocator.Current.GetInstance<InviteViewModel>(); } }
        public NewGroupViewModel Group { get { return ServiceLocator.Current.GetInstance<NewGroupViewModel>(); } }
        public AddUserViewModel AddUser { get { return ServiceLocator.Current.GetInstance<AddUserViewModel>(); } }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
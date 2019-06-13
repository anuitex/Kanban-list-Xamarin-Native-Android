using Acr.UserDialogs;
using KanbanList.Core.Configurations;
using KanbanList.Core.Helpers;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;

namespace KanbanList.Core.ViewModels.Base
{
    public abstract class BaseViewModel : MvxViewModel
    {
        #region Variables
        private Dictionary<string, string> _errorMessageDictionary;
        public Dictionary<string, string> ErrorMessageDictionary
        {
            get => _errorMessageDictionary;
            set => SetProperty(ref _errorMessageDictionary, value);
        }
        protected IMvxNavigationService NavigationService;
        protected IUserDialogs UserDialogs;
        #endregion Variables

        #region Constructors

        protected BaseViewModel()
        {
            UserDialogs = Mvx.IoCProvider.Resolve<IUserDialogs>();
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        #endregion Constructors

        #region Properties
        public virtual IMvxCommand BackCommand => new MvxCommand(() => NavigationService.Close(this));

        public virtual IMvxCommand LogoutCommand => new MvxCommand(LogOuting);
        #endregion Properties

        #region Methods

        public async virtual void LogOuting()
        {
            bool alertResult = await UserDialogs.ConfirmAsync("Do you want exit from this account?", "Exit action", "Exit", "Cancel");
            if (alertResult)
            {
                SecureStorageHelper.ClearSecureStorage();
                await NavigationService.Navigate<LoginViewModel>();
                return;
            }

        }

        #endregion Methods
    }

    public abstract class BaseViewModel<TParameter> : MvxViewModel<TParameter>
        where TParameter : class
    {

        #region Variables
        private Dictionary<string, string> _errorMessageDictionary;
        public Dictionary<string, string> ErrorMessageDictionary
        {
            get => _errorMessageDictionary;
            set => SetProperty(ref _errorMessageDictionary, value);
        }
        protected IMvxNavigationService NavigationService;
        protected IUserDialogs UserDialogs;
        #endregion Variables

        #region Constructors

        protected BaseViewModel()
        {
            UserDialogs = Mvx.IoCProvider.Resolve<IUserDialogs>();
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        #endregion Constructors

        #region Properties
        public virtual IMvxCommand BackCommand => new MvxCommand(() => NavigationService.Close(this));

        public virtual IMvxCommand LogoutCommand => new MvxCommand(LogOuting);
        #endregion Properties

        #region Methods

        public async virtual void LogOuting()
        {
            bool alertResult = await UserDialogs.ConfirmAsync("Do you want exit from this account?", "Exit action", "Exit", "Cancel");
            if (alertResult)
            {
                SecureStorageHelper.ClearSecureStorage();
                await NavigationService.Navigate<LoginViewModel>();
                return;
            }

        }

        #endregion Methods
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        #region Variables
        private Dictionary<string, string> _errorMessageDictionary;
        public Dictionary<string, string> ErrorMessageDictionary
        {
            get => _errorMessageDictionary;
            set => SetProperty(ref _errorMessageDictionary, value);
        }
        protected IMvxNavigationService NavigationService;
        protected IUserDialogs UserDialogs;
        #endregion Variables

        #region Constructors

        protected BaseViewModel()
        {
            UserDialogs = Mvx.IoCProvider.Resolve<IUserDialogs>();
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        #endregion Constructors

        #region Properties
        public virtual IMvxCommand BackCommand => new MvxCommand(() => NavigationService.Close(this));

        public virtual IMvxCommand LogoutCommand => new MvxCommand(LogOuting);
        #endregion Properties

        #region Methods

        public async virtual void LogOuting()
        {
            bool alertResult = await UserDialogs.ConfirmAsync("Do you want exit from this account?", "Exit action", "Exit", "Cancel");
            if (alertResult)
            {
                SecureStorageHelper.ClearSecureStorage();
                await NavigationService.Navigate<LoginViewModel>();
                return;
            }

        }

        #endregion Methods
    }
}

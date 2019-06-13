using KanbanList.Core.Configurations;
using KanbanList.Core.Helpers;
using KanbanList.Core.Models;
using KanbanList.Core.Providers.Interfaces;
using KanbanList.Core.Services.Interfaces;
using KanbanList.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace KanbanList.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Variables

        private readonly ILoginProvider _loginProvider;
        private readonly IValidationService _validationServices;

        #endregion Variables

        #region Constructors

        public LoginViewModel(ILoginProvider loginProvider, IValidationService validationServices)
        {
            _validationServices = validationServices;
            _loginProvider = loginProvider;
        }

        #endregion Constructors

        #region Properties
        private LoginModelResult _loginModel = new LoginModelResult();
        public LoginModelResult LoginModel
        {
            get => _loginModel;
            set => SetProperty(ref _loginModel, value);
        }

        #endregion Properties

        #region Commands  

        public IMvxCommand LoginCommand => new MvxCommand(Login);

        public IMvxCommand RegistrationCommand => new MvxCommand(() =>
        {
            NavigationService.Navigate<RegistrationViewModel>();
        });

        #endregion Commands

        #region Methods

        private async void Login()
        {
            UserDialogs.ShowLoading();
            ValidationModelResult validationResult = _validationServices.ValidateModel(LoginModel);

            if(!validationResult.IsValid)
            {
                ErrorMessageDictionary = validationResult.ErrorMessages;
                UserDialogs.HideLoading();
                return;
            }

            UserModelResult user = await _loginProvider.Login(LoginModel);

            if (user != null)
            {
                SecureStorageHelper.Save(Constants.CurrentUserId, user.Id);
                SecureStorageHelper.Save(Constants.OranizationName, user.OrganizationName);
                SecureStorageHelper.Save(Constants.IsLogined, true);
                await NavigationService.Navigate<DashboardViewModel>();
                UserDialogs.HideLoading();
                return;
            }
            UserDialogs.HideLoading();
            UserDialogs.Alert("Something is wrong", "Login error");
        }

        #endregion Methods
    }
}

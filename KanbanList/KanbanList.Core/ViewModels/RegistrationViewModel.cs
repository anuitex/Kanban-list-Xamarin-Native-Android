using KanbanList.Core.Models;
using KanbanList.Core.Services.Interfaces;
using KanbanList.Core.ViewModels.Base;
using MvvmCross.Commands;
using System;

namespace KanbanList.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {

        #region Variables

        private readonly IRegistrationService _registrationService;
        private readonly IValidationService _validationService;

        #endregion Variables

        #region Constructors

        public RegistrationViewModel(IRegistrationService registrationService, IValidationService validationService)
        {
            _registrationService = registrationService;
            _validationService = validationService;
        }

        #endregion Constructors

        #region Properties
        
        private UserModelResult _userModel = new UserModelResult();
        public UserModelResult UserModel
        {
            get => _userModel;
            set => SetProperty(ref _userModel, value);
        }

        #endregion Properties

        #region Commands  

        public IMvxCommand RegistrationCommand => new MvxCommand(async () =>
        {
            ValidationModelResult validationResult = _validationService.ValidateModel(UserModel);
            if (!validationResult.IsValid)
            {
                ErrorMessageDictionary = validationResult.ErrorMessages;
                return;
            }

            UserModel.Id = Guid.NewGuid().ToString();

            validationResult = await _registrationService.Registrate(UserModel);
            if (!validationResult.IsValid)
            {
                ErrorMessageDictionary = validationResult.ErrorMessages;
                return;
            }

            UserDialogs.Toast("User is registrated");

            await NavigationService.Navigate<LoginViewModel>();
        });

        #endregion Commands
    }
}

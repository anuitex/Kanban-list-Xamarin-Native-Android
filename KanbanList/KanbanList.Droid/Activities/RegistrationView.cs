using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using KanbanList.Core.Models;
using KanbanList.Core.ViewModels;
using KanbanList.Droid.Base.Activities;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace KanbanList.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "Registration",
        NoHistory = true,
        Theme = "@style/NoActionBarTheme",
        LaunchMode = LaunchMode.SingleTop,
        WindowSoftInputMode = Android.Views.SoftInput.StateHidden,
        Name = "kanbanList.droid.activities.RegistrationView")]
    public class RegistrationView : BaseActivity<RegistrationViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.registration_view);

            SetBinding();
        }

        public void SetBinding()
        {
            var set = this.CreateBindingSet<RegistrationView, RegistrationViewModel>();

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textFirstNameInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(UserModelResult.FirstName)]);

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textLastNameInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(UserModelResult.LastName)]);

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textEmailInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(UserModelResult.Email)]);

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textPasswordInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(UserModelResult.Password)]);

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textConfirmPasswordInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(UserModelResult.ConfirmPassword)]);

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textOrganizationNameInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(UserModelResult.OrganizationName)]);

            set.Apply();
        }
    }
}
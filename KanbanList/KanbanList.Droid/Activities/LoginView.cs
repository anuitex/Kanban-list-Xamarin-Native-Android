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
    [Activity(Label = "Sign in",
        //NoHistory = true,
        Theme = "@style/NoActionBarTheme",
        LaunchMode = LaunchMode.SingleTop,
        WindowSoftInputMode = Android.Views.SoftInput.StateHidden,
        Name = "kanbanList.droid.activities.LoginView")]
    public class LoginView : BaseActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.login_view);

            SetBinding();
        }

        public void SetBinding()
        {
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textUsernameInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(LoginModelResult.Username)]);

            set.Bind(FindViewById<TextInputLayout>(Resource.Id.textPasswordInputLayout))
                .For(x => x.Error)
                .To(vm => vm.ErrorMessageDictionary[nameof(LoginModelResult.Password)]);

            set.Apply();
        }
    }
}
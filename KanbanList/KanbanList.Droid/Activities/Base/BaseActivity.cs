using Android.OS;
using Android.Runtime;
using KanbanList.Droid.Services.Implementations;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;
using Plugin.Permissions;

namespace KanbanList.Droid.Base.Activities
{
    public class BaseActivity<TViewModel> : MvxAppCompatActivity<TViewModel> where TViewModel : MvxViewModel
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            PickerDialogService.Init(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
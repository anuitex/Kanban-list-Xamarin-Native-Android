
using Android.App;
using Android.Content.PM;
using Android.OS;
using KanbanList.Core.ViewModels;
using KanbanList.Droid.Base.Activities;
using KanbanList.Droid.Helpers;
using MvvmCross.Platforms.Android.Binding.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace KanbanList.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "Create task",
        Theme = "@style/NoActionBarTheme",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenLayout,
        WindowSoftInputMode = Android.Views.SoftInput.StateHidden,
        Name = "kanbanList.droid.activities.CreateTaskView")]
    public class CreateTaskView : BaseActivity<CreateTaskViewModel>
    {
        private MvxSpinner _spinner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.create_task_view);

            _spinner = FindViewById<MvxSpinner>(Resource.Id.assignedUserList);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _spinner.DropDownVerticalOffset = SizeHelper.DpToPx(40);
        }
    }
}
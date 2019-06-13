using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using KanbanList.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace KanbanList.Droid
{
    public class MainAndroidViewPresenter : MvxAppCompatViewPresenter
    {
        public MainAndroidViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        protected override Intent CreateIntentForRequest(MvxViewModelRequest request)
        {
            Intent intent = base.CreateIntentForRequest(request);

            if (request.ViewModelType == typeof(LoginViewModel))
            {
                intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            }

            return intent;
        }
    }
}
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using KanbanList.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
using System;

namespace KanbanList.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication()
        {
        }

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            UserDialogs.Init(this);
        }
    }
}
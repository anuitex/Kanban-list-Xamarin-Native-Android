using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace KanbanList.Droid
{
    [MvxActivityPresentation]
    [Activity(
            MainLauncher = true,
            Icon = "@mipmap/ic_launcher",
            Theme = "@style/Theme.Splash",
            NoHistory = true,
            ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.splash_screen)
        {
        }
    }
}
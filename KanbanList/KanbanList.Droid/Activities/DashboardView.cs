using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using KanbanList.Core.Enums;
using KanbanList.Core.ViewModels;
using KanbanList.Droid.Base.Activities;
using KanbanList.Droid.Layouts;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace KanbanList.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard",
        Theme = "@style/NoActionBarTheme",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenLayout,
        Name = "kanbanList.droid.activities.DashboardView")]
    public class DashboardView : BaseActivity<DashboardViewModel>
    {
        private HorizontalScrollView _horizontalScrollView;
        private KanbanLayout _kanbanLayout;
        private LinearLayout.LayoutParams _layoutParams;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.dashboard_view);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _horizontalScrollView = FindViewById<HorizontalScrollView>(Resource.Id.horizontal_scroll_view);

            _kanbanLayout = new KanbanLayout(this, ViewModel.ShowErrorMovedMessage, ViewModel.UpdateTask);

            _layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);

            _horizontalScrollView.AddView(_kanbanLayout, _layoutParams);

            _kanbanLayout.SetListValue(ViewModel.AllSectionItems);

            _kanbanLayout.TabLayoutBackgoundColor = Android.Graphics.Color.Orange;
            _kanbanLayout.TitleTextColor = Android.Graphics.Color.White;
        }

        protected override void OnStart()
        {
            base.OnStart();
            _kanbanLayout.ItemSelected += _kanbanLayout_ItemSelected;
        }

        private void _kanbanLayout_ItemSelected(object sender, Core.Models.TaskModelResult e)
        {
            ViewModel?.ItemSelectedCommand?.Execute(e);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _kanbanLayout.ItemSelected -= _kanbanLayout_ItemSelected;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.dashboard_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.dashboard_menu_logout)
            {
                ViewModel?.LogoutCommand?.Execute();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
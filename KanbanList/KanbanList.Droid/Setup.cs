using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Widget;
using KanbanList.Core;
using KanbanList.Core.Services.Interfaces;
using KanbanList.Droid.Converters;
using KanbanList.Droid.Services.Implementations;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters;
using System.Collections.Generic;
using System.Reflection;

namespace KanbanList.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(ViewPager).Assembly,
            typeof(MvxRecyclerView).Assembly,
            typeof(MvxSwipeRefreshLayout).Assembly,
        };

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            Mvx.IoCProvider.RegisterSingleton<IFileService>(() => new FileService());
            Mvx.IoCProvider.RegisterSingleton<IPickerDialogService>(() => new PickerDialogService());
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("DateToDateTimeString", new DateToDateTimeStringConverter());
            registry.AddOrOverwrite("BytesToBitmap", new BytesToBitmapValueConverter());
            registry.AddOrOverwrite("StringToDouble", new StringToDoubleConverter());
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MainAndroidViewPresenter(AndroidViewAssemblies);
        }
    }
}
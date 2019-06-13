using KanbanList.Core.Configurations;
using KanbanList.Core.Helpers;
using KanbanList.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace KanbanList.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            bool isLogined = SecureStorageHelper.GetBoolean(Constants.IsLogined, false);
            if (isLogined)
            {
                return NavigationService.Navigate<DashboardViewModel>();
            }
            return NavigationService.Navigate<LoginViewModel>();
        }
    }
}

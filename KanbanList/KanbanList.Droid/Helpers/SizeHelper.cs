using Android.Content.Res;

namespace KanbanList.Droid.Helpers
{
    public class SizeHelper
    {
        public static int DpToPx(int dp)
        {
            return (int)(dp * Resources.System.DisplayMetrics.Density);
        }
    }
}
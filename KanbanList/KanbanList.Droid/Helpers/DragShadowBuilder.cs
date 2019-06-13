using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

namespace KanbanList.Droid.Helpers
{
    public class DragShadowBuilder : View.DragShadowBuilder
    {
        #region Variables

        private Drawable _shadow;

        #endregion Variables

        #region Constructors

        public DragShadowBuilder(Context context)
        {
        }

        public DragShadowBuilder(View view, Context context) : base(view)
        {
            Bitmap bitmap = Bitmap.CreateBitmap(view.MeasuredWidth,
                    view.MeasuredHeight, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            view.Draw(canvas);

            _shadow = new BitmapDrawable(context.Resources, bitmap);
            _shadow.SetColorFilter(Color.LightGray, PorterDuff.Mode.Multiply);
        }

        #endregion Constructors

        #region Overrides

        public override void OnProvideShadowMetrics(Point size, Point touch)
        {
            int width = View.Width;
            int height = View.Height;

            _shadow.SetBounds(0, 0, width, height);
            size.Set(width, height);
            touch.Set(width / 2, height / 2);
        }

        public override void OnDrawShadow(Canvas canvas)
        {
            base.OnDrawShadow(canvas);
            _shadow.Draw(canvas);
        }

        #endregion Overrides
    }
}
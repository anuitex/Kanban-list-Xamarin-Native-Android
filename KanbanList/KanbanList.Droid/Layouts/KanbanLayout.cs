using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using KanbanList.Core.Models;
using KanbanList.Droid.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KanbanList.Droid.Layouts
{
    public class KanbanLayout : LinearLayout, AdapterView.IOnItemClickListener, AdapterView.IOnItemLongClickListener, View.IOnDragListener, IDisposable
    {
        #region Variables

        private readonly int _defaultCountColumnsLandscape = 3;
        private readonly int _defaultCountColumnsPortrait = 2;
        private readonly int _defaultCountColumns = 3;
        private int _defaulWidthColumns;
        private int _defaultDividerWidth = 1;

        private DateTime _previusDateTime;
        
        private Orientation _orientation;

        private Color _defaultTabLayoutBackgoundColor;
        private Color _defaultTitleTextColor;
        private Color _defaultDividerColor;

        private IList<View> _dividers = new List<View>();
        private IList<ListView> _tasksListView = new List<ListView>();
        private IList<TextView> _titles = new List<TextView>();
        private IList<LinearLayout> _tanbanSections = new List<LinearLayout>();

        #endregion Variables

        #region Actions

        private readonly Action<string, string> _actionErrorMoved;
        private readonly Action<TaskModelResult> _actionUpdate;

        public event EventHandler<TaskModelResult> ItemSelected;

        #endregion Actions

        #region Constructors

        public KanbanLayout(Context context) : base(context)
        {
            _defaultTabLayoutBackgoundColor = new Color(ContextCompat.GetColor(Context, Resource.Color.colorPrimary));
            _defaultTitleTextColor = Color.LightGray;
            _defaultDividerColor = Color.Black;
        }

        public KanbanLayout(Context context, Action<string, string> actionErrorMoved, Action<TaskModelResult> actionUpdate) : this(context)
        {
            _actionErrorMoved = actionErrorMoved;
            _actionUpdate = actionUpdate;
        }

        #endregion Constructors

        #region Properties

        private Android.Content.Res.Orientation CurrentOrientationScreen => Context.Resources.Configuration.Orientation;
        private int PixelWidth => Resources.DisplayMetrics.WidthPixels;


        public Color TabLayoutBackgoundColor
        {
            get => _defaultTabLayoutBackgoundColor;
            set
            {
                foreach (var item in _titles)
                {
                    item.SetBackgroundColor(value);
                }
            }
        }

        public Color TitleTextColor
        {
            get => _defaultTitleTextColor;
            set
            {
                foreach (var item in _titles)
                {
                    item.SetTextColor(value);
                }
            }
        }

        public int DividerWidth
        {
            get => _defaultDividerWidth;
            set
            {
                foreach (var item in _dividers)
                {
                    item.LayoutParameters.Width = value;
                }
            }
        }

        public Color DividerColor
        {
            get => _defaultDividerColor;
            set
            {
                foreach (var item in _dividers)
                {
                    item.SetBackgroundColor(value);
                }
            }
        }

        #endregion Properties

        #region Methods

        public void SetListValue(IList<SectionModelResult> allSectionItems)
        {
            SetColumns(allSectionItems.Count);

            for (int i = 0; i < allSectionItems.Count; i++)
            {
                var adapter = new ListViewTaskAdapter(Context, _tasksListView[i], Resource.Layout.task_item_cell, _actionUpdate);

                adapter.ItemsSource = allSectionItems[i].TaskItems;
                adapter.CanMoveTaskToList = allSectionItems[i].CanMoveTaskToList;
                adapter.CurrentTaskStatus = allSectionItems[i].CurrentSectionStatus;
                adapter.Title = allSectionItems[i].Title;

                _tasksListView[i].Adapter = adapter;

                _titles[i].Text = allSectionItems[i].Title;
            }
        }

        private void KanbanLayout_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                return;
            }

            TaskModelResult model = (sender as IList<TaskModelResult>).FirstOrDefault();

            if (model != null)
            {
                (_tasksListView?.FirstOrDefault()?.Adapter as ListViewTaskAdapter).ItemsSource.Add(model);
            }
        }

        private View GetNewColumn(int index, int columnsCount)
        {

            var item = new LinearLayout(Context);
            item.Orientation = Orientation.Vertical;

            item.SetMinimumWidth(PixelWidth / _defaultCountColumns);

            var titleView = new TextView(Context);
            titleView.TextSize = 15f;
            titleView.Gravity = GravityFlags.Center;
            titleView.SetTextColor(TitleTextColor);
            titleView.SetBackgroundColor(TabLayoutBackgoundColor);
            titleView.SetMinimumHeight(250);

            var tasksListView = new ListView(Context);
            tasksListView.Id = index;
            tasksListView.VerticalScrollBarEnabled = false;
            tasksListView.SetBackgroundColor(Color.White);
            tasksListView.DividerHeight = 0;
            tasksListView.Divider = null;

            var titleLayoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 0, 1);
            var tasksLayoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 0, 9);

            item.AddView(titleView, titleLayoutParams);
            item.AddView(tasksListView, tasksLayoutParams);

            _titles.Add(titleView);
            _tasksListView.Add(tasksListView);
            _tanbanSections.Add(item);

            tasksListView.OnItemClickListener = this;
            tasksListView.OnItemLongClickListener = this;
            tasksListView.SetOnDragListener(this);

            return item;
        }

        private void SetColumns(int colums)
        {
            if (colums < 1)
            {
                return;
            }

            _defaulWidthColumns = (PixelWidth - colums) /
                    (CurrentOrientationScreen == Android.Content.Res.Orientation.Portrait ? _defaultCountColumnsPortrait : _defaultCountColumnsLandscape);

            for (int i = 0; i < colums; i++)
            {

                var itemLayoutParams = new LinearLayout.LayoutParams(_defaulWidthColumns, LinearLayout.LayoutParams.MatchParent, 1);

                if (i > 0)
                {
                    var divider = new View(Context);
                    var dividerlayoutParams = new LinearLayout.LayoutParams(DividerWidth, LinearLayout.LayoutParams.MatchParent, 0);
                    divider.SetBackgroundColor(DividerColor);
                    AddView(divider, dividerlayoutParams);
                    _dividers.Add(divider);
                }

                View view = GetNewColumn(i, colums);

                AddView(view, itemLayoutParams);
            }
        }

        #endregion Methods

        #region Overrides

        public override Orientation Orientation { get => Orientation.Horizontal; set => _orientation = value; }


        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            if (Math.Abs(DateTime.Now.Second - _previusDateTime.Second) < 2 && Math.Abs(DateTime.Now.Millisecond - _previusDateTime.Millisecond) < 200)
            {
                TaskModelResult task = ((parent as ListView).Adapter as ListViewTaskAdapter)[position];
                ItemSelected?.Invoke(this, task);
            }
            else
            {
                _previusDateTime = DateTime.Now;
            }
        }

        public bool OnItemLongClick(AdapterView parent, View view, int position, long id)
        {
            ClipData data = ClipData.NewPlainText((parent as ListView).Id.ToString(), position.ToString());

            var my_shadown_screen = new Helpers.DragShadowBuilder(view, Context);

            view.StartDragAndDrop(data, my_shadown_screen, null, 0);

            return false;
        }

        public bool OnDrag(View view, DragEvent e)
        {
            if (e.Action != DragAction.Drop)
            {
                return true;
            }

            int sourcePositionItemInAdapter = int.Parse(e.ClipData.GetItemAt(0).Text);

            int sourceListViewId = int.Parse(e.ClipDescription.Label);
            int destinationListViewId = (view as ListView).Id;

            if (sourceListViewId == destinationListViewId)
            {
                return true;
            }

            var sourceListViewAdapter = (_tasksListView.FirstOrDefault(x => x.Id == sourceListViewId).Adapter as ListViewTaskAdapter);

            var destinationListViewAdapter = (_tasksListView.FirstOrDefault(x => x.Id == destinationListViewId).Adapter as ListViewTaskAdapter);

            if (sourceListViewAdapter.IsCanMoveItemToNextList(destinationListViewAdapter))
            {
                TaskModelResult holdOnModel = sourceListViewAdapter[sourcePositionItemInAdapter];

                sourceListViewAdapter.Remove(sourcePositionItemInAdapter);

                destinationListViewAdapter.Add(holdOnModel);

                return true;
            }

            _actionErrorMoved?.Invoke(sourceListViewAdapter.Title, destinationListViewAdapter.Title);

            return true;
        }

        #endregion Overrides
    }
}
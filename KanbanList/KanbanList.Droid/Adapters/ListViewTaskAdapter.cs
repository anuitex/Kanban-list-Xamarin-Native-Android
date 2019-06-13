using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using KanbanList.Core.Enums;
using KanbanList.Core.Models;

namespace KanbanList.Droid.Adapters
{
    public class ListViewTaskAdapter : BaseAdapter<TaskModelResult>
    {
        #region Variables
        
        private Context _activity;
        private int _itemTemplateId;
        private readonly Action<TaskModelResult> _actionUpdate;
        private ListView _listView;
        private ObservableCollection<TaskModelResult> _list = new ObservableCollection<TaskModelResult>();
        private Dictionary<TaskStatus, Color> _itemColors = new Dictionary<TaskStatus, Color>();

        #endregion Variables

        #region Constructors

        public ListViewTaskAdapter(Context activity, ListView listView, int itemTemplateId, Action<TaskModelResult> actionUpdate)
        {
            _activity = activity;
            _listView = listView;
            _itemTemplateId = itemTemplateId;
            _actionUpdate = actionUpdate;

            #region Init Dictionary

            _itemColors.Add(TaskStatus.None, new Color(105, 105, 105));
            _itemColors.Add(TaskStatus.Open, new Color(183, 225, 205));
            _itemColors.Add(TaskStatus.InProgress, new Color(109, 158, 235));
            _itemColors.Add(TaskStatus.Resolved, new Color(142, 124, 195));
            _itemColors.Add(TaskStatus.Verified, new Color(194, 123, 160));
            _itemColors.Add(TaskStatus.Completed, new Color(106, 168, 79));

            #endregion Init Dictionary
        }


        #endregion Constructors
        
        #region Properties

        public string Title { get; set; }

        public IList<TaskStatus> CanMoveTaskToList { get; set; }

        public TaskStatus CurrentTaskStatus { get; set; }

        public ObservableCollection<TaskModelResult> ItemsSource
        {
            get => _list;
            set
            {
                if (value != null && value != _list)
                {
                    _list = value;
                    value.CollectionChanged += ListViewTaskAdapter_CollectionChanged;
                    NotifyDataSetChanged();
                }
            }
        }

        #endregion Properties
        
        #region Methods

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TaskModelResult currentItem = ItemsSource[position];
            View cell = convertView;

            if (cell == null)
            {
                cell = LayoutInflater.From(_activity).Inflate(_itemTemplateId, null);
            }

            UpdateData(cell, currentItem);

            return cell;
        }

        private void UpdateData(View cell, TaskModelResult taskItem)
        {
            cell.FindViewById<TextView>(Resource.Id.lblTitleValue).Text = taskItem.Title;
            cell.FindViewById<TextView>(Resource.Id.lblCreatedDateOrEditDateValue).Text = taskItem.CreatedDate.ToShortTimeString();
            cell.FindViewById<CardView>(Resource.Id.MainCardView).SetCardBackgroundColor(_itemColors[taskItem.TaskStatus]);
        }

        public bool IsCanMoveItemToNextList(ListViewTaskAdapter destinationTaskAdapter)
        {
            return CanMoveTaskToList.Contains(destinationTaskAdapter.CurrentTaskStatus);
        }

        public void Add(TaskModelResult task)
        {
            task.TaskStatus = CurrentTaskStatus;
            ItemsSource.Add(task);
            _actionUpdate?.Invoke(task);
        }

        public void Remove(int index)
        {
            ItemsSource.RemoveAt(index);
        }

        #endregion Methods
        
        #region Overrides

        public override TaskModelResult this[int position] => ItemsSource[position];

        public override int Count => ItemsSource.Count;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                ItemsSource.CollectionChanged -= ListViewTaskAdapter_CollectionChanged;
            }
        }

        #endregion Overrides
        
        #region EventHandlers

        private void ListViewTaskAdapter_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                _listView.SmoothScrollToPosition(ItemsSource.Count - 1);
            }
        }

        #endregion EventHandlers
    }
}
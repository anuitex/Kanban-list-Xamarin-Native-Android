using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using KanbanList.Core.Services.Interfaces;

namespace KanbanList.Droid.Services.Implementations
{
    public class PickerDialogService : Java.Lang.Object, IPickerDialogService
    {

        #region Variables

        private static Context Context;

        protected string[] ArrayOptionTitle;
        protected Dictionary<string, Action> ChoosePiickerOptions;
        protected AlertDialog.Builder DialogBuilder;
        protected AlertDialog AlertDialog;

        #endregion Variables

        #region Constructors

        public static void Init(Context context)
        {
            Context = context;
        }

        #endregion Constructors

        #region Methods

        public void ShowChoosePicker(Dictionary<string, Action> choosePiickerOptions, string title = "Choose picker", string textOk = "Ok", string textCancel = "Cancel")
        {
            ChoosePiickerOptions = choosePiickerOptions;
            int layout = Resource.Layout.dialog_create_new_task;
            DialogBuilder = new AlertDialog.Builder(Context);
            View layoutView = LayoutInflater.From(Context).Inflate(layout, null);
            Button dialogButton = layoutView.FindViewById<Button>(Resource.Id.btnCnacel);
            ListView listView = layoutView.FindViewById<ListView>(Resource.Id.items_list);

            ArrayOptionTitle = ChoosePiickerOptions.Keys.ToArray();
            var listAdapter = new ArrayAdapter(Context, Android.Resource.Layout.SimpleListItem1, ArrayOptionTitle);
            listView.Adapter = listAdapter;

            DialogBuilder.SetView(layoutView);
            AlertDialog = DialogBuilder.Create();
            AlertDialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            AlertDialog.Show();

            ChoosePickerEventHandler eventHandler = new ChoosePickerEventHandler(this);

            listView.OnItemClickListener = eventHandler;
            dialogButton.SetOnClickListener(eventHandler);
            AlertDialog.SetOnCancelListener(eventHandler);

        }

        #endregion Methods

        public class ChoosePickerEventHandler : Java.Lang.Object, IDialogInterfaceOnCancelListener, View.IOnClickListener, AdapterView.IOnItemClickListener
        {
            private readonly PickerDialogService _pickerDialogService;

            public ChoosePickerEventHandler(PickerDialogService pickerDialogService)
            {
                _pickerDialogService = pickerDialogService;
            }

            public void OnCancel(IDialogInterface dialog)
            {
                _pickerDialogService.AlertDialog.Hide();
            }

            public void OnClick(View v)
            {
                _pickerDialogService.AlertDialog.Hide();
            }

            public void OnItemClick(AdapterView parent, View view, int position, long id)
            {
                _pickerDialogService.AlertDialog.Hide();
                _pickerDialogService.ChoosePiickerOptions.GetValueOrDefault(_pickerDialogService.ArrayOptionTitle[position])();
            }
        }
    }
}
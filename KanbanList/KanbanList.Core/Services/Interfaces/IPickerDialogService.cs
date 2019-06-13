using System;
using System.Collections.Generic;

namespace KanbanList.Core.Services.Interfaces
{
    public interface IPickerDialogService
    {
        void ShowChoosePicker(Dictionary<string, Action> choosePiickerOptions, string title = "Choose picker", string textOk = "Ok", string textCancel = "Cancel");
    }
}

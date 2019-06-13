using KanbanList.Core.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KanbanList.Core.Models
{
    public class SectionModelResult
    {
        public string Title { get; set; } 

        public TaskStatus CurrentSectionStatus { get; set; }

        private ObservableCollection<TaskModelResult> _taskItems = new ObservableCollection<TaskModelResult>();
        public ObservableCollection<TaskModelResult> TaskItems
        {
            get => _taskItems;
            set
            {
                _taskItems = value;
            }
        }

        private IList<TaskStatus> _canMoveTaskToList = new List<TaskStatus>();
        public IList<TaskStatus> CanMoveTaskToList
        {
            get => _canMoveTaskToList;
            private set
            {
                _canMoveTaskToList = value;
            }
        }
    }
}

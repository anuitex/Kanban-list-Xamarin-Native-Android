using KanbanList.Core.Entities.Base;
using KanbanList.Core.Enums;
using SQLite;
using System;

namespace KanbanList.Core.Entities
{
    [Table("Tasks")]
    public class TaskModelEntity : EntityBase
    {
        public string CreatorUserId { get; set; }

        public string AssignedUserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public double Estimate { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public DateTime ViewedDate { get; set; }
    }
}

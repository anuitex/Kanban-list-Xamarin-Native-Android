using KanbanList.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace KanbanList.Core.Models
{

    public class TaskModelResult
    {
        public string Id { get; set; }

        public string CreatorUserId { get; set; }

        public string AssignedUserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Estimate { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public DateTime ViewedDate { get; set; }
    }
}

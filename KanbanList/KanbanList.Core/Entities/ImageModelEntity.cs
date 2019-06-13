using KanbanList.Core.Entities.Base;
using SQLite;

namespace KanbanList.Core.Entities
{
    [Table("ImageFiles")]
    public class ImageModelEntity : EntityBase
    {
        public string TaskId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }
    }
}

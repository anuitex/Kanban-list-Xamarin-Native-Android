using SQLite;

namespace KanbanList.Core.Entities.Base
{
    public abstract class EntityBase
    {
        [PrimaryKey]
        public string Id { get; set; }
    }
}

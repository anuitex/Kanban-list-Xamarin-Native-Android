namespace KanbanList.Core.Models
{
    public class DestructionResult<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool Destroyed { get; set; }
    }
}

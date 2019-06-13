using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanList.Core.Entities;
using KanbanList.Core.Repositories.Base;
using KanbanList.Core.Repositories.Interfaces;
using KanbanList.Core.Services.Interfaces;

namespace KanbanList.Core.Repositories.Implementations
{
    public class SQLiteTaskRepository : BaseRepository<TaskModelEntity>, ITaskRepository<TaskModelEntity>
    {
        public SQLiteTaskRepository(IFileService fileService) : base(fileService)
        {
            Database.CreateTableAsync<TaskModelEntity>();
        }

        public Task<TaskModelEntity> GetItem(TaskModelEntity item)
        {
            return Database?.Table<TaskModelEntity>().FirstOrDefaultAsync(x => x.Id == item.Id);
        }

        public Task<List<TaskModelEntity>> GetAllTasksList()
        {
            return Database?.Table<TaskModelEntity>().ToListAsync();
        }

        public Task<List<TaskModelEntity>> GetAllTasksList(string userId)
        {
            return Database?.Table<TaskModelEntity>().Where(x => x.AssignedUserId == userId || x.CreatorUserId == userId).ToListAsync();
        }
    }
}

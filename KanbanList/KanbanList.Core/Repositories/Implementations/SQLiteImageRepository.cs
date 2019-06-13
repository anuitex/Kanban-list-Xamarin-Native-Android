using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanList.Core.Entities;
using KanbanList.Core.Repositories.Base;
using KanbanList.Core.Repositories.Interfaces;
using KanbanList.Core.Services.Interfaces;

namespace KanbanList.Core.Repositories.Implementations
{
    public class SQLiteImageRepository : BaseRepository<ImageModelEntity>, IImageRepository<ImageModelEntity>
    {
        public SQLiteImageRepository(IFileService fileService) : base(fileService)
        {
            Database.CreateTableAsync<ImageModelEntity>().Wait();
        }

        public Task DeleteAllImages(string taskId)
        {
            return Database?.Table<ImageModelEntity>().Where(x => x.TaskId == taskId).DeleteAsync();
        }

        public Task<List<ImageModelEntity>> GetAllImagesList(string taskId)
        {
            return Database?.Table<ImageModelEntity>().Where(x => x.TaskId == taskId).ToListAsync();
        }

        public Task<List<ImageModelEntity>> GetAllImagesList()
        {
            return Database?.Table<ImageModelEntity>().ToListAsync();
        }
    }
}

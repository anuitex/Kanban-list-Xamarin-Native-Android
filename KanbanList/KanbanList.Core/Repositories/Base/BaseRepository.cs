using System;
using System.Threading.Tasks;
using KanbanList.Core.Configurations;
using KanbanList.Core.Services.Interfaces;
using SQLite;

namespace KanbanList.Core.Repositories.Base
{
    public abstract class BaseRepository<TModel> : IRepository<TModel>, IDisposable where TModel : class
    {
        protected SQLiteAsyncConnection Database { get; private set; }

        public BaseRepository()
        {

        }

        public BaseRepository(IFileService fileService)
        {
            string databasepath = fileService.GetLocalPath(Constants.DastabaseName);
            Database = new SQLiteAsyncConnection(databasepath);
        }

        public void Dispose()
        {
            Database?.CloseAsync();
        }

        public Task Create(TModel item)
        {
            return Database?.InsertAsync(item);
        }

        public Task Delete(TModel item)
        {
            return Database.DeleteAsync(item);
        }

        public Task Update(TModel item)
        {
            return Database?.UpdateAsync(item);
        }
    }
}

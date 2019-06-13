using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanList.Core.Entities;
using KanbanList.Core.Repositories.Base;
using KanbanList.Core.Repositories.Interfaces;
using KanbanList.Core.Services.Interfaces;

namespace KanbanList.Core.Repositories.Implementations
{
    public class SQLiteUserRepository : BaseRepository<UserModelEntity>, IUserRepository<UserModelEntity>
    {
        public SQLiteUserRepository(IFileService fileService) : base(fileService)
        {
            Database.CreateTableAsync<UserModelEntity>();
        }

        public Task<UserModelEntity> GetUser(string email)
        {
            return Database?.Table<UserModelEntity>().FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<UserModelEntity> GetUser(string email, string password)
        {
            return Database?.Table<UserModelEntity>().FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public Task<UserModelEntity> GetUserById(string userId)
        {
            return Database?.Table<UserModelEntity>().FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<List<UserModelEntity>> GetAllUsersList()
        {
            List<UserModelEntity> result = await Database?.Table<UserModelEntity>().ToListAsync();
            return result.Select(x => new UserModelEntity() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }).OrderBy(y => y.FirstName).ToList();
        }
    }
}

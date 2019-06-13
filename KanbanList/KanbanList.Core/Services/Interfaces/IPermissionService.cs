using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanList.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> CheckPermissions(IList<Permission> permissions);
    }
}

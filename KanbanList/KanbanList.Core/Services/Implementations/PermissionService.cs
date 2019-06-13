using KanbanList.Core.Services.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanList.Core.Services.Implementations
{
    public class PermissionService : IPermissionService
    {
        public async Task<bool> CheckPermissions(IList<Permission> permissions)
        {
            foreach (Permission permission in permissions)
            {
                PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

                if (permissionStatus != PermissionStatus.Granted)
                {
                    Dictionary<Permission, PermissionStatus> resultPermissionStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);

                    permissionStatus = resultPermissionStatus[permission];
                }

                if (permissionStatus == PermissionStatus.Granted)
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}

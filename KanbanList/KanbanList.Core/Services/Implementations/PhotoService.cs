using Acr.UserDialogs;
using KanbanList.Core.Helpers;
using KanbanList.Core.Models;
using KanbanList.Core.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanList.Core.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly IPermissionService _permissionService;

        public PhotoService(IPermissionService permissionService, IUserDialogs userDialogs)
        {
            _permissionService = permissionService;
        }

        public async Task<ImageFileModelResult> PickPhoto()
        {
            bool hasPermission = await _permissionService.CheckPermissions(new List<Permission>() { Permission.Camera, Permission.Storage });

            if (!hasPermission)
            {
                return null;
            }

            MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                CompressionQuality = 100,
                SaveMetaData = true,
                CustomPhotoSize = 100,
                PhotoSize = PhotoSize.MaxWidthHeight
            });

            if (file == null)
            {
                return null;
            }

            byte[] localArray = PhotoHelper.ConvertToByteArray(file.GetStream());

            return new ImageFileModelResult() { FilePath = file.Path, ImageArray = localArray };
        }

        public async Task<ImageFileModelResult> TakePhoto(bool saveToAlbum = true)
        {
            bool hasPermission = await _permissionService.CheckPermissions(new List<Permission>() { Permission.Camera, Permission.Storage });

            if (!hasPermission)
            {
                return null;
            }

            string fileName = $"{Guid.NewGuid().ToString()}.jpg";
            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Photos",
                AllowCropping = false,
                PhotoSize = PhotoSize.MaxWidthHeight,
                CompressionQuality = 100,
                SaveToAlbum = saveToAlbum,
                Name = fileName,
            });

            if (file == null)
            {
                return null;
            }

            byte[] localArray = PhotoHelper.ConvertToByteArray(file.GetStream());

            return new ImageFileModelResult() { FileName = fileName, FilePath = file.Path, ImageArray = localArray };
        }
    }
}

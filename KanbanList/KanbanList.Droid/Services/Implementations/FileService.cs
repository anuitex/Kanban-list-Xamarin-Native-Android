using KanbanList.Core.Services.Interfaces;
using System;
using System.IO;

namespace KanbanList.Droid.Services.Implementations
{
    public class FileService : IFileService
    {
        public string GetLocalPath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
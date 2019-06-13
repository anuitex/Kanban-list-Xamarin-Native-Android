using System.IO;

namespace KanbanList.Core.Helpers
{
    public class PhotoHelper
    {
        public static byte[] ConvertToByteArray(Stream inputStream)
        {
            byte[] dataArray = null;

            using (Stream stream = inputStream)
            {
                stream.Position = 0;
                dataArray = new byte[stream.Length];
                stream.Read(dataArray, 0, (int)stream.Length);
            }
            return dataArray;
        }

        public static byte[] ConvertToByteArrayFromPath(string path)
        {
            byte[] dataArray = null;

            if (File.Exists(path))
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    stream.Position = 0;
                    dataArray = new byte[stream.Length];
                    stream.Read(dataArray, 0, (int)stream.Length);
                }
            }
            return dataArray;
        }
    }
}

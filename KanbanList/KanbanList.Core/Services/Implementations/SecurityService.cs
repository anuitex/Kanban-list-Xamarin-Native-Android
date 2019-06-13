using KanbanList.Core.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace KanbanList.Core.Services.Implementations
{
    public class SecurityService : ISecurityService
    {
        public string Encrypte(string source)
        {
            string hash = null;

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                hash = sBuilder.ToString();
            }

            return hash;
        }
    }
}

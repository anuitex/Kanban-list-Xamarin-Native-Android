using KanbanList.Core.Configurations;
using Plugin.SecureStorage;
using System;

namespace KanbanList.Core.Helpers
{
    public class SecureStorageHelper
    {

        public static bool Save<T>(string key, T value)
        {
            return CrossSecureStorage.Current.SetValue(key, value.ToString());
        }

        public static string GetString(string key, string defaultValue = null)
        {
            return CrossSecureStorage.Current.GetValue(key);
        }

        public static bool GetBoolean(string key, bool defaultValue = false)
        {
            if (CrossSecureStorage.Current.HasKey(key))
            {
                string value = CrossSecureStorage.Current.GetValue(key);
                return bool.Parse(value);
            }

            return defaultValue;
        }

        public static T Get<T>(string key, T defaultValue) where T : class
        {
            return CrossSecureStorage.Current.GetValue(key) as T;
        }


        public static bool SaveEnum<T>(string key, T value)
        {
            return CrossSecureStorage.Current.SetValue(key, value.ToString());
        }

        public static T GetEnum<T>(string key)
        {
            string value = CrossSecureStorage.Current.GetValue(key);
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static void ClearSecureStorage()
        {
            CrossSecureStorage.Current.DeleteKey(Constants.IsLogined);
            CrossSecureStorage.Current.DeleteKey(Constants.CurrentUserId);
            CrossSecureStorage.Current.DeleteKey(Constants.OranizationName);
        }
    }
}

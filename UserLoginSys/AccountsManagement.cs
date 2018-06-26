using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Hywire.Security;

namespace UserLoginSys
{
    static class AccountsManagement
    {
        public static ObservableCollection<User> LoadFromFile(string filePath)
        {
            try
            {
                byte[] decryptedBytes;
                DESHelper.DecryptFromFile(out decryptedBytes, filePath);
                using (MemoryStream stream = new MemoryStream(decryptedBytes))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    ObservableCollection<User> users = (ObservableCollection<User>)formatter.Deserialize(stream);
                    return users;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static bool SaveToFile(ObservableCollection<User> users, string filePath)
        {
            if (users == null)
            {
                return false;
            }
            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, users);
                    DESHelper.EncryptToFile(stream.ToArray(), filePath);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

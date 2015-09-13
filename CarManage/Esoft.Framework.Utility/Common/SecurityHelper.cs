using System;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.Text;
using System.IO;

namespace Esoft.Framework.Utility.Common
{
    public class SecurityHelper
    {
        const string KEY_64 = "afXds*dx";
        const string IV_64 = "zlxm&IvS";

        public SecurityHelper()
        {
            
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">需要加密的明文</param>
        /// <returns>返回密文</returns>
        public static string Encode(string data)
        {
            byte[] byKey = ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(data);
                        streamWriter.Flush();
                        cryptoStream.FlushFinalBlock();
                        streamWriter.Flush();
                        return Convert.ToBase64String(memoryStream.GetBuffer(), 0,
                            (int)memoryStream.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">需要解密的密文</param>
        /// <returns>明文</returns>
        public static string Decode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] encodeData = null;

            try
            {
                encodeData = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            using (MemoryStream memoryStream = new MemoryStream(encodeData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, 
                    cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// 设置指定目录访问权限
        /// </summary>
        /// <param name="directory">目录完全限定路径</param>
        /// <param name="userName">Windows账户名</param>
        /// <param name="rights">访问权限</param>
        public static void SetDirectorySecurity(string directory,
            string userName, FileSystemRights rights)
        {
            FileSystemRights fileRights = new FileSystemRights();

            fileRights = fileRights | rights;

            bool modified = false;
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            DirectorySecurity security = directoryInfo.GetAccessControl();
            InheritanceFlags flags = new InheritanceFlags();
            flags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

            FileSystemAccessRule accessRule = new FileSystemAccessRule(
                userName, fileRights, flags, PropagationFlags.None,
                AccessControlType.Allow);

            security.ModifyAccessRule(AccessControlModification.Add,
                accessRule, out modified);
            directoryInfo.SetAccessControl(security);
        }
    }
}

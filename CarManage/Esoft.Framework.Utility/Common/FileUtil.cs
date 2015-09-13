using System;
using System.IO;

namespace Esoft.Framework.Utility.Common
{
    public class FileUtil
    {
        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="srcDirectory">源目录</param>
        /// <param name="destDirectory">目标目录</param>
        /// <param name="deleteSrc">删除目录</param>
        public static void CopyDirectory(string srcDirectory, string destDirectory, bool deleteSrc)
        {
            if (srcDirectory.EndsWith("\\"))
                srcDirectory = srcDirectory.Substring(0, srcDirectory.Length - 1);

            if (destDirectory.EndsWith("\\"))
                destDirectory = destDirectory.Substring(0, destDirectory.Length - 1);

            DirectoryInfo srcDirecotoryInfo = new DirectoryInfo(srcDirectory);
            DirectoryInfo destDirectoryInfo = new DirectoryInfo(destDirectory);

            if (!srcDirecotoryInfo.Exists)
                return;

            if (!destDirectoryInfo.Exists)
                destDirectoryInfo.Create();

            foreach(FileInfo fileInfo in srcDirecotoryInfo.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(destDirectoryInfo.FullName ,fileInfo.Name), true);
            }

            foreach (DirectoryInfo directoryInfo in srcDirecotoryInfo.GetDirectories())
            {
                CopyDirectory(directoryInfo.FullName, 
                    destDirectory + directoryInfo.FullName.Replace(srcDirectory,string.Empty), 
                    false);
            }

            if (deleteSrc)
            {
                srcDirecotoryInfo.Delete(true);
                srcDirecotoryInfo.Create();
            }
        }

        /// <summary>
        /// 删除文件夹和文件夹下所有的文件及文件夹
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        /// <param name="keepDirectory">是否保留该文件夹;true:保留;false:不保留</param>
        public static void DeleteDirectory(string directory, bool keepDirectory)
        {
            if (Directory.Exists(directory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                directoryInfo.Delete();

                if (keepDirectory)
                {
                    directoryInfo.Create();
                }
            }
        }
    }
}

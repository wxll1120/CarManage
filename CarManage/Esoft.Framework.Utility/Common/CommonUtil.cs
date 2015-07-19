using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

namespace Esoft.Framework.Utility.Common
{
    public class CommonUtil
    {
        /// <summary>
        /// 过滤输入（移除首尾空格）
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>返回过滤后的字符串</returns>
        public static string FilterInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Trim();
        }
        /// <summary>
        /// 检查字符串，如果字符串为NULL，则返回空串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CheckText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input;
        }

        /// <summary>
        /// 获取文件拓展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileExtension(string fileName)
        {
            int index = fileName.LastIndexOf(".");

            if (index < 0)
                return string.Empty;

            return fileName.Substring(index);
        }

        /// <summary>
        /// 将二进制的文件数据以指定完全限定路径进行保存
        /// 如果目标文件已经存在，则覆盖原有文件
        /// </summary>
        /// <param name="filePath">文件完全限定路径</param>
        /// <param name="data">文件二进制数据</param>
        public static void SaveFile(string filePath, byte[] data)
        {
            string directory = filePath.Substring(0, filePath.LastIndexOf('\\'));

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (FileStream fileStream = new FileStream(filePath,
                FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(data, 0, data.Length); fileStream.Flush();
            }
        }


        public static void SaveFile(string filePath, string content)
        {
            string directory = filePath.Substring(0, filePath.LastIndexOf('\\'));

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (FileStream fileStream = new FileStream(filePath,
                FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(content);
                }
            }
        }

        public static byte[] GetFileData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            if (!File.Exists(filePath))
                return null;

            int readCount = 1024;
            int index = 0;
            byte[] data = null;
            FileStream stream = null;

            try
            {
                using (stream = new FileStream(filePath, FileMode.Open,FileAccess.Read))
                {
                    data = new byte[stream.Length];
                    
                    while (index < stream.Length)
                    {
                        if (stream.Length - index >= readCount)
                        {
                            stream.Read(data, index, readCount);
                            index += readCount;
                        }
                        else
                        {
                            stream.Read(data, index, (int)stream.Length - index);
                            break;
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                if (stream != null)
                    stream.Close();

                throw ex;
            }
        }

        /// <summary>
        /// 图片转为Byte字节数组
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns>字节数组</returns>
        public static byte[] GetImageBinaryData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            if (!File.Exists(filePath))
                return null;

            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Length.Equals(0))
                return null;

            using (MemoryStream stream = new MemoryStream())
            {
                using (Image image = Image.FromFile(filePath))
                {
                    using (Bitmap bitmap = new Bitmap(image))
                    {
                        bitmap.Save(stream, image.RawFormat);
                    }
                }

                return stream.ToArray();
            }
        }

        /// <summary>
        /// 读取文件并返回字符串形式的文件内容
        /// </summary>
        /// <param name="filePath">文件完全限定路径</param>
        /// <returns>返回字符串形式的文件内容</returns>
        public static string ReadFile(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                using (FileStream stream = new FileStream(filePath, 
                    FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = string.Empty;

                        do
                        {
                            line = reader.ReadLine();

                            if (line == null)
                                break;

                            sb.Append(line);
                        }
                        while (!reader.EndOfStream);
                    }
                }
            }
            catch
            { 
                
            }

            return sb.ToString();
        }

        /// <summary>
        /// 检查字符串是否是0、空串或NULL
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回一个布尔值，true为0、空串或NULL，false为否</returns>
        public static bool IsNullOrZero(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Equals("0"))
                return true;

            return false;
        }

        /// <summary>
        /// 获取可空日期的值
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>如果日期没有值则返回NULL，否则返回可空日期的值</returns>
        public static object GetNullableDateTime(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return null;

            return dateTime.Value;
        }

        /// <summary>
        /// 返回年月日格式的短日期（yyyy-MM-dd）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>返回yyyy-MM-dd格式的短日期</returns>
        public static string GetShortDateString(object dateTime)
        {
            if (dateTime == null)
                return string.Empty;

            DateTime convertResult = DateTime.MinValue;

            if (!DateTime.TryParse(dateTime.ToString(), out convertResult))
                return string.Empty;

            return convertResult.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 深度复制对象
        /// </summary>
        /// <typeparam name="T">源对象泛型</typeparam>
        /// <param name="input">源对象</param>
        /// <returns>返回复制后的新对象</returns>
        public static T DeepClone<T>(T input)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, input);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 判断JSON格式的内容是否为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool JSONNull(object input)
        {
            return input == null || string.IsNullOrEmpty(input.ToString());
        }

        private static Regex RegCHZN = new Regex("[\u4e00-\u9fff]");

        /// <summary> 
        /// 检查是否含有中文或中文标点
        /// </summary> 
        /// <param name="InputText">需要检查的字符串</param> 
        public static bool ContainsCNChar(string input)
        {
            //for (int i = 0; i < input.Length; i++)
            //{
            //    //汉字或中文标点
            //    if ((short)input[i] < 0 || (short)input[i] > 127)
            //    {
            //        return true;
            //    }
            //}

            //return false;

            return RegCHZN.IsMatch(input);
        }
    }
}

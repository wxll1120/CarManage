using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;

namespace Esoft.Framework.Utility.Common
{
    public class ConvertUtil
    {
        /// <summary>
        /// 将object类型数据转换成整型数据
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回整型</returns>
        public static int ToInt32(object input)
        {
            return int.Parse(input.ToString());
        }

        /// <summary>
        /// 将object类型数据转换成整型数据，如果转换失败则返回0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt32OrDefault(object input)
        {
            if (input == null)
                return 0;

            int result = 0;

            int.TryParse(input.ToString(), out result);

            return result;
        }

        /// <summary>
        /// 将object类型数据转换成decima数据，如果转换失败则返回0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal ToDecimalOrDefault(object input)
        {
            if (input == null)
                return 0;

            decimal result = 0;

            decimal.TryParse(input.ToString(), out result);

            return result;
        }

        /// <summary>
        /// 将object类型数据转换成可空的decimal类型。
        /// 如果输入为null或无效数据，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回可空的decimal类型数据</returns>
        public static decimal? ToNullableDecimal(object input)
        {
            if (input == null)
                return null;

            decimal convertResult = 0;
            if (!decimal.TryParse(input.ToString(), out convertResult))
                return null;

            return convertResult;
        }

        /// <summary>
        /// 将object类型数据转换成布尔值，如果输入数据为null，则返回false。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回布尔值类型数据</returns>
        public static bool ToBoolean(object input)
        {
            if (input == null)
                return false;

            string result = input.ToString().Trim();
            if (result.Equals(string.Empty))
                return false;
            else if (result.Equals("1"))
                return true;
            else if (result.Equals("0"))
                return false;
            else if (result.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (result.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                throw new InvalidCastException("未知输入数据类型！");
        }

        /// <summary>
        /// 将object类型数据转换成布尔值，如果输入数据为null，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回布尔值类型数据</returns>
        public static bool? ToNullableBoolean(object input)
        {
            if (input == null || string.IsNullOrEmpty(input.ToString()))
                return null;

            string result = input.ToString().Trim();
            if (result.Equals(string.Empty))
                return false;
            else if (result.Equals("1"))
                return true;
            else if (result.Equals("0"))
                return false;
            else if (result.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (result.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                throw new InvalidCastException("未知输入数据类型！");
        }

        /// <summary>
        /// 将object类型数据转换成布尔值，如果输入数据为null或者转换失败，则返回false。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回布尔值类型数据</returns>
        public static bool ToBooleanOrDefault(object input)
        {
            if (input == null)
                return false;

            string result = input.ToString().Trim();
            if (result.Equals(string.Empty))
                return false;
            else if (result.Equals("1"))
                return true;
            else if (result.Equals("0"))
                return false;
            else if (result.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (result.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                return false;
        }

        /// <summary>
        /// 将object类型数据转换成布尔值类型。
        /// 如果输入为null或无效格式数据，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回布尔值类型数据</returns>
        public static bool? GetNullableBoolean(object input)
        {
            if (input == null)
                return null;

            string result = input.ToString().Trim();

            if (result.Equals("1"))
                return true;
            else if (result.Equals("0"))
                return false;
            else if (result.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (result.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                throw new InvalidCastException("未知输入数据类型！");
        }

        /// <summary>
        /// 将object类型数据转换成布尔值类型。
        /// 如果输入为null或无效格式数据，则引发异常。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回布尔值类型数据</returns>
        public static bool GetBoolean(object input)
        {
            if (input == null)
                throw new InvalidCastException("未知输入数据类型！");

            string result = input.ToString().Trim();

            if (result.Equals("1"))
                return true;
            else if (result.Equals("0"))
                return false;
            else if (result.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (result.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                throw new InvalidCastException("未知输入数据类型！");
        }

        /// <summary>
        /// 将object类型数据转换成日期类型。
        /// 如果输入输入为null或无效格式日期，则返回DateTime.MinValue。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回DateTime类型数据</returns>
        public static DateTime ToDateTime(object input)
        {
            if (input == null)
                return DateTime.MinValue;

            DateTime convertResult = DateTime.MinValue;
            DateTime.TryParse(input.ToString(), out convertResult);

            return convertResult;
        }


        public static DateTime GetUnformatDateTime(string input)
        {
            string formatDate = string.Empty;

            if (input.Length > 4)
                formatDate += input.Substring(0, 4) + "-";

            if (input.Length > 6)
                formatDate += input.Substring(4, 2) + "-";
            if (input.Length > 8)
                formatDate += input.Substring(6, 2) + " ";
            if (input.Length > 10)
                formatDate += input.Substring(8, 2) + ":";
            if (input.Length > 12)
                formatDate += input.Substring(10, 2) + ":";
            if (input.Length > 14)
                formatDate += input.Substring(12, 2);

            return DateTime.Parse(formatDate);
        }

        /// <summary>
        /// 将object类型数据转换成日期类型。
        /// 如果输入为null或无效格式日期，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回DateTime类型数据</returns>
        public static DateTime? ToNullableDateTime(object input)
        {
            if (input == null)
                return null;

            DateTime convertResult = DateTime.MinValue;
            if (!DateTime.TryParse(input.ToString(), out convertResult))
                return null;

            return convertResult;
        }

        /// <summary>
        /// 获取可空日期的值
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>如果日期没有值则返回NULL，否则返回可空日期的值</returns>
        public static DateTime GetNullableDateTime(DateTime? dateTime)
        {
            //if (!dateTime.HasValue)
            //    return null;

            return dateTime.Value;
        }

        /// <summary>
        /// 获取可空日期的值
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>如果日期没有值则返回NULL，否则返回可空日期的值</returns>
        public static object GetNullableDate(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return DBNull.Value;

            return dateTime.Value;
        }

        /// <summary>
        /// 获取object类型的日期，如果日期为空则返回null。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static object GetDateTime(DateTime dateTime)
        {
            if (dateTime == null)
                return null;

            return dateTime;
        }

        /// <summary>
        /// 获取object类型的日期，如果日期为空则返回null。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static object GetDateTime(DateTime? dateTime)
        {
            if (dateTime == null || !dateTime.HasValue)
                return null;

            return dateTime.Value;
        }

        /// <summary>
        /// 将布尔值类型数据转换成int32类型数据。True返回1，False返回0。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回布尔值类型数据</returns>
        public static int GetInt32(bool input)
        {
            return input ? 1 : 0;
        }

        /// <summary>
        /// 将可空类型整数转换成int32类型数据。如果无值，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回object类型数据</returns>
        public static object GetInt32(int? input)
        {
            if (!input.HasValue)
                return null;

            return input.Value;
        }

        /// <summary>
        /// 将object类型数据转换成int32类型。
        /// 如果输入为null或无效数据，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回int32类型数据</returns>
        public static int? GetNullableInt32(object input)
        {
            if (input == null)
                return null;

            int convertResult = 0;
            if (!int.TryParse(input.ToString(), out convertResult))
                return null;

            return convertResult;
        }

        /// <summary>
        /// 获取字符串数据，如果为null则返回DBNull.Value
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>返回字符串数据</returns>
        public static object GetString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return DBNull.Value;

            return input;
        }

        /// <summary>
        /// 将object类型数据转换成可空的int32类型。
        /// 如果输入为null或无效数据，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回可空的int32类型数据</returns>
        public static int? ToNullableInt32(object input)
        {
            if (input == null)
                return null;

            int convertResult = 0;
            if (!int.TryParse(input.ToString(), out convertResult))
                return null;

            return convertResult;
        }


        /// <summary>
        /// 将object类型数据转换成decimal类型。
        /// 如果输入为null或无效数据，则返回null。
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns>返回decimal类型数据</returns>
        public static decimal? GetNullableDecimal(object input)
        {
            if (input == null)
                return null;

            decimal convertResult = 0;
            if (!decimal.TryParse(input.ToString(), out convertResult))
                return null;

            return convertResult;
        }

        /// <summary>
        /// 将Image对象转换成二进制数组
        /// </summary>
        /// <param name="image">Image对象</param>
        /// <returns>返回二进制数组</returns>
        public static byte[] GetBytes(Image image)
        {
            return GetBytes(image, image.RawFormat);
        }

        /// <summary>
        /// 将Image对象转换成二进制数组
        /// </summary>
        /// <param name="image">Image对象</param>
        /// <param name="format">图像文件格式</param>
        /// <returns>返回二进制数组</returns>
        public static byte[] GetBytes(Image image, ImageFormat format)
        {
            byte[] data = null;

            //创建一个bitmap类型的bmp变量来读取文件。
            Bitmap bitmap = new Bitmap(image);

            //新建第二个bitmap类型的bmp2变量，我这里是根据我的程序需要设置的。
            Bitmap newBitmap = new Bitmap(image.Width, image.Height,
                PixelFormat.Format16bppRgb555);

            //将第一个bmp拷贝到bmp2中
            Graphics graphics = Graphics.FromImage(newBitmap);
            graphics.DrawImage(bitmap, 0, 0);

            using (MemoryStream stream = new MemoryStream())
            {
                newBitmap.Save(stream, format);
                data = stream.ToArray();
            }

            graphics.Dispose();
            bitmap.Dispose();

            return data;
        }

        /// <summary>
        /// 将二进制数据转换成Image对象
        /// </summary>
        /// <param name="data">二进制数据</param>
        /// <returns>返回Image对象</returns>
        public static Image ToImage(byte[] data)
        {
            Image image = null;

            if (data == null)
                return null;

            using (MemoryStream stream = new MemoryStream(data))
            {
                image = Image.FromStream(stream);
            }

            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image ToImage(byte[] bytes, string fileName)
        {
            Image image = null;

            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(bytes);
                }
            }

            image = Image.FromFile(fileName, false);

            return image;
        }

        /// <summary>
        /// 将object类型的数据转换成指定类型枚举
        /// </summary>
        /// <typeparam name="T">指定类型枚举</typeparam>
        /// <param name="value">枚举值或名称</param>
        /// <returns>返回指定类型枚举</returns>
        public static T ToEnum<T>(object value)
        {
            T result = default(T);

            result = (T)System.Enum.Parse(typeof(T), value.ToString(), true);

            return result;
        }

        public static bool IsInt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            int convertResult = 0;

            return int.TryParse(input, out convertResult);
        }

        /// <summary>
        /// 除去保留两位小数的数字末尾的0
        /// </summary>
        /// <param name="decInputNum">输入保留两位小数的数字</param>
        /// <returns></returns>
        public static String RemoveLastTwo0(Decimal decInputNum)
        {
            return Regex.Replace(decInputNum.ToString("0.00").Replace(".00", ""), @"^(([1-9]\d*)|0)(\.[1-9])0$", "$1$3");
        }

        /// <summary>
        /// 除去保留两位小数的数字末尾的0
        /// </summary>
        /// <param name="dblInputNum">输入保留两位小数的数字</param>
        /// <returns></returns>
        public static String RemoveLastTwo0(Double dblInputNum)
        {
            return Regex.Replace(dblInputNum.ToString("0.00").Replace(".00", ""), @"^(([1-9]\d*)|0)(\.[1-9])0$", "$1$3");
        }

        /// <summary>
        /// 是否数字格式的模式
        /// </summary>
        public const string IsNumberPattern = @"^((-?[1-9]\d*)|0)$";

        /// <summary>
        /// 是否数字格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            if (null == value || string.Empty == value.Trim())
            {
                return false;
            }

            return Regex.IsMatch(value, IsNumberPattern);
        }

        /// <summary>
        /// 获取yyyy-MM-dd格式的短日期字符串
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>返回yyyy-MM-dd格式的短日期字符串</returns>
        public static string GetShortDateString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 向上舍入小数
        /// </summary>
        /// <param name="val">要舍弃的小数</param>
        /// <param name="decPoint">保留几位小数</param>
        /// <returns></returns>
        public static decimal RoundUp(decimal val, int decPoint)
        {
            bool flagMinus = false;
            if (val < 0)
            {
                val = -val;
                flagMinus = true;
            }

            decimal newVal = Math.Round(val, decPoint);
            decimal difference = val - newVal;
            if (difference > 0)
            {
                decimal padding = 1 / (decimal)(Math.Pow(10, decPoint));
                newVal += padding;
            }
            if (flagMinus)
                return newVal * -1;
            else
                return newVal;
        }

    }
}

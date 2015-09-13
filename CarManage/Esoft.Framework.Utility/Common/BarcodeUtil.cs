using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Design;
using System.IO;
using System.Collections;

namespace Esoft.Framework.Utility.Common
{
    /// <summary>
    /// 条形码公用类
    /// </summary>
    public static class BarCodeUtil
    {
        /// <summary>
        /// 生成条形码图片
        /// </summary>
        /// <param name="codeType">条形码类型</param>
        /// <param name="content">条形码内容</param>
        /// <param name="fontSize">显示字体大小</param>
        /// <param name="imgHeight">CODE图片高度</param>
        /// <param name="imgWidth">CODE图片宽度</param>
        /// <param name="withStart">是否在条形码中填加前后*号（条形码下方显示内容不变）</param>
        public static Image CreateBarcode(BarCodeType codeType, string content,
            float? fontSize, int imgHeight, int? imgWidth, bool withStart)
        {
            Image barCodeImage = null;
            switch (codeType)
            {
                case BarCodeType.Code39:
                    barCodeImage = GetCodeImage(content, fontSize, imgHeight,
                        imgWidth, Code39Model.Code39Normal, withStart);
                    break;
            }
            return barCodeImage;
        }

        #region 绘制code39码
        /// <summary>
        /// 根据条形码绘制图片
        /// </summary>
        /// <param name="strNumber">条形码</param>
        public static void DrawingBarcode39(string strNumber, float fontSize, bool withStart)
        {
            //ViewFont = new Font("宋体", fontSize);
            //Image img = GetCodeImage(strNumber, Code39Model.Code39Normal, withStart);
            //MemoryStream stream = new MemoryStream();
            //img.Save(stream, ImageFormat.Jpeg);

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.BufferOutput = true;
            //HttpContext.Current.Response.ContentType = "image/Jpeg";
            //HttpContext.Current.Response.BinaryWrite(stream.GetBuffer());
            //HttpContext.Current.Response.Flush();
        }
        /**
        CODE39码的编码规则是：
        1、 每五条线表示一个字符；
        2、 粗线表示1，细线表示0；
        3、 线条间的间隙宽的表示1，窄的表示0；
        4、 五条线加上它们之间的四条间隙就是九位二进制编码，而且这九位中必定有三位是1，所以称为39码；
        5、 条形码的首尾各一个＊标识开始和结束
        */
        private static Hashtable code39 = new Hashtable()
        {
            {"A", "1101010010110"},
            {"B", "1011010010110"},
            {"C", "1101101001010"},
            {"D", "1010110010110"},
            {"E", "1101011001010"},
            {"F", "1011011001010"},
            {"G", "1010100110110"},
            {"H", "1101010011010"},
            {"I", "1011010011010"},
            {"J", "1010110011010"},
            {"K", "1101010100110"},
            {"L", "1011010100110"},
            {"M", "1101101010010"},
            {"N", "1010110100110"},
            {"O", "1101011010010"},
            {"P", "1011011010010"},
            {"Q", "1010101100110"},
            {"R", "1101010110010"},
            {"S", "1011010110010"},
            {"T", "1010110110010"},
            {"U", "1100101010110"},
            {"V", "1001101010110"},
            {"W", "1100110101010"},
            {"X", "1001011010110"},
            {"Y", "1100101101010"},
            {"Z", "1001101101010"},
            {"0", "1010011011010"},
            {"1", "1101001010110"},
            {"2", "1011001010110"},
            {"3", "1101100101010"},
            {"4", "1010011010110"},
            {"5", "1101001101010"},
            {"6", "1011001101010"},
            {"7", "1010010110110"},
            {"8", "1101001011010"},
            {"9", "1011001011010"},
            {"+", "1001010010010"},
            {"-", "1001010110110"},
            {"*", "1001011011010"},
            {"/", "1001001010010"},
            {"%", "1010010010010"},
            {"$", "1001001001010"},
            {".", "1100101011010"},
            {" ", "1001101011010"} 
        };
        private static byte magnify = 0;
        /// <summary> 
        /// 放大倍数 
        /// </summary> 
        public static byte Magnify
        {
            get { return magnify; }
            set { magnify = value; }
        }
        private static int height = 80;
        /// <summary> 
        /// 图形高 
        /// </summary> 
        public static int Height
        {
            get { return height; }
            set { height = value; }
        }
        private static Font viewFont = new Font("宋体", 10);
        /// <summary> 
        /// 显示字体 
        /// </summary> 
        public static Font ViewFont
        {
            get { return viewFont; }
            set { viewFont = value; }
        }
        /// <summary>
        /// Code39编码类别
        /// </summary>
        public enum Code39Model
        {
            /// <summary> 
            /// 基本类别 1234567890ABC 
            /// </summary> 
            Code39Normal,
            /// <summary> 
            /// 全ASCII方式 +A+B 来表示小写 
            /// </summary> 
            Code39FullAscII
        }
        /// <summary> 
        /// 获得条码图形 
        /// </summary> 
        /// <param name="cotentText">Code文字信息</param> 
        /// <param name="fontSize">显示字体大小</param> 
        /// <param name="imgHeight">CODE图片高度</param> 
        /// <param name="imgWidth">CODE图片宽度</param> 
        /// <param name="model">Code39编码类别</param> 
        /// <param name="showStarChar">是否增加前后*号</param> 
        /// <returns>图形</returns> 
        public static Bitmap GetCodeImage(string cotentText, float? fontSize,
            int imgHeight, int? imgWidth, Code39Model model, bool showStarChar)
        {
            string valueText = "";
            string codeText = "";
            char[] valueChar = null;
            if (fontSize.HasValue)
            {
                ViewFont = new Font("宋体", (float)fontSize);
            }
            else
            {
                ViewFont = new Font("宋体", 10);
            }
            Height = imgHeight;

            switch (model)
            {
                case Code39Model.Code39Normal:
                    valueText = cotentText.ToUpper();
                    break;
                default:
                    valueChar = cotentText.ToCharArray();
                    for (int i = 0; i != valueChar.Length; i++)
                    {
                        if ((int)valueChar[i] >= 97 && (int)valueChar[i] <= 122)
                        {
                            valueText += "+" + valueChar[i].ToString().ToUpper();
                        }
                        else
                        {
                            valueText += valueChar[i].ToString();
                        }
                    }
                    break;
            }
            valueChar = valueText.ToCharArray();
            if (showStarChar == true) codeText += code39["*"];
            for (int i = 0; i != valueChar.Length; i++)
            {
                if (showStarChar == true && valueChar[i] == '*')
                    throw new Exception("带有起始符号不能出现*");
                object charCode = code39[valueChar[i].ToString()];
                if (charCode == null) throw new Exception("不可用的字符" + valueChar[i].ToString());
                codeText += charCode.ToString();
            }
            if (showStarChar == true) codeText += code39["*"];
            Bitmap codeBmp = null;
            if (imgWidth.HasValue)
            {
                codeBmp = new Bitmap((int)imgWidth, imgHeight);
                codeBmp = GetImage(codeText);
            }
            else
                codeBmp = GetImage(codeText);
            GetViewImage(codeBmp, cotentText);
            return codeBmp;
        }

        /// <summary> 
        /// 绘制编码图形 
        /// </summary> 
        /// <param name="cotentText">编码</param> 
        /// <returns>图形</returns> 
        private static Bitmap GetImage(string contentText)
        {
            char[] valueChar = contentText.ToCharArray();

            //宽 == 需要绘制的数量*放大倍数 + 两个字的宽    
            Bitmap codeImage = new Bitmap(valueChar.Length * ((int)magnify + 1), (int)height);
            Graphics graphics = Graphics.FromImage(codeImage);
            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, codeImage.Width, codeImage.Height));
            int lenEx = 0;
            for (int i = 0; i != valueChar.Length; i++)
            {
                int drawWidth = magnify + 1;
                if (valueChar[i] == '1')
                {
                    graphics.FillRectangle(Brushes.Black, new Rectangle(lenEx, 0, drawWidth, height));
                }
                else
                {
                    graphics.FillRectangle(Brushes.White, new Rectangle(lenEx, 0, drawWidth, height));
                }
                lenEx += drawWidth;
            }
            graphics.Dispose();
            return codeImage;
        }
        /// <summary> 
        /// 绘制文字,文字宽度大于图片宽度将不显示 
        /// </summary> 
        /// <param name="codeImage">图形</param> 
        /// <param name="cotentText">文字</param> 
        private static void GetViewImage(Bitmap codeImage, string contentText)
        {
            if (viewFont == null) return;
            Graphics graphics = Graphics.FromImage(codeImage);
            SizeF fontSize = graphics.MeasureString(contentText, viewFont);
            if (fontSize.Width > codeImage.Width || fontSize.Height > codeImage.Height - 20)
            {
                graphics.Dispose();
                return;
            }
            int starHeight = codeImage.Height - (int)fontSize.Height;
            graphics.FillRectangle(Brushes.White, new Rectangle(0, starHeight, codeImage.Width, (int)fontSize.Height));
            int starWidth = (codeImage.Width - (int)fontSize.Width) / 2;
            graphics.DrawString(contentText, viewFont, Brushes.Black, starWidth, starHeight);
            graphics.Dispose();
        }
        #endregion
    }

    /// <summary>
    /// 条形码类型
    /// </summary>
    public enum BarCodeType
    {
        Code39
    }
}

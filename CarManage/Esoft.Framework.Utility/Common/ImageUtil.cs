using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Text;

namespace Esoft.Framework.Utility.Common
{
    public class ImageUtil
    {
        ///<summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param>    
        public static byte[] CreateThumbnailImageData(Image originalImage)
        {
            if (null == originalImage)
                return null;

            int destWidth = originalImage.Width;
            int destHeight = originalImage.Height;
            byte[] thumbData = null;

            ///按比例得到图像大小
            if (destWidth > destHeight)
            {
                if (destWidth > 60)
                {
                    destHeight = destHeight * 60 / destWidth;
                    destWidth = 60;
                }
            }
            else
            {
                if (destHeight > 60)
                {
                    destWidth = destWidth * 60 / destHeight;
                    destHeight = 60;
                }
            }

            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(destWidth, destHeight);
            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //以jpg格式保存缩略图
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            try
            {
                //清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, destWidth, destHeight),
                    new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                    GraphicsUnit.Pixel);

                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbData = stream.ToArray();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
            }

            return thumbData;
        }

        ///<summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="image">图片二进制数据</param> 
        /// <param name="size">缩略图尺寸</param> 
        public static byte[] CreateThumbImage(byte[] image, Size size)
        {
            if (image == null)
                return null;

            byte[] thumbImage = null;

            Image originalImage = ConvertUtil.ToImage(image);

            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(size.Width, size.Height);
            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //以jpg格式保存缩略图
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            try
            {
                //清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, size.Width, size.Height),
                    new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                    GraphicsUnit.Pixel);

                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbImage = stream.ToArray();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
            }

            return thumbImage;
        }


        /// <summary>
        /// 获取按比例缩小的缩略图大小
        /// </summary>
        /// <param name="picture">原有图片</param>
        /// <returns>返回缩略图缩小尺寸</returns>
        public static Size GetThumbSize(byte[] picture)
        {
            Image image = ConvertUtil.ToImage(picture);
            Size size = new Size();
            double height = image.Height;//图片高度
            double width = image.Width;//图片宽度
            if (image.Width > 40)
            {
                size.Width = 40;
                if (height > width)
                    size.Height = (int)(height / width) * 40;
                else
                    size.Height = (int)(40 / (width / height));
            }
            else if (image.Height > 50)
            {
                size.Height = 50;
                if (height > width)
                    size.Width = (int)(50 / (height / width));
                else
                    size.Width = (int)(width / height) * 50;
            }
            else
            {
                size.Height = image.Height;
                size.Width = image.Width;
            }
            return size;
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="filePath">图像完整路径</param>
        /// <param name="data">图像二进制数据</param>
        public static void SaveImage(string filePath, byte[] data)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create,
                FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }

        /// <summary>
        /// 获取图片色彩模式
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageColorFormat GetColorFormat(Bitmap bitmap)
        {
            //索引模式
            const int pixelFormatIndexed = 0x00010000;
            //CMYK模式
            const int pixelFormat32bppCMYK = 0x200F;
            //灰度模式
            const int pixelFormat16bppGrayScale = (4 | (16 << 8));

            // Check image flags
            var flags = (ImageFlags)bitmap.Flags;
            if (flags.HasFlag(ImageFlags.ColorSpaceCmyk) || flags.HasFlag(ImageFlags.ColorSpaceYcck))
            {
                return ImageColorFormat.Cmyk;
            }
            else if (flags.HasFlag(ImageFlags.ColorSpaceGray))
            {
                return ImageColorFormat.Grayscale;
            }

            // Check pixel format
            var pixelFormat = (int)bitmap.PixelFormat;
            if (pixelFormat == pixelFormat32bppCMYK)
            {
                return ImageColorFormat.Cmyk;
            }
            else if ((pixelFormat & pixelFormatIndexed) != 0)
            {
                return ImageColorFormat.Indexed;
            }
            else if (pixelFormat == pixelFormat16bppGrayScale)
            {
                return ImageColorFormat.Grayscale;
            }

            // Default to RGB
            return ImageColorFormat.Rgb;
        }

        /// <summary>
        /// 获取图片像素属性
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetImageFlags(Image image)
        {
            ImageFlags imageFlags = (ImageFlags)System.Enum.Parse(typeof(ImageFlags), 
                image.Flags.ToString());

            return imageFlags.ToString();
        }

        public static string GetImageMd5(byte[] image)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retValOld = md5.ComputeHash(image);
            StringBuilder sbOld = new StringBuilder();
            for (int iold = 0; iold < retValOld.Length; iold++)
            {
                sbOld.Append(retValOld[iold].ToString("x2"));
            }

            return sbOld.ToString();
        }

    }

    public enum ImageColorFormat
    {
        Rgb,
        Cmyk,
        Indexed,
        Grayscale
    }
}

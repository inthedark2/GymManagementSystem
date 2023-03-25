using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace MS.Common.Services
{
    public static class WorkImage
    {
        public static string SaveImage(HttpPostedFileBase Image, string path, int height, int width)
        {
            string name = Guid.NewGuid().ToString()+ Path.GetExtension(Image.FileName);
            Bitmap imgs = WorkImage.CreateImage(Image, width, height);
            string filename = string.Empty;
            if (imgs != null)
            {  
                filename = path+name;
                imgs.Save(filename, ImageFormat.Jpeg);
            }
            return name;
        }

        private static Bitmap CreateImage(HttpPostedFileBase hpf, int maxWidth, int maxHeight)
        {
            if (hpf != null && hpf.ContentLength != 0 && hpf.ContentLength <= 10000000)
            {
                try
                {
                    using (Bitmap originalPic = new Bitmap(hpf.InputStream, true))
                    {
                        int width = originalPic.Width;
                        int height = originalPic.Height;
                        int widthDiff = (width - maxWidth);
                        int heightDiff = height - maxHeight;
                        bool doWidthResize = (maxWidth > 0 && width > maxWidth && widthDiff > -1 && widthDiff > heightDiff);
                        bool doHeightResize = (maxHeight > 0 && height > maxHeight && heightDiff > -1 && heightDiff > widthDiff);

                        if (doWidthResize || doHeightResize || (width.Equals(height) && widthDiff.Equals(heightDiff)))
                        {
                            int iStart;
                            Decimal divider;
                            if (doWidthResize)
                            {
                                iStart = width;
                                divider = Math.Abs((Decimal)iStart / maxWidth);
                                width = maxWidth;
                                height = (int)Math.Round((height / divider));
                            }
                            else
                            {
                                iStart = height;
                                divider = Math.Abs((Decimal)iStart / maxHeight);
                                height = maxHeight;
                                width = (int)Math.Round((width / divider));
                            }
                        }

                        using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                        {
                            using (Graphics oGraphics = Graphics.FromImage(outBmp))
                            {
                                oGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                oGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                oGraphics.DrawImage(originalPic, 0, 0, width, height);
                                return new Bitmap(outBmp);
                            }
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}

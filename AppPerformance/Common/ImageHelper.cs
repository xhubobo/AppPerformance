using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AppPerformance.Common
{
    internal class ImageHelper
    {
        /// <summary>
        /// 马赛克处理
        /// </summary>
        /// <param name="bitmap">位图</param>
        /// <param name="startX">起始坐标</param>
        /// <param name="effectWidth">影响范围 每一个格子数</param>
        /// <returns>处理后的位图</returns>
        public static Bitmap AdjustTobMosaic(Bitmap bitmap, int startX, int effectWidth)
        {
            if (startX >= bitmap.Width)
            {
                return bitmap;
            }
            else if (startX < 0)
            {
                startX = 0;
            }

            // 差异最多的就是以照一定范围取样 玩之后直接去下一个范围
            for (int heightOfffset = 0; heightOfffset < bitmap.Height; heightOfffset += effectWidth)
            {
                for (int widthOffset = startX; widthOffset < bitmap.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 计算范围平均
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // 所有范围内都设定此值
                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {

                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
            }
            return bitmap;
        }

        /// <summary>
        /// 获取位图中ARGB值最小像素的颜色值
        /// </summary>
        /// <param name="bmp">位图</param>
        /// <param name="bmpWidth">位图处理宽度</param>
        /// <param name="colorWidth">处理颜色的宽度</param>
        /// <returns>目标颜色</returns>
        public static Color GetMinColor(Bitmap bmp, int bmpWidth, int colorWidth)
        {
            Color color = new Color();
            Color minColor = new Color();
            int minColorValue = int.MaxValue;
            int colorValue = 0;

            for (int i = 0; i < colorWidth; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(bmpWidth - i - 1, j);
                    colorValue = color.ToArgb();
                    if (colorValue < minColorValue)
                    {
                        minColorValue = colorValue;
                        minColor = color;
                    }
                }
            }

            return minColor;
        }

        /// <summary>
        /// 获取位图中ARGB值最大像素的颜色值
        /// </summary>
        /// <param name="bmp">位图</param>
        /// <param name="bmpWidth">位图处理宽度</param>
        /// <param name="colorWidth">处理颜色的宽度</param>
        /// <returns>目标颜色</returns>
        public static Color GetMaxColor(Bitmap bmp, int bmpWidth, int colorWidth)
        {
            Color color = new Color();
            Color maxColor = new Color();
            int maxColorValue = int.MinValue;
            int colorValue = 0;

            for (int i = 0; i < colorWidth; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(bmpWidth - i - 1, j);
                    colorValue = color.ToArgb();
                    if (colorValue > maxColorValue)
                    {
                        maxColorValue = colorValue;
                        maxColor = color;
                    }
                }
            }

            return maxColor;
        }

        /// <summary>
        /// 获取位图中的平均颜色值
        /// </summary>
        /// <param name="bmp">位图</param>
        /// <param name="bmpWidth">位图处理宽度</param>
        /// <param name="colorWidth">处理颜色的宽度</param>
        /// <returns>目标颜色</returns>
        public static Color GetAvgColor(Bitmap bmp, int bmpWidth, int colorWidth)
        {
            Color color = new Color();
            int sumA = 0;
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;

            for (int i = 0; i < colorWidth; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(bmpWidth - i - 1, j);
                    sumA += color.A;
                    sumR += color.R;
                    sumG += color.G;
                    sumB += color.B;
                }
            }

            color = Color.FromArgb(
                (byte)(sumA / colorWidth / bmp.Height),
                (byte)(sumR / colorWidth / bmp.Height),
                (byte)(sumG / colorWidth / bmp.Height),
                (byte)(sumB / colorWidth / bmp.Height));
            return color;
        }
    }
}

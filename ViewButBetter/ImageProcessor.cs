using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace Visualisation_Applications
{
    public static class ImageProcessor
    {
        private static Dictionary<string, Bitmap> SpriteReference;


        public static void Clear()
        {
            SpriteReference.Clear();
        }
        public static void ClearEmpty()
        {
            if (SpriteReference != null)
            {
                if (SpriteReference.ContainsKey("empty"))
                {
                    SpriteReference.Remove("empty");
                }
            }

        }
        public static Bitmap GetBitmap(string key)
        {
            if (SpriteReference == null)
            {
                SpriteReference = new Dictionary<string, Bitmap>();
            }

            if (!SpriteReference.ContainsKey(key))
            {

                SpriteReference.Add(key, (Bitmap)Bitmap.FromFile(key));
            }

            return SpriteReference[key];
        }

        public static Bitmap GenerateBitmap(int width, int height)
        {
            if (SpriteReference == null)
            {
                SpriteReference = new();
            }
            if (!SpriteReference.ContainsKey("empty"))
            {
                Bitmap bmp = new(width, height);
                SpriteReference.Add("empty", bmp);
            }

            return SpriteReference["empty"].Clone() as Bitmap;
        }


        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

        }


        public static Bitmap ColorReplace(Bitmap bmp, Color oldColor, Color newColor)
        {   Bitmap returmImg = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < returmImg.Height; y++)
            {
                for (int x = 0; x < returmImg.Width; x++)
                {
                    if (bmp.GetPixel(x, y) == oldColor)
                    {
                        returmImg.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        returmImg.SetPixel(x, y, bmp.GetPixel(x, y));
                    }


                }
            }
            return returmImg;
        }
    }



}

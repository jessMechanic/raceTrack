using System;
using System.Collections.Generic;
using System.Drawing;

using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Visualisation_Applications
{
   public static class ImageProcessor
    {
        private static Dictionary<string, Bitmap> SpriteReference;


         public static void Clear()
        {
            SpriteReference.Clear();
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
            if(SpriteReference == null)
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
    }



}

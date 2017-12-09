using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace ImageEdgeDetection
{
    public class Wrapper
    {
        public Wrapper()
        {
            
        }
        
        public Bitmap BlackAndWhite(Bitmap image)
        {
            return ExtBitmap.BlackWhiteFilter(image);
        }

        public Bitmap KirschFilter(Bitmap sourceBitmap, bool grayscale = true)
        {
            return ExtBitmap.KirschFilter(sourceBitmap, grayscale);
        }

        public Bitmap PrewittFilter(Bitmap sourceBitmap, bool grayscale = true)
        {
            return ExtBitmap.PrewittFilter(sourceBitmap, grayscale);
        }

        public Bitmap Laplacian3x3Filter(Bitmap sourceBitmap, bool grayscale = true)
        {
            return ExtBitmap.Laplacian3x3Filter(sourceBitmap, grayscale);
        }

        public bool CompareTwoImages(Bitmap img1, Bitmap img2)
        {
            return ExtBitmap.CompareTwoImages(img1, img2);
        }
    }
}

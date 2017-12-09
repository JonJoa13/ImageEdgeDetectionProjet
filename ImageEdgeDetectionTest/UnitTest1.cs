using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEdgeDetection;
using System.Drawing;
using ImageEdgeDetectionTest.Properties;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class UnitTest1
    {
        Wrapper wrapper = new Wrapper();

        [TestMethod]
        public void TestBlackAndWhiteFilter()
        {
            Bitmap Original = Resources.image;
            Bitmap BlackAndWhite = Resources.imageBW;
            Bitmap Modified = wrapper.BlackAndWhite(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, BlackAndWhite));
            
            
        }

        [TestMethod]
        public void TestKirschFilter()
        {
            Bitmap Original = Resources.image;
            Bitmap Kirsch = Resources.imageKirsch;
            Bitmap Modified = wrapper.KirschFilter(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, Kirsch));
        }

        [TestMethod]
        public void TestPrewittFilter()
        {
            Bitmap Original = Resources.image;
            Bitmap Prewitt = Resources.imagePrewitt;
            Bitmap Modified = wrapper.PrewittFilter(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, Prewitt));
        }

        [TestMethod]
        public void TestLaplacian3x3Filter()
        {
            Bitmap Original = Resources.image;
            Bitmap Laplacian = Resources.imageLaplacian;
            Bitmap Modified = wrapper.Laplacian3x3Filter(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, Laplacian));
        }
    }
}

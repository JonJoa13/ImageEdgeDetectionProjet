/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;
        
        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;
        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Jpeg Images(*.jpg)|*.jpg|Png Images(*.png)|*.png";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;
                picOriginal.Image = previewBitmap;

                ApplyFilter(true);
            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyFilter(false);

            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Jpeg Images(*.jpg)|*.jpg|Png Images(*.png)|*.png";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        private void ApplyFilter(bool preview)
        {
            string filterName = cmbEdgeDetection.SelectedItem.ToString();

            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;

            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }

            if (selectedSource != null)
            {
                switch (filterName)
                {
                    case "None":
                        bitmapResult = selectedSource;
                        break;
                    case "Laplacian 3x3":
                        bitmapResult = selectedSource.Laplacian3x3Filter(false);
                        break;
                    case "Laplacian 3x3 Grayscale":
                        bitmapResult = selectedSource.Laplacian3x3Filter(true);
                        break;
                    case "Laplacian 5x5":
                        bitmapResult = selectedSource.Laplacian5x5Filter(false);
                        break;
                    case "Laplacian 5x5 Grayscale":
                        bitmapResult = selectedSource.Laplacian5x5Filter(true);
                        break;
                    case "Laplacian of Gaussian":
                        bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                        break;
                    case "Laplacian 3x3 of Gaussian 3x3":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                        break;
                    case "Laplacian 3x3 of Gaussian 5x5 - 1":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                        break;
                    case "Laplacian 3x3 of Gaussian 5x5 - 2":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                        break;
                    case "Laplacian 5x5 of Gaussian 3x3":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                        break;
                    case "Laplacian 5x5 of Gaussian 5x5 - 1":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                        break;
                    case "Laplacian 5x5 of Gaussian 5x5 - 2":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                        break;
                    case "Sobel 3x3":
                        bitmapResult = selectedSource.Sobel3x3Filter(false);
                        break;
                    case "Sobel 3x3 Grayscale":
                        bitmapResult = selectedSource.Sobel3x3Filter();
                        break;
                    case "Prewitt":
                        bitmapResult = selectedSource.PrewittFilter(false);
                        break;
                    case "Prewitt Grayscale":
                        bitmapResult = selectedSource.PrewittFilter();
                        break;
                    case "Kirsch":
                        bitmapResult = selectedSource.KirschFilter(false);
                        break;
                    case "Kirsch Grayscale":
                        bitmapResult = selectedSource.KirschFilter();
                        break;
                    case "BlackWhite":
                        bitmapResult = selectedSource.BlackWhiteFilter();
                        break;
                }
            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                }
            }
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }
    }
}

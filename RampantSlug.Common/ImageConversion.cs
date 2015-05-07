using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RampantSlug.Common
{
    /// <summary>
    /// Helper class to convert an image to and from a string
    /// </summary>
    public static class ImageConversion
    {
        private static string _imageNotFound =
          "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";

        /// <summary>
        /// Convert a bitmap image into a Base64 string
        /// </summary>
        /// <param name="filePath">File path to input image</param>
        /// <returns>Resulting string after conversion</returns>
        public static string ConvertImageFileToString(string filePath)
        {
            var inputImage = new Bitmap(filePath);
            var byteBlobData = ImageToByteArray(inputImage);
            var blobAsBase64 = Convert.ToBase64String(byteBlobData);
            return blobAsBase64;
        }

        /// <summary>
        /// Convert a Base64 string into a bitmap image
        /// </summary>
        /// <param name="inputBlobData">Desired image as a string</param>
        /// <returns>Resulting image after conversion</returns>
        public static BitmapImage ConvertStringToImage(string inputBlobData)
        {
            if (string.IsNullOrEmpty(inputBlobData))
            {
                inputBlobData = _imageNotFound;
            }

            var blobAsNewByteArray = Convert.FromBase64String(inputBlobData);
            var image = ByteArrayToImage(blobAsNewByteArray);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            var memoryStream = new MemoryStream();
            // Save to a memory stream...
            image.Save(memoryStream, image.RawFormat);
            // Rewind the stream...
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }


        private static byte[] ImageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        private static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}

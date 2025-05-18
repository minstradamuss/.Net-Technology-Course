using System;
using System.Drawing;
using System.IO;

namespace ChatBook.Services
{
    public class ImageService
    {
        public static Image LoadImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    return Image.FromStream(fs);
                }
            }
            return null;
        }
    }
}

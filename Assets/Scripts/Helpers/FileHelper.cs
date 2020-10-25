using System;
using System.IO;
using UnityEngine;

namespace FacialExpression.Helpers
{
    public static class FileHelper
    {
        private static string ImagePath
        {
            get
            {
#if UNITY_EDITOR
                return Path.Combine(Application.persistentDataPath, "captures");
#elif UNITY_ANDROID
                return $"file:///{Path.Combine(Application.persistentDataPath, "captures")}";           
#endif
            }
        }
        public static string SaveImageHelper(Texture2D texture2D)
        {
            if (!Directory.Exists(ImagePath))
                Directory.CreateDirectory(ImagePath);
            string imagePath = Path.Combine(ImagePath, $"image-{Guid.NewGuid()}.png");
            File.WriteAllBytes(imagePath, texture2D.EncodeToPNG());
            return imagePath;
        }

        public static Texture2D LoadImage(string path)
        {
            if (!File.Exists(path))
                return null;

            var fileData = File.ReadAllBytes(path);
            var image = new Texture2D(2, 2);
            image.LoadImage(fileData);
            return image;
        }

        public static byte[] LoadImageAsArray(string path)
        {
            if (!File.Exists(path))
                return null;

            return File.ReadAllBytes(path);
        }

        public static string[] GetAllImagesName()
        {
            return Directory.Exists(ImagePath) ? Directory.GetFiles(ImagePath, "*.png") : null;
        }

    }
}
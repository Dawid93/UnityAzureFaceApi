using System;
using System.IO;
using UnityEngine;

namespace FacialExpression.Helpers
{
    public static class FileHelper
    {
        private static readonly string ImagePath = $"{Application.persistentDataPath}/captures";
        
        public static string SaveImageHelper(Texture2D texture2D)
        {
            if (!Directory.Exists(ImagePath))
                Directory.CreateDirectory(ImagePath);
            string imagePath = $"/{ImagePath}/image-{ImagesInDirectoryCount()}.png";
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

        public static string[] GetAllImagesName()
        {
            return Directory.Exists(ImagePath) ? Directory.GetFiles(ImagePath) : null;
        }

        private static int ImagesInDirectoryCount()
        {
            return Directory.Exists(ImagePath) ? Directory.GetFiles(ImagePath).Length : 0;
        }
    }
}
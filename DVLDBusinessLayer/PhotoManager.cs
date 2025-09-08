using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class PhotoManager
    {
        private static readonly string targetFolder = @"C:\DVLD-People-Images";

        public static string SaveOrUpdatePersonPhoto(string originalPath, string oldPhotoPath)
        {
            // 1. Remove old photo if it exists
            if (!string.IsNullOrWhiteSpace(oldPhotoPath) && File.Exists(oldPhotoPath))
            {
                File.Delete(oldPhotoPath);
            }

            // 2. Generate new file name
            string extension = Path.GetExtension(originalPath);
            string newFileName = Guid.NewGuid().ToString() + extension;
            string newFullPath = Path.Combine(targetFolder, newFileName);

            // 3. Ensure folder exists
            Directory.CreateDirectory(targetFolder);

            // 4. Copy new photo
            File.Copy(originalPath, newFullPath, true);

            // 5. Return the new path
            return newFullPath;
        }

        public static void DeletePersonPhoto(string photoPath)
        {
            if (!string.IsNullOrWhiteSpace(photoPath) && File.Exists(photoPath))
            {
                File.Delete(photoPath);
            }
        }
    }


}

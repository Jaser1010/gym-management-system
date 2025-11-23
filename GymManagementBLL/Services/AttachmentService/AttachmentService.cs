using GymManagementBLL.Services.Attachmentservice;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.AttachmentService
{


    public class AttachmentService : IAttachmentService
    {
        public AttachmentService(IWebHostEnvironment webHost)
		{
			WebHost = webHost;
        }


        private readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };  
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB

        public IWebHostEnvironment WebHost { get; }

        public bool Delete(string fileName, string folderName)
        {
            try
			{
				if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) return false;
				var filePath = Path.Combine(WebHost.WebRootPath, "images", folderName, fileName);
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed To Delete File = {fileName} From Folder = {folderName} : {ex}");
				return false;
			}

		}

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
				if (file is null || folderName is null || file.Length == 0) return null; // No file provided
				if (file.Length > maxFileSize) return null; // File size exceeds limit
				var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
				if (!allowedExtensions.Contains(extension)) return null; // Invalid file type
				var FolderPath = Path.Combine(WebHost.WebRootPath, "images", folderName);
				if (!Directory.Exists(FolderPath))
				{
					Directory.CreateDirectory(FolderPath);
				}
				var FileName = Guid.NewGuid().ToString() + extension;
				var filePath = Path.Combine(FolderPath, FileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				return FileName;
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Failed To Upload File TO Folder = {folderName} : {ex}");
				return null;
			} 

		}
    }
}

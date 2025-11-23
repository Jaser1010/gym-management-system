using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Services.Attachmentservice
{
    public interface IAttachmentService
    {
        string? Upload(string folderName, IFormFile file); 

        bool Delete(string fileName, string folderName);
	}
}

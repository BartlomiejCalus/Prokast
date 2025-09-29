using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IBlobPhotoStorageService
    {
        byte[] DownloadPhotoAsync(string photoName);
        string UploadPhotoAsync(BLOBPhotoModel photo);
    }
}
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Services
{
    public class BlobPhotoStorageService// : IBlobPhotoStorageService
    {
        private readonly BlobServiceClient _blobServiceClient = new(Environment.GetEnvironmentVariable("StorageConnection"));

        /// <summary>
        /// Funkcja odpowiedzialna za upload podanego zdjęcia do Blob-a
        /// </summary>
        /// <param name="photoName"></param>
        /// <param name="containerName"></param>
        /// <param name="photoData"></param>
        /// <returns></returns>
        public async Task<string> UploadPhotoAsync(string photoName , byte[] photoData) {
            var container = _blobServiceClient.GetBlobContainerClient("images");
            await container.CreateIfNotExistsAsync();

            var blobClient = container.GetBlobClient(photoName);

            var upload = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders 
                {
                    ContentType = photoName.Contains(".jpg") ? "image/jpeg" : "image/png"
                }
            };

            await using var stream = new MemoryStream(photoData);
            stream.Position = 0;
            await blobClient.UploadAsync(stream, upload);
            
            return blobClient.Uri.ToString();
        }

        /// <summary>
        /// Funkcja odpowiedzialna za pobranie wybranego zdjęcia z Blob-a
        /// </summary>
        /// <param name="photoName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public async Task<byte[]> DownloadPhotoAsync(string photoName) 
        {
            var container = _blobServiceClient.GetBlobContainerClient("images");
            var blobClient = container.GetBlobClient(photoName);

            if(!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException($"Plik '{photoName}' nie istnieje!");
            }

            var downloadInfo = await blobClient.DownloadAsync();
            await using var stream = new MemoryStream();
            await downloadInfo.Value.Content.CopyToAsync(stream);

            return stream.ToArray();
        }
    }
}

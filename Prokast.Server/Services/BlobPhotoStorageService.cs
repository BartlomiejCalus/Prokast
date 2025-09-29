using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Text;
using Azure.Core;

namespace Prokast.Server.Services
{
    public class BlobPhotoStorageService : IBlobPhotoStorageService
    {
        private readonly AzureBlobStorageSettings _blobStorageSettings;
        public BlobPhotoStorageService(IOptions<AzureBlobStorageSettings> options)
        {
            _blobStorageSettings = options.Value;
        }


        //private readonly BlobServiceClient _blobServiceClient = new(Environment.GetEnvironmentVariable("StorageConnection"));


        /// <summary>
        /// Funkcja odpowiedzialna za upload podanego zdjęcia do Blob-a
        /// </summary>
        /// <param name="photoName"></param>
        /// <param name="containerName"></param>
        /// <param name="photoData"></param>
        /// <returns></returns>
        public string UploadPhotoAsync(BLOBPhotoModel photo)
        {
            var _blobServiceClient = new BlobServiceClient(_blobStorageSettings.StorageConnection);

            var container = _blobServiceClient.GetBlobContainerClient("images");
            container.CreateIfNotExists();




            var blobClient = container.GetBlobClient(photo.Name);

            //var ValueToCompare =photo.Value.ToString();
            //if (ValueToCompare.StartsWith("iVBORw")) { var xd = "image/png"; }


            var upload = new BlobUploadOptions
            {

                HttpHeaders = new BlobHttpHeaders
                {
                    // png == iVBORw  jpg == /9j/4
                    ContentType = photo.ContentType.Contains("png") ? "image/png" : "image/jpeg"
                    //if (photo.ContentType.Contains("png")) { BlobHttpHeaders.ContentType = "image/png"; }
                    // ContentType = photo.Name.Contains(".jpg") ? "image/jpeg" : "image/png"
                }
            };


            using var stream = new MemoryStream(photo.Value);
            stream.Position = 0;
            blobClient.Upload(stream, upload);

            return blobClient.Uri.ToString();
        }

        /// <summary>
        /// Funkcja odpowiedzialna za pobranie wybranego zdjęcia z Blob-a
        /// </summary>
        /// <param name="photoName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public byte[] DownloadPhotoAsync(string photoName)
        {
            var _blobServiceClient = new BlobServiceClient(_blobStorageSettings.StorageConnection);

            var container = _blobServiceClient.GetBlobContainerClient("images");
            var blobClient = container.GetBlobClient(photoName);

            if (!blobClient.Exists())
            {
                throw new FileNotFoundException($"Plik '{photoName}' nie istnieje!");
            }

            var downloadInfo = blobClient.Download();
            using var stream = new MemoryStream();
            downloadInfo.Value.Content.CopyTo(stream);

            return stream.ToArray();
        }
    }
}

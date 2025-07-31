
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Controllers
{


    [Route("api/photoStorage")]
    [Tags("PhotoStorage")]
    public class BlobPhotoStorageController : ControllerBase
    {
        private readonly IBlobPhotoStorageService _storageService;

        public BlobPhotoStorageController(IBlobPhotoStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Funkcja która przekazuje zdjęcie do zapisu w kontenerze na Azure BLOB
        /// </summary>
        /// <param name="photoName"></param>
        /// <param name="containerName"></param>
        /// <param name="photoData"></param>
        /// <returns></returns>
        [HttpPost]
        [EndpointSummary("Upload Photo")]
        [EndpointDescription("A POST operation. Endpoint uploads photo to a BLOB container.")]

        public ActionResult<string> UploadPhotoAsync(BLOBPhotoModel photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _storageService.UploadPhotoAsync( photo);
            return Ok();
        }

        /// <summary>
        /// Funkcja która pobiera dane zdjecia z kontenera Azure BLOB
        /// </summary>
        /// <param name="photoName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        [HttpGet]
        [EndpointSummary("Download Photo")]
        [EndpointDescription("A GET operation. Endpoint downloads photo from a BLOB container.")]

        public ActionResult<Task<string>> DownloadPhotoAsync(string photoName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _storageService.DownloadPhotoAsync(photoName);
            return Ok();
        }


    }
}
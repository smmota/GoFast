using Azure.Storage.Blobs;
using GoFast.API.Models;

namespace GoFast.API.Interfaces.Services
{
    public interface IBlobStorageService
    {
        Task<BlobClient> UploadBase64Image(string base64Image, string container);

        Task<bool> DeleteImage(BlobStorage blobStorage);
    }
}

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using GoFast.API.Interfaces.Services;
using GoFast.API.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text.RegularExpressions;

namespace GoFast.API.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IConfiguration _configuration;

        private string _conn = string.Empty;

        public BlobStorageService(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("connBlobStorage");
        }

        public async Task<BlobClient> UploadBase64Image(string base64Image, string container)
        {
            //Gera um nome randomico para imagem
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            //Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            //Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);

            //Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(_conn, container, fileName);

            //Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                await blobClient.UploadAsync(stream);
            }            

            //Retorna as informações da imagem
            return blobClient;
        }

        public async Task<bool> DeleteImage(BlobStorage blobStorage)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_conn);
            BlobContainerClient cont = blobServiceClient.GetBlobContainerClient(blobStorage.Container);
            return await cont.GetBlobClient(blobStorage.Name).DeleteIfExistsAsync();
        }
    }
}

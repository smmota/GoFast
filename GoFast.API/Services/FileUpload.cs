using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using GoFast.API.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text.RegularExpressions;

namespace GoFast.API.Services
{
    public class FileUpload
    {
        //private string conn = "DefaultEndpointsProtocol=https;AccountName=storagefase1;AccountKey=O468vEKmKkDelei7wSGbupOSSLeY5l0ztRaIArkNTcQ/2+0uznS7wUvZps16Bd/m8AQVZKMNSB0h+ASth+GoBg==;EndpointSuffix=core.windows.net";
        private readonly IConfiguration _configuration;

        private string _conn = string.Empty;

        public FileUpload(string conn)
        {
            _conn = conn;
        }

        public BlobClient UploadBase64Image(string base64Image, string container)
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
                blobClient.Upload(stream);
            }            

            //Retorna as informações da imagem
            return blobClient;
        }

        public bool DeleteImage(BlobStorage blobStorage)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_conn);
            BlobContainerClient cont = blobServiceClient.GetBlobContainerClient(blobStorage.Container);
            return cont.GetBlobClient(blobStorage.Name).DeleteIfExists();
        }
    }
}

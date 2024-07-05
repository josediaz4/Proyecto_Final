using Azure.Storage.Blobs;

namespace Proyecto_Final.Helpers
{
    public class HelpersBlob : IHelpersBlob
    {
        private readonly BlobServiceClient _blobClient;
        public HelpersBlob(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];
            _blobClient = new BlobServiceClient(keys);
        }
        public string DeleteBlob(string name, string containerName)
        {
            BlobContainerClient container = GetContainer(containerName);
            BlobClient blobClient = container.GetBlobClient(name);
            blobClient.Delete();
            return name;
        }

        public BlobContainerClient GetContainer(string containerName)
        {
            BlobContainerClient container = _blobClient.GetBlobContainerClient(containerName);
            return container;
        }

        public async Task<string> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            string name = file.FileName;

            BlobContainerClient container = GetContainer(containerName);
            BlobClient blobClient = container.GetBlobClient(name);
            await blobClient.UploadAsync(stream);

            return name;
        }
    }
}

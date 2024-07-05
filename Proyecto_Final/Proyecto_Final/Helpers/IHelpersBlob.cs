using Azure.Storage.Blobs;

namespace Proyecto_Final.Helpers
{
    public interface IHelpersBlob
    {
        string DeleteBlob(string name, string containerName);
        BlobContainerClient GetContainer(string containerName);
        Task<string> UploadBlobAsync(IFormFile file, string containerName);
    }
}

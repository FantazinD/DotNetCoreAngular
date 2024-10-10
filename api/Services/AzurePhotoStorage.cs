using api.Interfaces;
using Azure;
using Azure.Storage.Blobs;

namespace api.Services
{
    public class AzurePhotoStorage : IPhotoStorage
    {
        private readonly string _connectionString = "<Connection_String>";
        private readonly string _containerName = "<Container_Name>";
        public async Task<string> StorePhoto(string uploadsFolderPath, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is not provided or is empty.");
            }

            try
            {
                var blobServiceClient = new BlobServiceClient(_connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

                // Create the container if it doesn't already exist
                await containerClient.CreateIfNotExistsAsync();

                // Generate a unique filename
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var blobClient = containerClient.GetBlobClient(fileName);

                // Upload the file
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                return fileName;
            }
            catch (RequestFailedException ex)
            {
                throw new Exception("Error uploading the file.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while uploading the file.", ex);
            }
        }
    }
}
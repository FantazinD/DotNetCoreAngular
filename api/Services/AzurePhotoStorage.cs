using api.DTOs.Photo;
using api.Interfaces;
using api.Models;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace api.Services
{
    public class AzurePhotoStorage(IOptionsSnapshot<AppSettings> optionsSnapshot) : IPhotoStorage
    {
        private readonly AppSettings _appSettings = optionsSnapshot.Value;
        public async Task<PhotoDTO> StorePhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is not provided or is empty.");
            }

            try
            {
                var blobServiceClient = new BlobServiceClient(_appSettings.PhotoStorageConnectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(_appSettings.PhotoStorageName);

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

                return new PhotoDTO {
                    FileName = fileName,
                    URL = blobClient.Uri.ToString()
                };
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
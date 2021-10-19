using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private readonly string _azureConnectionString;
        
        public UploadController(IConfiguration configuration)
        {
            _azureConnectionString = configuration.GetConnectionString("AzureConnectionString");
        }

        [HttpPost("{blobContainer}")]
        public async Task<IActionResult> UploadFile(string blobContainer)
        {
            var response = await MethodUpload(blobContainer);
            return Ok(response);
        }
        
        public async Task<string> MethodUpload(string blobContainer)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files[0];

                if (file.Length <= 0) return "File null";
                
                var container = new BlobContainerClient(_azureConnectionString, blobContainer);
                var createResponse = await container.CreateIfNotExistsAsync();

                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                {
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);
                }
                
                var blob = container.GetBlobClient(file.FileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                await using (var fileStream = file.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders {ContentType = file.ContentType});
                }

                return blob.Uri.ToString();
            }
            catch (Exception ex)
            {
                return $"Internal server error: {ex}";
            }
        }
    }
}
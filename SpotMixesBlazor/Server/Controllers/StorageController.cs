using System;
using System.Threading.Tasks;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : Controller
    {
        [HttpPost("{folderName}")]
        public async Task<IActionResult> Upload(string folderName)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files[0];

                if (file.Length <= 0) 
                    return BadRequest("No se ha podido encontrar el archivo");
                
                var responseFirebase = new FirebaseStorage("spotmixes-test.appspot.com",
                        new FirebaseStorageOptions
                        {
                            ThrowOnCancel = true
                        })
                    .Child(folderName)
                    .Child(file.FileName)
                    .PutAsync(file.OpenReadStream());

                var fileLink = await responseFirebase;
                return Ok(fileLink);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }
    }
}
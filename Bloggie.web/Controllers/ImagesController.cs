using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.web.Controllers
{
    

    // Defines the route for this controller (e.g., https://localhost:****/api/images)
    [Route("api/[controller]")]
    // Marks the class as an API controller, enabling certain API-specific behaviors
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // Private field to hold the injected repository instance
         private readonly IImageRepository imageRepository;

        // Constructor for Dependency Injection
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
       
            // 1. Call the repository to upload the file and get the URL
            var imageURL = await imageRepository.UploadAsync(file);

            // 2. Check if the upload was successful (repository returns null on failure)
            if (imageURL == null)
            {
                // 3. Return a 500 Internal Server Error (ProblemDetails response)
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            // 4. Return a 200 OK response with the image URL in a JSON object
            return new JsonResult(new { link = imageURL });

        }

    }
}

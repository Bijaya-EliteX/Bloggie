using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    // Defines the route for this controller (e.g., https://localhost:****/api/images)
    [Route("api/[controller]")]
    // Marks the class as an API controller, enabling certain API-specific behaviors
    [ApiController]
    public class ImagesController : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //call a repository
            throw new NotImplementedException();

        }

    }
}

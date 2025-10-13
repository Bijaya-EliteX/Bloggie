using Microsoft.AspNetCore.Mvc;
namespace Bloggie.web.Controllers
{
    public class AdminBlogPostsController: Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}

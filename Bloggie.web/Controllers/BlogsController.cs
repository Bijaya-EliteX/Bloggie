using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository) {
            this.blogPostRepository = blogPostRepository;
        }
        [Route("urlhandle")]
        [HttpGet]

        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            return View(blogPost);
        }
    }
}

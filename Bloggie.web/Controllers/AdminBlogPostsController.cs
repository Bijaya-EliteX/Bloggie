using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;

        // Constructor for dependency injection
        public AdminBlogPostsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        // GET Action: Displays the form with a list of tags
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get tags from repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        // POST Action: Handles form submission
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // Placeholder for form processing logic
            return RedirectToAction("Add");
        }
    }
}
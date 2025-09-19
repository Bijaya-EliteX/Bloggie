using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BloggiedbContext bloggiedbContext;

        public AdminTagController(BloggiedbContext bloggiedbContext)
        {
            this.bloggiedbContext = bloggiedbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            bloggiedbContext.Tags.Add(tag);
            bloggiedbContext.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //use dbcontext to read the tags from the database
            var tags = bloggiedbContext.Tags.ToList();
            return View(tags);
        }
    }
}

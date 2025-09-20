using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await bloggiedbContext.Tags.AddAsync(tag);
            await bloggiedbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use dbcontext to read the tags from the database
            var tags =await bloggiedbContext.Tags.ToListAsync();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await bloggiedbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag != null)
            {
                //mapping domain model to view model
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View("Null");
        }

        // This action responds to HTTP POST requests when the edit form is submitted.
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag =await bloggiedbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                // Update the properties of the existing tag.
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                // Save the changes to the database.
                await bloggiedbContext.SaveChangesAsync();

                // After a successful update, redirect the user back to the 'Edit' page to show the changes.
                // Pass the ID as a route value to reload the correct tag.
                return RedirectToAction("List", new { id = editTagRequest.Id });
            }

            // If the tag was not found, redirect back to the Edit page.
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await bloggiedbContext.Tags.FindAsync(editTagRequest.Id);
            if (tag != null)
            {
                bloggiedbContext.Tags.Remove(tag);
                bloggiedbContext.SaveChanges();
                return RedirectToAction("List", new { id = editTagRequest.Id });
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
    }   }
}

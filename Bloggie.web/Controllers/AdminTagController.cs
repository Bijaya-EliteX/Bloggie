using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bloggie.web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use dbcontext to read the tags from the database
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }
        [HttpGet]
        //just the form is displayed
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
            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
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
            var updatedtag =await tagRepository.UpdateAsync(tag);
            if (updatedtag != null)
            {
                // Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            // If the tag was not found, redirect back to the Edit page.
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedtag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedtag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
    
        }   
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        // Properties copied from BlogPost Domain Model
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // --- Tags functionality for the Edit form ---

        // Display tags (used to populate the <select> element)
        public IEnumerable<SelectListItem> Tags { get; set; }

        // Collect tag (used to collect the selected tags from the form)
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public bool Visible { get; set; }

        // Property to display all available tags in a dropdown
        // THIS is what @Model.Tags refers to:
        public IEnumerable<SelectListItem> Tags { get; set; }
        // It's initialized to an empty array to prevent null reference errors.
        public string[]  SelectedTags { get; set; }=Array.Empty<string>();
    }
}

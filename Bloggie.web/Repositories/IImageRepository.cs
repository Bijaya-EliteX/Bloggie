using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace Bloggie.web.Repositories
{
    public interface IImageRepository
    {
        // Method definition to upload a file asynchronously and return the URL (string)
        Task<string> UploadAsync(IFormFile file);
    }
}

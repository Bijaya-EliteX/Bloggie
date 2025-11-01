using System.Diagnostics;
using Bloggie.web.Models;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogRepository )
        {
            _logger = logger;
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await blogRepository.GetAllAsync(); 
            return View(blogPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}


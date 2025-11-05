using Bloggie.web.Models.Domain;    
using Microsoft.EntityFrameworkCore;
namespace Bloggie.web.Data
{
    public class BloggiedbContext:DbContext
    {
        public BloggiedbContext(DbContextOptions<BloggiedbContext> options):base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}

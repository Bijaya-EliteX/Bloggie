using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;    

namespace Bloggie.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggiedbContext bloggiedbContext;

        public BlogPostRepository(BloggiedbContext bloggiedbContext)
        {
            this.bloggiedbContext = bloggiedbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggiedbContext.AddAsync(blogPost);
            await bloggiedbContext.SaveChangesAsync();
            return blogPost;
        }
        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
     

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggiedbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

         
        public Task<BlogPost?> GetAsync(Guid id) { throw new NotImplementedException(); }
        public Task<BlogPost?> UpdateAsync(BlogPost blogpost) { throw new NotImplementedException(); }
       
    }
}
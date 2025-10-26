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


        public async Task<BlogPost?> GetAsync(Guid id)
        {
            // Eagerly load the related Tags property
            return await bloggiedbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<BlogPost?> UpdateAsync(BlogPost blogpost) { throw new NotImplementedException(); }
       
    }
}
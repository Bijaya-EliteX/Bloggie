using Bloggie.web.Data;
using Bloggie.web.Models.Domain;

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
        public Task<IEnumerable<BlogPost>> GetAllAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetAsync(Guid id) { throw new NotImplementedException(); }
        public Task<BlogPost?> UpdateAsync(BlogPost blogpost) { throw new NotImplementedException(); }
        
    }
}
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
        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost) 
        {
            var existingBlog = await bloggiedbContext.BlogPosts
                                     .Include(x => x.Tags) // Eager load tags for modification
                                     .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                // 2. Update the properties of the existing blog post
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Author = blogPost.Author;
                existingBlog.Visible = blogPost.Visible;

                // 3. Update Tags (Clear existing tags and add the new ones)
                existingBlog.Tags = blogPost.Tags;

                // Save changes to the database
                await bloggiedbContext.SaveChangesAsync();

                return existingBlog;
            }

            return null;
        }
       
    }
}
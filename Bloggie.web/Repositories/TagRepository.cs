using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggiedbContext bloggiedbContext;

        // Constructor to inject the DbContext
        public TagRepository(BloggiedbContext bloggiedbContext)
        {
            this.bloggiedbContext = bloggiedbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggiedbContext.Tags.AddAsync(tag);
            await bloggiedbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggiedbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                bloggiedbContext.Tags.Remove(existingTag);
                await bloggiedbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await bloggiedbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await bloggiedbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggiedbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggiedbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }
    }
}
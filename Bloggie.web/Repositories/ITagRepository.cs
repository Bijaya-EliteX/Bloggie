using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid id); //read
        Task<Tag> AddAsync(Tag tag); //create
        Task<Tag?> UpdateAsync(Tag tag); //update
        Task<Tag?> DeleteAsync(Guid id); //delete
    }
}

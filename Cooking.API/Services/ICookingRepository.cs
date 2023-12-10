using Cooking.API.Entities;

namespace Cooking.API.Services
{
    public interface ICookingRepository
    {
        Task<(IEnumerable<Cookware>, PaginationMetaData)> GetCookwaresAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Cookware?> GetCookwareAsync(int id);
        void AddCookware(Cookware cookware);
        void DeleteCookware(Cookware cookware);
        Task<bool> SaveChangesAsync();
    }
}

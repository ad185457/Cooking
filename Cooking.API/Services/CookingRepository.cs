using Cooking.API.DbContexts;
using Cooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cooking.API.Services
{
    public class CookingRepository : ICookingRepository
    {
        private readonly CookingContext _context;
        public CookingRepository(CookingContext context)
        {
            _context = context;
        }

        public void AddCookware(Cookware cookware)
        {
            _context.Add(cookware);
        }

        public void DeleteCookware(Cookware cookware)
        {
            _context.Cookwares.Remove(cookware);
        }

        public async Task<Cookware?> GetCookwareAsync(int id)
        {
            return await _context.Cookwares.Where(c =>  c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<Cookware>, PaginationMetaData)> GetCookwaresAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Cookwares as IQueryable<Cookware>;
            if(!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }
            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery) || a.Color.Contains(searchQuery));
            }
            var totalItemCount = await collection.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var collectionToReturn = await collection.OrderBy(c => c.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
            return (collectionToReturn, paginationMetaData);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

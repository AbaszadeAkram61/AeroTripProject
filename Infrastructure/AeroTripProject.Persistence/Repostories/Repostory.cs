using AeroTripProject.Application.Repostories;
using AeroTripProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AeroTripProject.Persistence.Repostories
{
    public class Repostory<T> : IRepostory<T> where T:class
    {
        private readonly AeroTripDbContext _context;

        public Repostory(AeroTripDbContext context)
        {
            _context = context;
        }

        public async Task Delete(T t)
        {
            _context.Remove(t);
           await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetList()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task Insert(T t)
        {
           await _context.AddAsync(t);
           await _context.SaveChangesAsync();
        }

        public async Task Update(T t)
        {
            _context.Update(t);
           await _context.SaveChangesAsync();

        }
    }
}

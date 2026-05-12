using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using AeroTripProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace AeroTripProject.Persistence.Repostories
{
    public class Repostory<T> : IRepostory<T> where T:BaseEntity
    {
        private readonly AeroTripDbContext _context;

        public Repostory(AeroTripDbContext context)
        {
            _context = context;
        }


        public DbSet<T> Table => _context.Set<T>();

        public async Task<List<T>> GetListAsync()
        {
           return await Table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(T t)
        {
           await _context.AddAsync(t);
           await _context.SaveChangesAsync();
        }

       

        public async Task DeleteAsync(int Id)
        {
            T t= await Table.FirstOrDefaultAsync(x => x.Id == Id);
            Table.Remove(t);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T t)
        {
           Table.Update(t);
           await _context.SaveChangesAsync();
           return t;
        }

        public async Task<int> CountAsync()
        {
            var count= await Table.CountAsync();
            return count;
        }

        public async Task<List<T>> GetByIdListFilterAsyc(Expression<Func<T, bool>> filter)
        {
            return await Table.Where(filter).ToListAsync();
        }

        public Task<List<string>> GetListNameAsync(Expression<Func<T, string>> selector)
        {
            return Table.Select(selector).ToListAsync();
        }

        
    }
}

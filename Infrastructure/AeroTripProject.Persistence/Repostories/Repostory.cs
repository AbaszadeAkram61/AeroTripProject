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

        public async Task<int> GetListFilterSumAsyc(Expression<Func<T, bool>> filter, Expression<Func<T, int>> selector)
        {
            return await Table.Where(filter).SumAsync(selector);
        }

        public async Task<List<TResult>> GetSelectedListAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await Table.Select(selector).ToListAsync();
        }

        public async Task<List<T>> GetListIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> ChangeStatusAsync(int id, bool status)
        {
            var T = await Table.FirstOrDefaultAsync(x => x.Id == id);

            T.Status = status;

            await _context.SaveChangesAsync();

            return T;
        }
    }
}

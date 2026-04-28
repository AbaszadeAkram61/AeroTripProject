using AeroTripProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Repostories
{
    public interface IRepostory<T> where T:BaseEntity
    {
        Task InsertAsync(T t);
        Task <int> CountAsync();
        Task DeleteAsync(int Id);
        Task<T> UpdateAsync(T t);
        Task<List<T>> GetListAsync();
        Task<T> GetByIdAsync(int Id);
    }
}

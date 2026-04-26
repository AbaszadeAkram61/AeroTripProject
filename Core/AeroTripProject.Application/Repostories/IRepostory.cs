using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Repostories
{
    public interface IRepostory<T> where T:class
    {
        Task Insert(T t);
        Task Delete(T t);
        Task Update(T t);
       Task<List<T>> GetList();
    }
}

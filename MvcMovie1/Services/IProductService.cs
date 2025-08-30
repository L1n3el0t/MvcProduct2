using System.Collections.Generic;
using System.Threading.Tasks;
using MvcMovie1.Models;

namespace MvcMovie1.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);
    }
}

using Dapper_Example_Project.Entities;

namespace Dapper_Example_Project.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> ProductByCategoryASync(int CategoryId);
    }
}

using Dapper_Example_Project.Entities;

namespace Dapper_Example_Project.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> TopFiveCategoryAsync();
    }
}

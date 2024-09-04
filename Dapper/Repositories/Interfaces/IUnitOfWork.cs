using System.Data.Common;

namespace Dapper_Example_Project.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository _productRepository { get; }

        ICategoryRepository _categoryRepository { get; }
        void Commit();
        void Dispose();
    }
}

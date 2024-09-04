using Dapper_Example_Project.Connection;
using Dapper_Example_Project.Repositories.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dapper_Example_Project.Repositories
{
    public class UnitofWork : IUnitOfWork, IDisposable
    {
        public IProductRepository _productRepository { get; }

        public ICategoryRepository _categoryRepository { get; }

        IDbTransaction _dbTransaction;

        public UnitofWork(IProductRepository productRepository, ICategoryRepository categoryRepository, IDbTransaction dbTransaction)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                //_dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}

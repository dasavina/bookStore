using Dapper;
using Dapper_Example_Project.Entities;
using Dapper_Example_Project.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Dapper_Example_Project.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Product")
        {
        }

        public async Task<IEnumerable<Product>> ProductByCategoryASync (int productsCategoryId)
        {
            string sql = @"SELECT * FROM Product WHERE CategoryId = @ProductsCategoryId";

            var results = await _sqlConnection.QueryAsync<Product>(sql,
                param: new { ProductsCategoryId = productsCategoryId },
                transaction: _dbTransaction);
            return results;

        }
    }
}

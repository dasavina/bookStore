using Dapper;
using DapperPart.Entities;
using DapperPart.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DapperPart.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Comment")
        {
        }

        public async Task<IEnumerable<Comment>> GetCommentsForBook(int bookID)
        {
            string sql = @"SELECT * 
                   FROM Comment 
                   WHERE bookID = @BookID 
                   ORDER BY id DESC";

            var results = await _sqlConnection.QueryAsync<Comment>(sql, new { BookID = bookID }, transaction: _dbTransaction);
            return results;
        }

    }
}

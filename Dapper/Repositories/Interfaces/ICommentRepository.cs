using DapperPart.Entities;

namespace DapperPart.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsForBook(int bookID);
    }
}

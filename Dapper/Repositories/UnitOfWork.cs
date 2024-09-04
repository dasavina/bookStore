using System.Data;

namespace DapperPart.Repositories.Interfaces
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IUserRepository _userRepository { get; set; }
        public IBookRepository _bookRepository { get; set; }
        public ICommentRepository _commentRepository { get; set; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(IUserRepository userRepository, IBookRepository bookRepository, ICommentRepository commentRepository, IDbTransaction dbTransaction)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _commentRepository = commentRepository;
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

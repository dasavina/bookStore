namespace DapperPart.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository _bookRepository { get; }
        ICommentRepository _commentRepository { get; }
        IUserRepository _userRepository { get; }
        void Commit();
        void Dispose();
    }
}

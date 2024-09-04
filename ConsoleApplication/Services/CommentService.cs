using ConsoleApplication.Services.Interfaces;
using DapperPart.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task GetAllInfoAboutComment(int id)
        {
            // Fetch the comment data from the repository using the id
            var comment = await _unitOfWork._commentRepository.GetAsync(id);

            // Display the comment details in the console with formatting
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-->> Зведена інформація про коментар");
            Console.WriteLine("-->> Ідентифікатор коментаря - \t" + comment.Id);
            Console.WriteLine("-->> Тіло коментаря - \t" + comment.Body);

            // Fetch user information using UserID
            var user = await _unitOfWork._userRepository.GetAsync(comment.UserID);
            Console.WriteLine("-->> Користувач, що залишив коментар - \t" + user.Name + " (Email: " + user.Email + ")");

            // Fetch book information using BookID
            var book = await _unitOfWork._bookRepository.GetAsync(comment.BookID);
            Console.WriteLine("-->> Книга, до якої залишено коментар - \t" + book.Name);

            Console.WriteLine("" + Environment.NewLine);
            Console.ResetColor();
        }
    }


}

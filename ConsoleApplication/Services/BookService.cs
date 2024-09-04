using ConsoleApplication.Services.Interfaces;
using DapperPart.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task GetAllInfoAboutBook(int id)
        {
            // Fetch the book data from the repository using the id
            var book = await _unitOfWork._bookRepository.GetAsync(id);

            // Display the book details in the console with formatting
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-->> Зведена інформація про книгу");
            Console.WriteLine("-->> Назва книги - \t" + book.Name);
            Console.WriteLine("-->> Опис книги - \t" + book.Description);
            Console.WriteLine("-->> ISBN книги - \t" + book.ISBN);
            Console.WriteLine("-->> Ціна книги - \t" + book.Price);
            Console.WriteLine("-->> В наявності - \t" + (book.InStorage ? "Так" : "Ні"));
            Console.WriteLine("-->> Обкладинка книги - \t" + book.Cover);
            Console.WriteLine("-->> Жанр книги - \t" + book.Genre);

            // Fetch the author of the book using AuthorID
            //todo

            Console.WriteLine("" + Environment.NewLine);
            Console.ResetColor();
        }
    }

}

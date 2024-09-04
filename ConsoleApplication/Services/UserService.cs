using ConsoleApplication.Services.Interfaces;
using DapperPart.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task GetAllInfoAboutUser(int id)
        {
            // Fetch the user data from the repository using the id
            var user = await _unitOfWork._userRepository.GetAsync(id);

            // Display the user details in the console with formatting
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-->> Зведена інформація про користувача");
            Console.WriteLine("-->> Ім'я користувача - \t" + user.Name);
            Console.WriteLine("-->> Електронна пошта користувача - \t" + user.Email);

            Console.WriteLine("" + Environment.NewLine);
            Console.ResetColor();
        }
    }

}

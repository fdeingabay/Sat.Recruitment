using System;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Model.Users.Factory;
using Domain.Repositories;

namespace Domain.Services.Users
{
    public class UsersService : IUsersService
    {
        private IUserFactoryResolver userFactoryResolver;
        private readonly IUsersRepository usersRepository;

        public UsersService(
            IUserFactoryResolver userFactoryResolver,
            IUsersRepository usersRepository)
        {
            this.userFactoryResolver = userFactoryResolver ?? throw new ArgumentNullException(nameof(userFactoryResolver));
            this.usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public Task CreateUser(UserDto userDto)
        {
            var normalizedEmail = NormalizeEmail(userDto.Email);

            var newUser = this.userFactoryResolver
                .GetFactory(userDto.UserType)
                .Create(userDto.Name, normalizedEmail, userDto.Address, userDto.Phone, userDto.Money);

            return this.usersRepository.Add(newUser);
        }

        private static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
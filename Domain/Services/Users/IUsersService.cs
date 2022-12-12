using System.Threading.Tasks;
using Domain.DTOs;

namespace Domain.Services.Users
{
    public interface IUsersService
    {
        Task CreateUser(UserDto userDto);
    }
}
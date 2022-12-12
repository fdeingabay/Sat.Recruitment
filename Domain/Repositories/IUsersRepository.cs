using System.Threading.Tasks;
using Domain.Model.Users;

namespace Domain.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User newUser);
    }
}
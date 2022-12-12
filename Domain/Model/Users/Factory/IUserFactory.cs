using Domain.ValueObjects;

namespace Domain.Model.Users.Factory
{
    public interface IUserFactory
    {
        public UserType UserType { get; }

        User Create(string name, string email, string address, string phone, decimal money);
    }
}
using Domain.ValueObjects;

namespace Domain.Model.Users.Factory
{
    public class PremiumUserFactory : IUserFactory
    {
        public UserType UserType { get => UserType.Premium; }

        public User Create(string name, string email, string address, string phone, decimal money)
        {
            decimal initialMoney = money;
            if (money > 100)
            {
                var gift = initialMoney * 2;
                initialMoney += gift;
            }

            return new User(name, email, address, phone, initialMoney, this.UserType);
        }
    }
}
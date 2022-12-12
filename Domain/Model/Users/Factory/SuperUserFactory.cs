using System;
using Domain.ValueObjects;

namespace Domain.Model.Users.Factory
{
    public class SuperUserFactory : IUserFactory
    {
        public UserType UserType { get => UserType.SuperUser; }

        public User Create(string name, string email, string address, string phone, decimal money)
        {
            decimal initialMoney = money;
            if (initialMoney > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gift = initialMoney * percentage;
                initialMoney += gift;
            }

            return new User(name, email, address, phone, initialMoney, this.UserType);
        }
    }
}
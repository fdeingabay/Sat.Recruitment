using System;
using Domain.ValueObjects;

namespace Domain.Model.Users.Factory
{
    public class NormalUserFactory : IUserFactory
    {
        public UserType UserType { get => UserType.Normal; }

        public User Create(
            string name, string email, string address, string phone, decimal money)
        {
            decimal gift = 0;
            var initialMoney = money;

            if (initialMoney > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                
                gift = initialMoney * percentage;
            }
            else if (initialMoney > 10)
            {
                var percentage = Convert.ToDecimal(0.8);
                gift = initialMoney * percentage;
            }

            initialMoney += gift;

            return new User(name, email, address, phone, initialMoney, this.UserType);
        }
    }
}
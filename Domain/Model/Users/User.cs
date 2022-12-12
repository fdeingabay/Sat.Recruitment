using System;
using Domain.ValueObjects;

namespace Domain.Model.Users
{
    public class User
    {
        public string Name { get; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; }
        public decimal Money { get; }

        public User(string name, string email, string address, string phone, decimal money, UserType userType)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Email = email ?? throw new ArgumentNullException(nameof(email));
            this.Address = address ?? throw new ArgumentNullException(nameof(address));
            this.Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            this.Money = money;
            this.UserType = userType;
        }
    }
}
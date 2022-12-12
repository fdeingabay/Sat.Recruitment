using Domain.ValueObjects;

namespace Domain.Model.Users.Factory
{
    public interface IUserFactoryResolver
    {
        IUserFactory GetFactory(UserType userType);
    }
}
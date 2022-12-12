using System;
using Domain.Model.Exceptions;
using Domain.Model.Users.Factory;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Sat.Recruitment.Test.Domain.Model.Users.Factory
{
    public class UserFactoryTest
    {
        public UserFactoryTest()
        {
        }

        [Fact]
        public void Non_Existent_User_Type_Throws_InvalidUserTypeException()
        {
            // Arrange
            var userFactoryResolver = new UserFactoryResolver(new IUserFactory[] { new NormalUserFactory() });

            // Act & Assert
            Func<IUserFactory> result = () => userFactoryResolver.GetFactory(UserType.Premium);
            result.Should().Throw<InvalidUserTypeException>();
        }

        [Fact]
        public void Existent_User_Type_Returns_Correct_Factory()
        {
            // Arrange
            var userFactoryResolver = new UserFactoryResolver(new IUserFactory[] { new NormalUserFactory(), new PremiumUserFactory() });

            // Act
            var result = userFactoryResolver.GetFactory(UserType.Normal);

            // Assert
            result.Should().BeEquivalentTo(new NormalUserFactory());
        }
    }
}
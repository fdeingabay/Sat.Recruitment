using System;
using System.Threading.Tasks;
using Domain.Model.Exceptions;
using Domain.Model.Users;
using Domain.Repositories;
using Domain.ValueObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Domain.ValueObjects;
using Xunit;

namespace Sat.Recruitment.Test.Domain.Repositories
{
    public class UsersRepositoryTest : IClassFixture<UsersFixture>
    {
        private readonly TestServer server;

        public UsersRepositoryTest()
        {
            this.server = new TestServer(
                new WebHostBuilder()
                    .ConfigureAppConfiguration(configurationBuilder =>
                    {
                        configurationBuilder.AddJsonFile("appsettings.json");
                    })
                    .UseStartup<Startup>());
        }

        [Fact]
        public async Task Add_User_With_Existing_Email_Throws_DuplicatedUserException()
        {
            // Arrange
            var testUser = new User("Juan", "juan@mail.com", "av siempre viva 123", "+54123456", 100, UserType.Normal);

            //var repository = new UsersRepository(this.usersContext);
            var repository = this.server.Services.GetService<IUsersRepository>();

            await repository.Add(testUser);

            // Act
            Func<Task> result = async () => await repository.Add(testUser);

            // Assert
            await result.Should()
                .ThrowAsync<DuplicatedUserException>()
                .WithMessage(Messages.UserDuplicatedError);
        }

        [Fact]
        public async Task Add_User_With_Existing_Phone_Throws_DuplicatedUserException()
        {
            // Arrange
            var testUser = new User("Pepe", "pepe@mail.com", "corrientes 514", "+55555", 100, UserType.Normal);

            var repository = this.server.Services.GetService<IUsersRepository>();

            testUser.Address = "corrientes 515";
            testUser.Email = "otroemail@email.com";

            await repository.Add(testUser);

            // Act
            Func<Task> result = async () => await repository.Add(testUser);

            // Assert
            await result.Should()
                .ThrowAsync<DuplicatedUserException>()
                .WithMessage(Messages.UserDuplicatedError);
        }

        [Fact]
        public async Task Add_User_With_Existing_Name_And_Address_Throws_DuplicatedUserException()
        {
            // Arrange
            var testUser = new User("Carlos", "carlos@mail.com", "malabia 123", "+123123", 100, UserType.Normal);

            var repository = this.server.Services.GetService<IUsersRepository>();

            testUser.Email = "otroemailcarlos@email.com";
            testUser.Phone = "321654897";

            await repository.Add(testUser);

            // Act
            Func<Task> result = async () => await repository.Add(testUser);

            // Assert
            await result.Should()
                .ThrowAsync<DuplicatedUserException>()
                .WithMessage(Messages.UserDuplicatedError);
        }

        [Fact]
        public async Task Add_Non_Existent_User_Saves_User_Correctly()
        {
            // Arrange
            var testUser = new User("new user", "newuser@mail.com", "new user address", "new user phone", 100, UserType.Normal);

            var repository = this.server.Services.GetService<IUsersRepository>();

            await repository.Add(testUser);

            testUser.Email = "newuser2@mail.com";
            testUser.Phone = "new user phone 2";
            testUser.Address = "new user address 2";

            // Act
            Func<Task> result = async () => await repository.Add(testUser);

            // Assert
            await result.Should()
                .NotThrowAsync<Exception>();
        }
    }
}
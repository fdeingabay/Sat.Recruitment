using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs;
using Domain.Model.Exceptions;
using Domain.Services.Users;
using FluentAssertions;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Extensions;
using Sat.Recruitment.Api.Infrastructure.AutoMapper;
using Sat.Recruitment.Api.ValueObjects;
using Sat.Recruitment.Domain.ValueObjects;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    public class UsersControllerTest
    {
        private readonly Mock<IUsersService> usersServiceMock;
        private readonly IMapper mapper;

        public UsersControllerTest()
        {
            this.usersServiceMock = new Mock<IUsersService>();
            this.mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper();
        }

        [Fact]
        public async Task Create_User_Returns_Successful_Result()
        {
            // Arrange
            this.usersServiceMock
                .Setup(x => x.CreateUser(It.IsAny<UserDto>()))
                .Returns(Task.CompletedTask);

            var controller = new UsersController(this.usersServiceMock.Object, mapper);

            // Act
            var result = await controller.CreateUser(new Api.Requests.CreateUserRequest());

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(new Result(){ IsSuccess = true, Description = Messages.UserCreated});
        }

        [Fact]
        public async Task Create_User_When_Is_Duplicated_Returns_Failed_Result()
        {
            // Arrange
            var ex = new DuplicatedUserException("exception message");

            this.usersServiceMock
                .Setup(x => x.CreateUser(It.IsAny<UserDto>()))
                .ThrowsAsync(ex);

            var controller = new UsersController(this.usersServiceMock.Object, mapper);

            // Act
            var result = await controller.CreateUser(new Api.Requests.CreateUserRequest());

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(ex.ToFailedResult());
        }
    }
}
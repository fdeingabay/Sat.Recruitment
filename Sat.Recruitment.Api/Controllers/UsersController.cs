using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs;
using Domain.Model.Exceptions;
using Domain.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Extensions;
using Sat.Recruitment.Api.Requests;
using Sat.Recruitment.Api.ValueObjects;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<ActionResult<Result>> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                await this.usersService.CreateUser(this.mapper.Map<UserDto>(request));
            }
            catch(Exception ex)
            {
                return ex.ToFailedResult();
            }

            return new Result()
            {
                IsSuccess = true,
                Description = Messages.UserCreated
            };
        }
    }
}
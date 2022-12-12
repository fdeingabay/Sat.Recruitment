using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Sat.Recruitment.Api.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Money { get; set; }        
        [EnumDataType(typeof(UserType))]
        public string UserType { get; set; }
    }
}
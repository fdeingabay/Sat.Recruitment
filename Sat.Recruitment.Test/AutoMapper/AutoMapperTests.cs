using AutoMapper;
using Sat.Recruitment.Api.Infrastructure.AutoMapper;
using Xunit;

namespace Sat.Recruitment.Test.AutoMapper
{
    public class AutoMapperTests
    {
        [Fact]
        public void Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
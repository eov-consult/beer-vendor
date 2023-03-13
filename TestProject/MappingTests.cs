using Api.Mapper;
using AutoMapper;

namespace TestProject
{
    public class MappingTests
    {
        private readonly MapperConfiguration _configuration;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}

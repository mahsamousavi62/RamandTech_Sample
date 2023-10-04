using Dapper;
using Moq;
using Moq.Dapper;
using RamandTech.Dapper.Entities;
using RamandTech.Dapper.IServices;
using RamandTech.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTech.UnitTest.Dapper
{
    public class UserRepositoryTest
    {

        private readonly Mock<IUserRepository> _repository = new();

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User
            {
                Id = 1,
                FirstName = "mahsa",
                LastName = "mousavi",
                Username = "user1",
                Password = "111111"
            };


            var configuration = new ConfigurationBuilder()
              .AddInMemoryCollection(new Dictionary<string, string>
              {
                    { "ConnectionStrings:DefaultConnection", "Server=.;Database=RamandTech;User Id =sa;password=123;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" },

              })
              .Build();


            var userRepository = new UserRepository(configuration);

            // Act
            var result = await userRepository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            // Assert other user properties as needed
        }


        [Fact]
        public async Task GetAll_ShouldReturnCount()
        {
            var configuration = new ConfigurationBuilder()
              .AddInMemoryCollection(new Dictionary<string, string>
              {
                    { "ConnectionStrings:DefaultConnection", "Server=.;Database=RamandTech;User Id =sa;password=123;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" },

              })
              .Build();

            var userRepository = new UserRepository(configuration);

            // Act
            var result = await userRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Count());
            // Assert other user properties as needed
        }
    }
}

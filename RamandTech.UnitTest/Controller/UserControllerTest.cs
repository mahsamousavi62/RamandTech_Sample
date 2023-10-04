using Azure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RamandTech.Api.Controllers.v1;
using RamandTech.Api.Model;
using RamandTech.Dapper.Entities;
using RamandTech.Dapper.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTech.UnitTest.Controller
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public async Task Authenticate_ReturnsBadRequest_WhenInvalidCredentials()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "Server=.;Database=RamandTech;User Id =sa;password=123;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" },

                })
                .Build();


            var userRepository = new UserRepository(configuration);


            var controller = new UsersController(userRepository);
            var requestModel = new AuthenticateRequest
            {
                Username = "invalidUsername",
                Password = "invalidPassword"
            };

            // Act
            var result = await controller.Authenticate(requestModel) as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Authenticate_ReturnsOkResult_WhenValidCredentials()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "Server=.;Database=RamandTech;User Id =sa;password=123;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" },
                     {"SecretKey:key","11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" }
    })
                .Build();


            var userRepository = new UserRepository(configuration);

            var controller = new UsersController(userRepository);
            var requestModel = new AuthenticateRequest
            {
                Username = "user1",
                Password = "111111"
            };

            // Act
            var result = await controller.Authenticate(requestModel);

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<string>(okResult.Value);
            Assert.NotNull(response);

        }
    }
}

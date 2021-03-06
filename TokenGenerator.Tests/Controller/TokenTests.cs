using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tests;
using TokenGenerator.Controllers;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenGeneratorService;
using TokenGeneratorService.Domain;
using TokenGeneratorService.Domain.Filters;
using TokenGeneratorService.Mapper;
using TokenGeneratorService.Services;
using Xunit;

namespace TokenGenerator.Tests.Controller
{
    public class TokenTests : IClassFixture<InjectionFixture>
    {
        private HttpClient _client;
        private CardsController _controller;
        private ITokenService _service;
        private IMapper _mapper;
        private IWebHostBuilder _builder;

        public TokenTests()
        {
            #region Setup Test Server

            var projectDir = Directory.GetCurrentDirectory();
            var configuration = (new ConfigurationBuilder().SetBasePath(projectDir).AddJsonFile("appsettings.json")).Build();
            var builder = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseEnvironment("Development")
                .UseStartup<CustomStartup.CustomStartup>();
            TestServer testServer = new TestServer(builder);
            _client = testServer.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:60793");

            #endregion

            #region Setup mock required instances
            var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
           .SetMinimumLevel(LogLevel.Trace)
             .AddConsole());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();


            ILogger<CardsController> logger = loggerFactory.CreateLogger<CardsController>();

            #endregion

            #region Setup Controller by using a fake Service implementation

            _service = new TokenGeneratorServiceFake();
            _controller = new CardsController(_service,logger,_mapper);

            #endregion
        }
        [Fact]
        public async Task SaveCard_ShouldReturnContent()
        {
            //Arrange
            var card = new SaveCardFilter()
            {
                CardNumber = 12345678,
                CustomerID = 1,
                CVV = 9999
            };
            //Act
            var response = await _client.PostAsync("/Cards/SaveCard",new StringContent(JsonSerializer.Serialize<SaveCardFilter>(card)));
            //Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseString);
            
         
        }
        [Fact]
        public async Task ValidateCard_ShouldReturnBool()
        {
            //Arrange
            var card = new SaveCardFilter()
            {
                CardNumber = 12345678,
                CustomerID = 1,
                CVV = 9999
            };
            //Act
            var response = await _client.PostAsync("/Cards/ValidateToken", new StringContent(JsonSerializer.Serialize<SaveCardFilter>(card)));
            //Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);


        }
        [Fact]
        public async Task SaveCard_ReturnsCreatedResponse()
        {
          
            // Arrange
            var card = new SaveCardFilter()
            {
                CardNumber = 12345678,
                CustomerID = 1,
                CVV = 9999
            };
            // Act
            var createdResponse = _controller.SaveCard(card);
            // Assert
            Assert.IsType<CreatedResult>(createdResponse);
        }
        [Fact]
        public async Task SaveCard_ReturnsBadRequest()
        {
            // Arrange
            var CVVWrongLength = new SaveCardFilter()
            {
               CardNumber = 12345678,
               CustomerID = 1,
               CVV = 2
            };
            _controller.ModelState.AddModelError("CVV", "Value must be between 1 to 99999");
            // Act
            var badResponse = _controller.SaveCard(CVVWrongLength );
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);

        }

        [Fact]
        public async Task ValidateToken_ExpiredToken_ReturnsBadRequest()
        {
            // Arrange
            var expired = new ValidateTokenFilter()
            {
                CardId = 2,
                CustomerID = 2,
                CVV = 2,
                Token = 7856
            };
            
            // Act
            var response = _controller.ValidateToken(expired);
            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

        }

        [Fact]
        public async Task ValidateToken_DifferentOwner_ReturnsBadRequest()
        {
            // Arrange
            var expired = new ValidateTokenFilter()
            {
                CardId = 3,
                CustomerID = 2,
                CVV = 2,
                Token = 7856
            };

            // Act
            var response = _controller.ValidateToken(expired);
            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

        }


        [Fact]
        public async Task ValidateToken_DifferentToken_ReturnsBadRequest()
        {
            // Arrange
            var diftoken = new ValidateTokenFilter()
            {
                CardId = 4,
                CustomerID = 1,
                CVV = 3,
                Token = 7856
            };
          //  System.Diagnostics.Debugger.Launch();
            // Act
            var response = _controller.ValidateToken(diftoken);
            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

        }

        [Fact]
        public async Task ValidateToken_ValidScenario_ReturnsOk()
        {
            
            // Arrange
            var card = new ValidateTokenFilter()
            {
                CardId = 1,
                CustomerID = 1,
                CVV = 2,
                Token = 7856
            };

            // Act
            var response = _controller.ValidateToken(card);
            // Assert
            Assert.IsType<OkObjectResult>(response);

        }

    }
    ///Test status codes from methods
    ///Test ID and Token return
    ///
    /// test time condition 
    /// 
    /// test card owner and not owner
    /// Test different tokens
}

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


        public TokenTests()
        {


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var projectDir = Directory.GetCurrentDirectory();
            var configuration = (new ConfigurationBuilder().SetBasePath(projectDir).AddJsonFile("appsettings.json")).Build();
            var builder = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseEnvironment("Development")
                .UseStartup<CustomStartup.CustomStartup>();
            TestServer testServer = new TestServer(builder);
            _client = testServer.CreateClient();
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
             .SetMinimumLevel(LogLevel.Trace)
               .AddConsole());
 
            ILogger<CardsController> logger = loggerFactory.CreateLogger<CardsController>();
            
            _service = new TokenGeneratorServiceFake();
            
            _controller = new CardsController(_service,logger,_mapper);
        }

        
        [Fact]
        public async Task SaveCard_ReturnsCreatedResponse()
        {
             //System.Diagnostics.Debugger.Launch();
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
               CVV = 9999999 
            };
            _controller.ModelState.AddModelError("CVV", "Value must be between 1 to 99999");
            // Act
            var badResponse = _controller.SaveCard(CVVWrongLength );
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);

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

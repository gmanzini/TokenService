using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using TokenGenerator;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenGeneratorService.Domain;
using TokenGeneratorService.Services;


namespace TokenGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerCardsController : Controller
    {
        private readonly TokenGeneratorContext _context;
        private ConnectionFactory _messageFactory;
        private ILogger<CustomerCardsController> _logger;
        private IConfiguration _configuration;
        private ITokenService _tokenGeneratorService;
        public CustomerCardsController(IConfiguration configuration, ITokenService tokenGeneratorService, ConnectionFactory messageFactory, ILogger<CustomerCardsController> logger, TokenGeneratorContext context)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _messageFactory = messageFactory;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CustomerID,CardNumber,CVV")] CardDTO customerCard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _tokenGeneratorService.CreateToken(customerCard, _context);

                    return Ok(new { CardId = response.Result.CardId, RegistrationDate = response.Result.RegistrationDate, Token = response.Result.Token });
                    //#region Publish Token and Registration Date to Rabbit MQ
                    //AsyncMessaging.Publish(_messageFactory,_configuration.GetValue<string>("TokenQueue"),JsonSerializer.Serialize(response));
                    //#endregion
                }

                return BadRequest(customerCard);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error during the request: " + ex.Message);
                return StatusCode(500, "Generic exception during the request, please contact your administrator");
            }
        }
    }
}

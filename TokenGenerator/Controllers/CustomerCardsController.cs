using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AsyncMessagesUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using TokenGenerator;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenUtils;

namespace TokenGenerator.Controllers
{
    public class CustomerCardsController : Controller
    {
        private readonly TokenGeneratorContext _context;
        private ConnectionFactory _messageFactory;
        private ILogger<CustomerCardsController> _logger;
        private IConfiguration _configuration;
        public CustomerCardsController(IConfiguration configuration, ConnectionFactory messageFactory, ILogger<CustomerCardsController> logger, TokenGeneratorContext context)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _messageFactory = messageFactory;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,CardNumber,CVV")] CustomerDTO customerCard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registrationDate = DateTime.Now;
                    var token = TokenGeneration.GenerateToken(customerCard.CardNumber.ToString()[^4..], customerCard.CVV);
                    #region Add to Database
                    customerCard.RegistrationDate = DateTime.Now;
                    customerCard.Token = token;
                    _context.Add(customerCard);
                    await _context.SaveChangesAsync();

                    #endregion

                    var response = new RegisterCardResponseDTO()
                    {
                        CardId = customerCard.id,
                        RegistrationDate = registrationDate,
                        Token = token
                    };
                    return Ok(response);
                    //#region Publish Token and Registration Date to Rabbit MQ
                    //AsyncMessaging.Publish(_messageFactory,_configuration.GetValue<string>("TokenQueue"),JsonSerializer.Serialize(response));
                    //#endregion
                }
              
                return BadRequest(customerCard);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error during the request: " + ex.Message);
                return StatusCode(500,"Generic exception during the request, please contact your administrator");
            }
        }



        private bool CustomerCardExists(string id)
        {
            return _context.CustomerCard.Any(e => e.CustomerID == id);
        }
    }
}

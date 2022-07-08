using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using TokenGenerator;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenGeneratorService;
using TokenGeneratorService.Domain;
using TokenGeneratorService.Domain.Filters;
using TokenGeneratorService.Services;


namespace TokenGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : Controller
    {
        private readonly TokenGeneratorContext _context;
        private ConnectionFactory _messageFactory;
        private ILogger<CardsController> _logger;
        private IConfiguration _configuration;
        private ITokenService _tokenGeneratorService;
        private IMapper _mapper;
        public CardsController(IMapper mapper,IConfiguration configuration, ITokenService tokenGeneratorService, ConnectionFactory messageFactory, ILogger<CardsController> logger, TokenGeneratorContext context)
        {
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _messageFactory = messageFactory;
            _tokenGeneratorService = tokenGeneratorService;
        }
        public CardsController(ITokenService tokenService,ILogger<CardsController> logger, IMapper mapper)
        {
            _tokenGeneratorService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("SaveCard")]
        public IActionResult SaveCard(SaveCardFilter customerCard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var card = _mapper.Map<CardDTO>(customerCard);
                    var response =  _tokenGeneratorService.SaveCard(card, _context);

                    return Created($"www.example/api/card/{card.CardId}",response);
                  
                }

                return BadRequest(customerCard);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error during the request: " + ex.Message);
                return StatusCode(500, "Generic exception during the request, please contact your administrator");
            }
        }
        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken(ValidateTokenFilter filter)
        {
            try
            {
                var card = _mapper.Map<CardDTO>(filter);
                bool result = _tokenGeneratorService.ValidateToken(_context, card);
                if (result) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error during the request: " + ex.Message);
                return StatusCode(500, "Generic exception during the request, please contact your administrator");
            }
        }
    }
}

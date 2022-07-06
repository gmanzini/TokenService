using AsyncMessagesUtil;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenGeneratorService.Domain;
using TokenGeneratorService.Services;
using TokenUtils;

namespace TokenGeneratorService
{
    public class TokenService : ITokenService
    {

        private ConnectionFactory _messageFactory;
        private ILogger<TokenService> _logger;
        private IConfiguration _configuration;
        public TokenService(ILogger<TokenService> logger,ConnectionFactory messageFactory,IConfiguration configuration)
        {
            logger = _logger;
            messageFactory = _messageFactory;
            configuration = _configuration;
        }
        public async Task<RegisterCardResponseDTO> CreateToken(CardDTO customerCard, TokenGeneratorContext _context)
        {
            try
            {
                var registrationDate = DateTime.Now;
                var token = TokenGeneration.GenerateToken(customerCard.CardNumber.ToString()[^4..], customerCard.CVV);

                customerCard.RegistrationDate = DateTime.Now;
                customerCard.Token = token;
                _context.Card.Add(customerCard);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is CardDTO)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                proposedValues[property] = proposedValue;
                            }

                            await _context.SaveChangesAsync();

                            entry.OriginalValues.SetValues(databaseValues);
                            //The new proposed value is saved but the overwriten value is subscribed to a queue to be checked 
                            _logger.LogWarning($"Concurrency values subscribed to {_configuration.GetValue<string>("TokenQueue")} from Customer {customerCard.CustomerID} ");
                            AsyncMessaging.Publish(_messageFactory,_configuration.GetValue<string>("TokenQueue"),
                                JsonSerializer.Serialize(new {Token = token,ProposedValues = proposedValues,DatabaseValues = databaseValues }));
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
                int id = (int)customerCard.Id;
                return new RegisterCardResponseDTO()
                {
                    CardId = id,
                    RegistrationDate = registrationDate,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the Token: {ex.Message}" );
            }
        }

        public bool ValidateToken()
        {
            throw new NotImplementedException();
        }
    }
}

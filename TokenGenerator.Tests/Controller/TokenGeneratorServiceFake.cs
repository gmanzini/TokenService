
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TokenGenerator.Controllers;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenGeneratorService;
using TokenGeneratorService.Domain;
using TokenGeneratorService.Services;
using TokenUtils;
using System.Linq;

namespace TokenGenerator.Tests.Controller
{
    class TokenGeneratorServiceFake : ITokenService
    {
        public CardDTO _card;
       
        public static List<CardDTO> cards = new List<CardDTO>();
        public TokenGeneratorServiceFake()
        {
            _card = new CardDTO();
            cards.Add(new CardDTO() { CardId = 1, CardNumber = 12345678, CustomerID = 1, CVV = 2, RegistrationDate = DateTime.Now, Token = 7856 });             //Correct Information
            cards.Add(new CardDTO() { CardId = 2, CardNumber = 12345678, CustomerID = 1, CVV = 2, RegistrationDate = DateTime.Now.AddDays(-1), Token = 7856 }); //Incorrect Information - Expired Date
            cards.Add(new CardDTO() { CardId = 3, CardNumber = 12345678, CustomerID = 1, CVV = 2, RegistrationDate = DateTime.Now, Token = 7856 });             //Incorrect Information - Different Owner
            cards.Add(new CardDTO() { CardId = 4, CardNumber = 12345678, CustomerID = 1, CVV = 3, RegistrationDate = DateTime.Now, Token = 7856 });             //Incorrect Information - Invalid Token


        }
        public RegisterCardResponseDTO SaveCard(CardDTO customerCard, TokenGeneratorContext _context)
        {
            _card.CardId = new Random().Next(int.MinValue, int.MaxValue);


            var token = TokenGeneration.GenerateToken(customerCard.CardNumber.ToString()[^4..], customerCard.CVV);
            _card = customerCard;
            _card.Token = token;
            _card.RegistrationDate = DateTime.Now;
            return new RegisterCardResponseDTO() { CardId = _card.CardId, RegistrationDate = DateTime.Now, Token = token };
        }

        public bool ValidateToken(TokenGeneratorContext _context, CardDTO customerCard)
        {
            CardDTO dbCard = cards.FirstOrDefault(w => w.CardId == customerCard.CardId);
            TimeSpan timeSpan = DateTime.Now - dbCard.RegistrationDate;
            if (timeSpan.TotalMinutes > 30)
            {
                return false;
            }
            if (customerCard.CustomerID != dbCard.CustomerID)
            {
                return false;
            }
            long token = TokenGeneration.GenerateToken(dbCard.CardNumber.ToString()[^4..], customerCard.CVV);
            if (dbCard.Token != token)
            {
                return false;
            }
            return true;
        }
    }
}


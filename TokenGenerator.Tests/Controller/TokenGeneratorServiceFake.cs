﻿
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

namespace TokenGenerator.Tests.Controller
{
    class TokenGeneratorServiceFake : ITokenService
    {
        private CardDTO _card;
        
        public TokenGeneratorServiceFake()
        {
            _card = new CardDTO() { CardId = 1, CardNumber = 12345678, CustomerID = 1, CVV = 2, RegistrationDate = DateTime.Now, Token = 123456 };
           
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


            TimeSpan timeSpan = DateTime.Now - _card.RegistrationDate;
            if (timeSpan.TotalMinutes > 30)
            {
                return false;
            }
            if (customerCard.CustomerID != _card.CustomerID)
            {
                return false;
            }
            long token = TokenGeneration.GenerateToken(_card.CardNumber.ToString()[^4..], _card.CVV);
            if (_card.Token != token)
            {
                return false;
            }
            return true;
        }
    }
}


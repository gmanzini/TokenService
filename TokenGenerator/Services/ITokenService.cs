using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenGenerator.Data;
using TokenGenerator.Model;
using TokenGeneratorService.Domain;

namespace TokenGeneratorService.Services
{
    public interface ITokenService
    {
        public Task<RegisterCardResponseDTO> SaveCard(CardDTO customerCard, TokenGeneratorContext _context);

        public bool ValidateToken(TokenGeneratorContext _context,CardDTO customerCard);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokenGenerator;
using TokenGeneratorService.Domain;

namespace TokenGenerator.Data
{
    public class TokenGeneratorContext : DbContext
    {
        public TokenGeneratorContext (DbContextOptions<TokenGeneratorContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerDTO> Customer { get; set; }
        public DbSet<CardDTO> Card { get; set; }
    }
}

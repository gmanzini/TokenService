using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TokenGeneratorService.Domain;

namespace TokenGenerator
{
    public sealed class CustomerDTO
    {
        [Key]
        public string CustomerID { get; set; }

        public CardDTO Card { get; set; }

    }
}

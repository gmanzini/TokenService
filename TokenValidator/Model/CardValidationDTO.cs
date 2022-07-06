using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenValidatorService
{
    public class CardValidationDTO
    {
        public int CustomerId { get; set; }

        public int CardId { get; set; }

        public long Token { get; set; }
        [MaxLength(5)]
        public int CVV { get; set; }
        
    }
}

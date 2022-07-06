using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenGeneratorService.Domain
{
    public class CardDTO
    {
        [Key]
        public long CardNumber { get; set; }
        [MaxLength(5)]
        public int CVV { get; set; }

        public long Token { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int CustomerID { get; set; } 
    }
}

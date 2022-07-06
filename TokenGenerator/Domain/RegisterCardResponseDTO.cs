using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenGenerator.Model
{
    public sealed class RegisterCardResponseDTO
    {
        public DateTime RegistrationDate { get; set; }

        public long Token { get; set; }
        [Key]
        public int CardId { get; set; }
      
    }
}

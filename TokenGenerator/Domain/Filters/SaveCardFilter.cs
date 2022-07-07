using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenGeneratorService.Domain.Filters
{
    public class SaveCardFilter
    {

        public long CardNumber { get; set; }
        [Range(1, 99999, ErrorMessage = "Value must be between 1 to 99999")]
        public int CVV { get; set; }

        public int CustomerID { get; set; }

    }
}

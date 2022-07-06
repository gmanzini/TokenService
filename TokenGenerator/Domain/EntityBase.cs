using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenGeneratorService.Domain
{
    public class EntityBase
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

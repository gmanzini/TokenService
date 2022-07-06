﻿using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TokenGeneratorService.Domain
{
    public class CardDTO 
    {
        
        public long CardNumber { get; set; }
        [Range(1, 99999, ErrorMessage = "Value must be between 1 to 99999")]
        public int CVV { get; set; }
        [IgnoreDataMember]
        public long Token { get; set; }
        [IgnoreDataMember]
        public DateTime RegistrationDate { get; set; }

        public int CustomerID { get; set; }
        [IgnoreDataMember]
        [Key]
        public int Id { get; set; }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenGeneratorService.Domain;
using TokenGeneratorService.Domain.Filters;

namespace TokenGeneratorService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ValidateTokenFilter, CardDTO>();
            CreateMap<SaveCardFilter, CardDTO>();
        }
    }
}

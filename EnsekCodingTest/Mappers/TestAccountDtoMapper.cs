using AutoMapper;
using EnsekCodingTest.Models;
using EnsekCodingTest.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Mappers
{
    public class TestAccountDtoMapper : Profile
    {
        public TestAccountDtoMapper()
        {
            CreateMap<TestAccount, TestAccountDto>().ReverseMap();
        }
    }
}

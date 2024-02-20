using CsvHelper.Configuration;
using EnsekCodingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Mappers
{
    public class TestAccountMappings: ClassMap<TestAccount>
    {
        public TestAccountMappings()
        {
            Map(m => m.AccountId).Name("AccountId");
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.LastName).Name("LastName");
        }
    }
}

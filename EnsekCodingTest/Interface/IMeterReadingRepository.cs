using EnsekCodingTest.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Interface
{
    public interface IMeterReadingRepository
    {
        string[] CheckMeterReadings(IFormFile uploadFile);
        List<TestAccount> GetTestAccountDeatils();
        List<MeterReading> GetAccount(int accountId);
    }
}

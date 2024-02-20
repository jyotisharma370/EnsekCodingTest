using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Models
{
    public class EnsekDbContext: DbContext
    {
        public EnsekDbContext(DbContextOptions<EnsekDbContext> options) : base(options)
        {

        }

        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<TestAccount> TestAccounts { get; set; }
    }
}

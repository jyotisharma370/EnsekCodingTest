using CsvHelper;
using EnsekCodingTest.Interface;
using EnsekCodingTest.Mappers;
using EnsekCodingTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Data
{
    public class DbInitialiser: IDbInitialiser
    {
        private readonly EnsekDbContext _context;

        public DbInitialiser(EnsekDbContext context)
        {
            _context = context;
        }

        public void LoadTestCsvToDb()
        {
            if (!_context.TestAccounts.Any())
            {
                var csvTable = new DataTable();
                using (var streamReader = new StreamReader(@"CSVFiles\Test_Accounts.csv"))
                {
                    using (var csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.CurrentCulture, true))
                    {
                        csvReader.Context.RegisterClassMap<TestAccountMappings>();
                        var records = csvReader.GetRecords<TestAccount>().ToList();

                        List<TestAccount> obj = records;
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            obj.ForEach(x => _context.TestAccounts.Add(x));

                            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.TestAccounts ON;");
                            _context.SaveChanges();
                            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.TestAccounts OFF;");
                            transaction.Commit();
                        }
                    }

                }
            }
        }
    }
}
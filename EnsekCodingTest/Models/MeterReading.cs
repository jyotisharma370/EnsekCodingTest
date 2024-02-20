using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Models
{
    public class MeterReading
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }

        [ForeignKey("AccountId")]
        public TestAccount TestAccount { get; set; }
    }
}

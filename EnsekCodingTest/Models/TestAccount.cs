using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekCodingTest.Models
{
    public class TestAccount
    {
        [Key]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public TestAccount()
        {

        }

        public TestAccount(int accountId, string fname, string lname)
        {
            this.AccountId = accountId;
            this.FirstName = fname;
            this.LastName = lname;
        }
    }
}

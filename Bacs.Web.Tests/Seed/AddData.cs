using ENSEK.Data;
using ENSEK.Data.Seed;
using ENSEK.Models;
using ENSEK.Services.Constants;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Web.Tests.Seed
{
    [TestFixture(Category ="SeedData")]
    public class SeedTests: BaseTest
    {
        ENSEKContext context;
        [SetUp]
        public void SetUp()
        {
            context = SetUpContext(true);
        }
        [Test]
        public void AddData()
        {
            List<Account> result = null;
            
            Assert.DoesNotThrow(()=>result = AccountsSeed.SeedAccounts(Constants.AccountsSeedFile, context));
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}

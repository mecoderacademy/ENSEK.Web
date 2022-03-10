

using ENSEK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ENSEK.Data.Repository
{
    public class AccountRepository : GenericRepository<Account>
    {
        public AccountRepository(ENSEKContext context) : base(context)
        {
        }
    }
}


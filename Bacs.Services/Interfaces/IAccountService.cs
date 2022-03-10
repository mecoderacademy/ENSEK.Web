
using ENSEK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();


        Account GetByID(int Id);


        void Insert(Account Entity);

        Account GetByAccountId(int AccountId);
        
    }
}

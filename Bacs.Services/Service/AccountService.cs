using ENSEK.Data.Repository;
using ENSEK.Models;
using ENSEK.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Services.Service
{
    public class AccountService: IAccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMeterReadingService _meterReadingService;
        public AccountService(AccountRepository accountRepository, IMeterReadingService meterReadingService)
        {
            _accountRepository = accountRepository;
            _meterReadingService = meterReadingService;
        }

        public IEnumerable<Account> GetAll()
        {
           return _accountRepository.GetAll();
        }

        public Account GetByID(int Id)
        {
            return _accountRepository.GetByID(Id);
        }
        public Account GetByAccountId(int AccountId)
        {
            return _accountRepository.GetAll(x => x.AccountId == AccountId).FirstOrDefault();
        }
        public void Insert(Account account)
        {
            account.MeterReadings.ToList().ForEach(transaction =>
            {
                _meterReadingService.Insert(transaction);
            });
            _accountRepository.Insert(account);
            _accountRepository.Save();
        }

    }
}

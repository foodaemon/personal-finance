using System;
using System.Collections.Generic;
using Finance.Common.Helpers;
using Finance.Data.Repositories;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Core.Services
{
    public class AccountService : IAccountService
    {
        readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {   
            _accountRepository = accountRepository;
        }

        public void CreateAccount(string email, string password)
        {
            var salt = SecurityHelper.GenerateSalt();
            var passwordHash = SecurityHelper.GenerateHash(password, salt);
            var account = new Account
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow,
                GroupId = Guid.NewGuid().ToString(),
            };
            _accountRepository.Insert(account);
        }

        private void FindByDescription(string search)
        {
            // var transaction = _repo.Table.Where(x => Regex.IsMatch(x.Description, "^search"));
        }

        public IEnumerable<Account> GetAll()
        {
            var result = _accountRepository.GetAll();
            return result;
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByEmail(string email)
        {
            var account = await _accountRepository.GetByEmail(email.ToLower());
            return account;
        }

        public void UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidLogin(string email, string password)
        {
            var account = await GetByEmail(email);
            if (account == null)
                return false;

            var hashedPassword = SecurityHelper.GenerateHash(password, account.PasswordSalt);
            return (hashedPassword == account.PasswordHash);
        }
    } // class
} // namespace 

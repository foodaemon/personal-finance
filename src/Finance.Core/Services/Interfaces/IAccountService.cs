using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Core.Services
{
    public interface IAccountService
    {
        void CreateAccount(string email, string password);
        
        IEnumerable<Account> GetAll();
        
        void GetById(int id);
        
        Task<Account> GetByEmail(string email);
        
        void UpdateAccount(Account account);
        
        Task<bool> IsValidLogin(string email, string password);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Data.Repositories
{
    public interface IAccountRepository
    {
		IEnumerable<Account> GetAll();
		Task<Account> GetById(string id);
        Task<Account> GetByEmail(string email);
		void Insert(Account account);
		bool Delete(string id);
		void Update(Account account);
    }
}

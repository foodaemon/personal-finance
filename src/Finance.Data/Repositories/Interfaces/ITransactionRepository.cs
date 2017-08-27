using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Data.Repositories
{
    public interface ITransactionRepository
    {
		IEnumerable<Transaction> GetAll();
        Task<IEnumerable<Transaction>> GetByDate(DateTime startDate, DateTime endDate);
		Task<Transaction> GetById(string id);
		void Insert(Transaction transaction);
		Task Delete(string id);
		void Update(Transaction transaction);
    }
}

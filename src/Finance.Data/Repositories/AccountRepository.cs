using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using Finance.Common.Entities;

namespace Finance.Data.Repositories
{
    public class AccountRepository: IAccountRepository
    {
		const string CollectionName = @"accounts";

		readonly IMongoDatabase _db;

        public AccountRepository()
        {
            _db = DbHelper.GetConnection();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetById(string id)
        {
            throw new NotImplementedException();
        }

		public async Task<Account> GetByEmail(string email)
		{
			var query = Builders<Account>.Filter.Eq(e => e.Email, email);

			var account = await _db.GetCollection<Account>(CollectionName)
									.Find(query).ToListAsync();

			return account.FirstOrDefault();
		}

        public void Insert(Account account)
        {
			_db.GetCollection<Account>(CollectionName)
					 .InsertOneAsync(account);
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}

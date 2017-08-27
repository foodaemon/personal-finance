using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Finance.Common.Entities;

namespace Finance.Data.Repositories
{
    public class TransactionRepository: ITransactionRepository
    {
		const string CollectionName = @"transactions";

		readonly IMongoDatabase _db;

        public TransactionRepository() 
        {
            _db = DbHelper.GetConnection();
        }

		public TransactionRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task Delete(string id)
        {
            var query = Builders<Transaction>.Filter.Eq(e => e.Id, id);
            var result = await _db.GetCollection<Transaction>(CollectionName)
                .DeleteOneAsync(query);
        }

        public IEnumerable<Transaction> GetAll()
        {
			var results = _db.GetCollection<Transaction>(CollectionName)
									  .Find(new BsonDocument()).ToListAsync();
			return results.Result;
        }

        public async Task<IEnumerable<Transaction>> GetByDate(DateTime startDate, DateTime endDate) 
        {
            var collection = _db.GetCollection<BsonDocument>(CollectionName);
            var filterBuilder = Builders<BsonDocument>.Filter;
			var filter = filterBuilder.Gte("date", startDate) & filterBuilder.Lte("date", endDate);

            var projectJson = @"{'date': 1, 'amount': 1, 'description': 1, 
                'is_income': 1, 'category_id': 1, 
                'category': { '$arrayElemAt': [ '$category', 0 ]}}";
            var projectDoc = BsonSerializer.Deserialize<BsonDocument>(projectJson);
			//db.transactions.aggregate(
			//	[

			//		{ "$lookup": {
			//					"from": "categories",     
			//                "localField": "category",     
			//                "foreignField": "_id",     
			//                "as": "category"

			//		}
			//			}, 
			//        {
			//				"$project": {
			//					"date": 1, 
			//            "amount": 1, 
			//            "category": { "$arrayElemAt": [ "$category", 0 ]
			//	}
			//}} 
			//    ]
			//)
            var pipeline = await collection.Aggregate()
                                           .Match(filter)
                                           .Lookup("categories", "category_id", "_id", "category")
                                           .Project(projectDoc)
                                           .ToListAsync();
            
            var results = BsonSerializer.Deserialize<IEnumerable<Transaction>>(pipeline.ToJson());
            return results;
        }

        public async Task<Transaction> GetById(string id)
        {
            var filter = Builders<Transaction>.Filter.Eq(e => e.Id, id);
            var transaction = await _db.GetCollection<Transaction>(CollectionName)
                .Find(filter).ToListAsync();
            return transaction.SingleOrDefault();
        }

        public void Insert(Transaction transaction)
        {
			_db.GetCollection<Transaction>(CollectionName)
					 .InsertOneAsync(transaction);
        }

        public void Update(Transaction transaction)
        {
            var query = Builders<Transaction>.Filter.Eq(e => e.Id, transaction.Id);
            var update = _db.GetCollection<Transaction>(CollectionName)
                .ReplaceOneAsync(query, transaction);
        }
    }
}

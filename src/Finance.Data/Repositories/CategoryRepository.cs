using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Finance.Common.Entities;

namespace Finance.Data.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        const string CollectionName = @"categories";

        readonly IMongoDatabase _db;
        readonly MongoContext _dataContext;


        public CategoryRepository()
        {
            _dataContext = new MongoContext();
            _db = DbHelper.GetConnection();
        }

        public async Task Add(Category category) 
        {   
            await _db.GetCollection<Category>(CollectionName)
                     .InsertOneAsync(category);
        }

        public IEnumerable<Category> GetAll() 
        {
            var categories = _db.GetCollection<Category>(CollectionName)
                                      .Find(new BsonDocument()).ToListAsync();
            return categories.Result;
        }

		public async Task<Category> GetById(string id)
		{
			var query = Builders<Category>.Filter.Eq(e => e.Id, id);
			var category = await _db.GetCollection<Category>(CollectionName)
                                    .Find(query).ToListAsync();

			return category.FirstOrDefault();
		}

		public bool Delete(string id)
		{
			var query = Builders<Category>.Filter.Eq(e => e.Id, id);
			var result = _db.GetCollection<Category>(CollectionName)
                                  .DeleteOneAsync(query);

			return GetById(id) == null;
		}

		public void Update(Category category)
		{
			var query = Builders<Category>.Filter.Eq(e => e.Id, category.Id);
			var update = _db.GetCollection<Category>(CollectionName)
                                  .ReplaceOneAsync(query, category);
		}
	}
}

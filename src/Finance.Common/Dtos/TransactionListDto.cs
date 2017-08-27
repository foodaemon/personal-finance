using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Finance.Common.Entities;

namespace Finance.Common.Dtos
{
    public class TransactionListDto
    {
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonIgnoreIfDefault]
		public string Id { get; set; }

		[BsonElement("date")]
		public DateTime Date { get; set; }

		[BsonElement("description")]
		public string Description { get; set; }

		[BsonElement("amount")]
		public double Amount { get; set; }

		[BsonElement("is_income")]
		public bool IsIncome { get; set; }

		[BsonElement("category_id")]
		public bool CategoryId { get; set; }

		[BsonElement("category")]
		public Category Category { get; set; }

    }
}

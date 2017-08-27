using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.Common.Entities
{
    [BsonIgnoreExtraElements]
    public class Transaction
    {
        [BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonIgnoreIfDefault]
        public string Id { get; set; }
		
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("description")]
		public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("category_id")]
		public string CategoryId { get; set; }

        [BsonElement("amount")]
		public double Amount { get; set; }

        [BsonElement("is_income")]
		public bool IsIncome { get; set; }

        [BsonElement("comments")]
		public string Comments { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("created_by")]
        public string CreatedBy { get; set; }

        [BsonElement("created_at")]
		public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
		public DateTime UpdatedAt { get; set; }

		[BsonElement("group_id")]
		public string GroupId { get; set; }

        [BsonElement("category")]
        public Category Category { get; set; }
    }
}

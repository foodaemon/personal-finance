using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.Common.Entities
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        [BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("group_id")]
        public string GroupId { get; set; }

		[BsonElement("created_by")]
		public string CreatedBy { get; set; }

		[BsonElement("created_at")]
		public DateTime CreatedAt { get; set; }

		[BsonElement("updated_at")]
		public DateTime UpdatedAt { get; set; }
    }
}

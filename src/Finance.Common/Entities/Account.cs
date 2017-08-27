using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.Common.Entities
{
    public class Account
    {
        [BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		[BsonIgnoreIfDefault]
        public string Id { get; set; }

		[BsonElement("firstname")]
		public string FirstName { get; set; }

		[BsonElement("lastname")]
		public string LastName { get; set; }

        [BsonElement("email")]
		public string Email { get; set; }

        [BsonElement("password_hash")]
		public string PasswordHash { get; set; }

        [BsonElement("password_salt")]
		public string PasswordSalt { get; set; }

        [BsonElement("is_locked")]
		public bool IsLocked { get; set; }

        [BsonElement("role")]
		public AccountRole Role { get; set; }

        [BsonElement("lastlogin_at")]
		public DateTime? LastLoginAt { get; set; }

        [BsonElement("lastlogin_ip")]
		public string LastLoginIP { get; set; }

        [BsonElement("created_at")]
		public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
		public DateTime UpdatedAt { get; set; }

		[BsonElement("group_id")]
		public string GroupId { get; set; }
    }
}

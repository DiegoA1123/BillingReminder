using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BillingReminder.Domain.Entities
{
    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; } = default!;

        [BsonElement("Id")]
        public string Id { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string ClientEmail { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

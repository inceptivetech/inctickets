using MongoDB.Bson.Serialization.Attributes;

namespace gamerszone.Data
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonRequired]
        public string TicketName { get; set; }
        [BsonRequired]
        public string? TicketDescription { get; set; }

        [BsonRequired]
        public string? Severity { get; set; }
        [BsonRequired]

        public string? Project { get; set; }
        [BsonRequired]
        public string? Category { get; set; }
        [BsonRequired]
        public string PostedBy { get; set; }

        public string[] Attach1 { get; set; }
        public string? Attach2 { get; set; }

        public bool active { get; set; } = false;
    }
}

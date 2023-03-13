using MongoDB.Bson.Serialization.Attributes;

namespace gamerszone.Data
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonRequired]
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public bool active { get; set; } = false;
    }
}

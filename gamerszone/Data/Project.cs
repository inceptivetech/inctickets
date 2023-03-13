using MongoDB.Bson.Serialization.Attributes;

namespace gamerszone.Data
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonRequired]
        public string ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public bool active { get; set; } = false;
    }
}

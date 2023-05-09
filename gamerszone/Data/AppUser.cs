using MongoDB.Bson.Serialization.Attributes;

namespace gamerszone.Data
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonRequired]
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; }    
        public string EmailVerified { get; set; }
        public string Role { get; set; }
        public bool active { get; set; } = false;

    }

   
}

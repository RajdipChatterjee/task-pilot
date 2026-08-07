using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using todo_backend.Enums;

namespace todo_backend.Models
{
    public class Todo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("title")]
        public string Title { get; set; } = String.Empty;

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("status")]
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
    }
}
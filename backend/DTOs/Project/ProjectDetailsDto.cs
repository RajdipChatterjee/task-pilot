using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskPilot.Api.DTOs.Project;

public class ProjectDetailsDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("taskCount")]
    public int TaskCount { get; set; }

    [BsonElement("createdBy")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
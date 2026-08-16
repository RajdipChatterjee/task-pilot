using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskPilot.Api.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("username")]
    public string Username { get; set; } = null!;
    [BsonElement("email")]
    public string Email { get; set; } = null!;
    [BsonElement("passwordHash")]
    public string? PasswordHash { get; set; }
    [BsonElement("refreshTokens")]
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    [BsonElement("externalLogins")]
    public List<ExternalLogin> ExternalLogins { get; set; } = [];
}
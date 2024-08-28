using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoAppApi.Documents;

public class TodoListDocument
{
    public string Id { get; set; }

    public string Title { get; set; }

    public float Rank { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset Modified { get; set; } = DateTimeOffset.UtcNow;
}
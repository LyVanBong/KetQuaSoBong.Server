using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Supports;

public class Support
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserName { get; set; }
    public string NumberPhone { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public string DateCreate { get; set; } = DateTimeOffset.Now.ToString("G");
}
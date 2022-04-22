using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Supports;

public class Contact
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; }
    public string Icon { get; set; }
    public string ContactInfo { get; set; }
}
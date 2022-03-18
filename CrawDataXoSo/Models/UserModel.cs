using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawDataXoSo.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string UserName { get; set; }

    public string Password { get; set; }
}
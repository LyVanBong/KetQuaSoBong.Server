using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawDataXoSo.Models;

public class KqxsMbModel : KqxsModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string NgayQuay { get; set; }
}
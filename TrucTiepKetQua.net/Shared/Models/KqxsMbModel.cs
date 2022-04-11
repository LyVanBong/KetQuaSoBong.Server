using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrucTiepKetQua.net.Shared.Models;

public class KqxsMbModel : KqxsModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string NgayQuay { get; set; }
}
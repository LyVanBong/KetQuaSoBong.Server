using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrucTiepKetQua.net.Shared.Models;

public class KqxsMbModel : KqxsModel
{
    [BsonElement("_id")]
    public string NgayQuay { get; set; }
}
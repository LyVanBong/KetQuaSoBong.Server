using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class KqxsMbModel : KqxsModel
{
    [BsonElement("_id")]
    public string NgayQuay { get; set; }
}
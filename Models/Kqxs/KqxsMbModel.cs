using MongoDB.Bson.Serialization.Attributes;

namespace Models.Kqxs;

public class KqxsMbModel : KqxsModel
{
    [BsonElement("_id")]
    public string NgayQuay { get; set; }
}
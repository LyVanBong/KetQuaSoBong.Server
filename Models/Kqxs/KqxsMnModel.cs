using MongoDB.Bson.Serialization.Attributes;

namespace Models.Kqxs;

public class KqxsMnModel
{
    [BsonElement("_id")]
    public string NgayQuay { get; set; }
    public List<KqxsModel> Datas { get; set; } = new List<KqxsModel>();
}
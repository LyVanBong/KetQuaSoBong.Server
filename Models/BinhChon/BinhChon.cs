using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.BinhChon;

public class BinhChon
{
    [BsonElement("_id")]
    public string Number { get; set; }
    public int SoLuongBinhChon { get; set; }
}
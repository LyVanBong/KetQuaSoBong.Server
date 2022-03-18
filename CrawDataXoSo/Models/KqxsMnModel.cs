﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrawDataXoSo.Models;

public class KqxsMnModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string NgayQuay { get; set; }
    public List<KqxsModel> Datas { get; set; } = new List<KqxsModel>();
}
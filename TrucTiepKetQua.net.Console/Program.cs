// See https://aka.ms/new-console-template for more information

using MongoDB.Driver;
using TrucTiepKetQua.net.Console;

Console.WriteLine("Hello, World!");

var mongo = new MongoClient(AppConstants.ConnectionStringMongoDb);
var database = mongo.GetDatabase("Kqxs");
var collection = database.GetCollection<KqxsMnModel>("KqxsMn");

var countItem = await collection.CountDocumentsAsync(model => model.Id != null);

var item = collection.Find(model => model.Id != null);

Console.WriteLine();
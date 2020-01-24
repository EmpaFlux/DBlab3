using System;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Lab3DB
{
    class Program
    {
        static void Main(string[] args)
        {
            var updateBuilder = Builders<BsonDocument>.Update;
            var filterBuilder = Builders<BsonDocument>.Filter;
            var projectionBuilder = Builders<BsonDocument>.Projection;
            var mongoCRUD = new MongoCRUD("Restaurants");
            // mongoCRUD.InsertRecord(mongoCRUD.GetRestaurants());
            var collection = mongoCRUD.GetMongoCollection();

            //Skriv ut alla dokument
            PrintResult(collection.Find(new BsonDocument()).ToList());

            //Exkludera _id
            var projection = projectionBuilder.Exclude("_id");

            //Sök på cafe
            var filter = filterBuilder.Eq("categories", "Cafe");
            PrintResult(collection.Find(filter).Project(projection).ToList());
            
            //Öka stars för "XYZ Coffee Bar" till 6 stars
            filter = filterBuilder.Eq("name", "XYZ Coffee Bar");
            var update = updateBuilder.Inc("stars", 1);
            collection.FindOneAndUpdate(filter, update);
            PrintResult(collection.Find(new BsonDocument()).Project(projection).ToList());

            //Byt namn på "456 Cookies Shop" till "123 Cookies Heaven"
            filter = filterBuilder.Eq("name", "456 Cookies Shop");
            update = updateBuilder.Set("name", "123 Cookies Heaven");
            collection.FindOneAndUpdate(filter, update);
            filter = filterBuilder.Eq("name", "123 Cookies Heaven");
            PrintResult(collection.Find(filter).Project(projection).ToList());

            //Skriv ut alla restauranger med 4+ stars. Endast names och stars ska skrivas ut.
            filter = filterBuilder.Eq("stars", 4) | filterBuilder.Gt("stars", 4);
            projection = projectionBuilder.Exclude("_id").Exclude("categories");
            PrintResult(collection.Find(filter).Project(projection).ToList());

        }
        public static void PrintResult(List<BsonDocument> result)
        {
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord(List<BsonDocument> restaurants)
        {
            var collection = db.GetCollection<BsonDocument>("Restaurants");
            collection.InsertMany(restaurants);
        }
        public List<BsonDocument> GenerateRestaurants()
        {
            var list = new List<BsonDocument>()
            {
                new BsonDocument{ { "name", "Sun Bakery Trattoria" }, { "stars", 4 }, {"categories", new BsonArray{"Pizza", "Pasta", "Italian", "Coffee", "Sandwiches"} } },
                new BsonDocument{ { "name", "Blue Bagels Grill" }, { "stars", 3 }, {"categories", new BsonArray{ "Bagels", "Cookies", "Sandwiches" } } },
                new BsonDocument{ { "name", "Hot Bakery Cafe" }, { "stars", 4 }, {"categories", new BsonArray{ "Bakery", "Cafe", "Coffee", "Dessert" } } },
                new BsonDocument{ { "name", "XYZ Coffee Bar" }, { "stars", 5 }, {"categories", new BsonArray{ "Coffee", "Cafe", "Bakery", "Chocolates" } } },
                new BsonDocument{ { "name", "456 Cookies Shop" }, { "stars", 4 }, {"categories", new BsonArray{"Bakery", "Cookies", "Cake", "Coffee"} } }
            };
            return list;
        }

        public IMongoCollection<BsonDocument> GetMongoCollection()
        {
            var collection = db.GetCollection<BsonDocument>("Restaurants");
            return collection;
        }
       
        public static void PrintResult(List<BsonDocument> result)
        {
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }

        
    }
    
}

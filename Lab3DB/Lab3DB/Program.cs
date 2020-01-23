using System;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using Lab3DB.Models;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace Lab3DB
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoCRUD = new MongoCRUD("Restaurants");
            mongoCRUD.InsertRecord(mongoCRUD.GetRestaurants());
            var collection = mongoCRUD.ReadRestaurants();
           
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
        public List<BsonDocument> GetRestaurants()
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

        public List<BsonDocument> ReadRestaurants()
        {
            var collection = db.GetCollection<BsonDocument>("Restaurants");
            return collection.Find(new BsonDocument()).ToList();
        }


        public static void WriteDocuments(List<BsonDocument> restaurants)
        {
            foreach (var item in restaurants)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
    
}

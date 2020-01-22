using System;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using Lab3DB.Models;

namespace Lab3DB
{
    class Program
    {
        static void Main(string[] args)
        {
            
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

        public void InsertRecord(List<Restaurant> restaurants)
        {
            var collection = db.GetCollection<Restaurant>("Restaurants");
            foreach (var restaurant in restaurants)
            {
                collection.InsertOne(restaurant);
            }
        }
    }
}

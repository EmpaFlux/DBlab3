using System;
using MongoDB.Driver;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

namespace OfficialDriver
{
    class Program
    {

        private const string conStr = "mongodb://127.0.0.1:27017";
        private const string dbName = "TestData";
        private const string dbCollection = "myclass";


        static void Main(string[] args)
        {
            MongoClientSettings mcs = new MongoClientSettings();

            var client = new MongoClient(conStr);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<BsonDocument>(dbCollection);






            //Create();
        }

        private static void Create()
        {

            #region 创建Client、Server、Database

            //MongoServerAddress.Parse(conStr);
            var client = new MongoClient(conStr);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<BsonDocument>("myclass");

            var document = new BsonDocument{
                {"name","MongoDB"},
                {"type","Database"},
                {"count",1},
                {"info",new BsonDocument{
                    {"x",203},
                    {"y",102}   
                }}
            };




            var entity = new People { Name = "Tom" };



            //collection.InseryrtOne(entity);

            #region official git explain

            //Task await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

            //var list =   await collection.Find(new BsonDocument("Name", "Jack")).ToListAsync();

            //foreach (var document in list)
            //{
            //    Console.WriteLine(document["Name"]);
            //}
            #endregion

            #endregion
        }

        private static void Insert()
        {

        }

        public class People
        {
            public ObjectId Id { get; set; }
            public string Name { get; set; }
        }

    }
}

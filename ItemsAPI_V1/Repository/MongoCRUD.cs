using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI_V1
{
    public class MongoCRUD : IMongoCRUD
    {
        public string DatabaseName { get; set; }
        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }
        //MongoDBConfiguration config
        public MongoCRUD(MongoDBConfiguration config)
        {
            this.DatabaseName = config.Database;
            Client = new MongoClient(config.ConnectionString);
            Database = Client.GetDatabase(config.Database);
        }
        public MongoCRUD(string connectionstring, string DatabaseName)
        {
            this.DatabaseName = DatabaseName;
            //Environment.SetEnvironmentVariable("ConnectionString", "mongodb://root:root@db:27017/?authSource=admin&readPreference=primary&appname=test&ssl=false");

            Client = new MongoClient(connectionstring);
            //Client = new MongoClient(Environment.GetEnvironmentVariable("mongodb://root:root@db:27017/?authSource=admin&readPreference=primary&appname=test&ssl=false"));
            Database = Client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }


        public IAsyncCursor<BsonDocument> GetCollections<T>()
        {
            return Database.ListCollections();
        }

        public void CreateCollection(string collectionName)
        {
            Database.CreateCollection(collectionName);

        }

        public void DeleteCollection<T>(string collectionName)
        {
            Database.DropCollection(collectionName);

            if (Database.GetCollection<T>(collectionName) != null)
                Console.WriteLine("{0} collection exists", collectionName);
            else
                Console.WriteLine("{0} collection was not found", collectionName);
        }


        public void InsertOneDocument<T>(string table, T record)
        {
            var collection = this.Database.GetCollection<T>(table);
            collection.InsertOne(record);
        }
        public void InsertManyDocuments<T>(string table, Collection<T> records)
        {
            var collection = this.Database.GetCollection<T>(table);
            collection.InsertMany(records);
        }
        public List<T> GetDocuments<T>(string table)
        {
            var collection = this.Database.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
        public T GetDocumentById<T>(string table, Guid id)
        {
            var collection = this.Database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }
        public T GetDocumentByName<T>(string table, string name)
        {
            var collection = this.Database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Name", name);

            return collection.Find(filter).First();
        }
        public T GetDocumentByFilter<T>(string table, FilterDefinition<T> filter)
        {
            var collection = this.Database.GetCollection<T>(table);
             return collection.Find(filter).FirstOrDefault();
        }
        public List<T> GetDocumentsByFilter<T>(string table, FilterDefinition<T> filter)
        {
            var collection = this.Database.GetCollection<T>(table);


            return collection.Find(filter).ToList<T>();
        }
        public void UpSertDocument<T>(string table, Guid id, T record)
        {
            var collection = this.Database.GetCollection<T>(table);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true }
                                 );
        }

        public void DeleteDocumentById<T>(string table, Guid id)
        {
            var collection = this.Database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
        public void DeleteDocumentByFilter<T>(string table, FilterDefinition<T> filter)
        {
            var collection = this.Database.GetCollection<T>(table);

            collection.DeleteOne(filter);
        }
        //public void DeleteDocumentsByFilter<T>(string table, FilterDefinition<T> filter)
        //{
        //    var collection = this.Database.GetCollection<T>(table);

        //    collection.DeleteMany<T>(filter,);
        //}
    }
}

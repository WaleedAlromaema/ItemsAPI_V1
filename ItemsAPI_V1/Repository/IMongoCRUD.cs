using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ItemsAPI_V1
{
    public interface IMongoCRUD
    {
        IMongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
        string DatabaseName { get; set; }

        void CreateCollection(string collectionName);
        void DeleteCollection<T>(string collectionName);
        void DeleteDocumentByFilter<T>(string table, FilterDefinition<T> filter);
        void DeleteDocumentById<T>(string table, Guid id);
        IMongoCollection<T> GetCollection<T>(string collectionName);
        IAsyncCursor<BsonDocument> GetCollections<T>();
        T GetDocumentByFilter<T>(string table, FilterDefinition<T> filter);
        T GetDocumentById<T>(string table, Guid id);
        T GetDocumentByName<T>(string table, string name);
        List<T> GetDocuments<T>(string table);
        List<T> GetDocumentsByFilter<T>(string table, FilterDefinition<T> filter);
        void InsertManyDocuments<T>(string table, Collection<T> records);
        void InsertOneDocument<T>(string table, T record);
        void UpSertDocument<T>(string table, Guid id, T record);
    }
}
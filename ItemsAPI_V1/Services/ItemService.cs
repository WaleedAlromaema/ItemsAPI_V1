using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI_V1
{
    public class ItemService : IItemService
    {
        public MongoCRUD CRUD { get; set; }
        public ItemService(string stringConnection, string databaseName)
        {
            CRUD = new MongoCRUD(stringConnection, databaseName);

        }
        public void DeleteItem(string collectionName, Item item)
        {
            CRUD.DeleteDocumentById<Item>(collectionName, item.Id);
        }

        public List<Item> GetAllItems(string collectionName)
        {
            return CRUD.GetDocuments<Item>(collectionName);
        }

        public Item GetItemById(string collectionName, Guid id)
        {
            return CRUD.GetDocumentById<Item>(collectionName, id);
        }

        public Item GetItemByType(string collectionName, string type)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Type", type);
            return CRUD.GetDocumentByFilter<Item>(collectionName, filter);
        }
        public List<Item> GetItemsByType(string collectionName, string type)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Type", type);
            return CRUD.GetDocumentsByFilter<Item>(collectionName, filter);
        }

        public Item GetItemByNewURL(string collectionName, string newUrl)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("NewURL", newUrl);
            return CRUD.GetDocumentByFilter<Item>(collectionName, filter);
        }

        public void UpdateItem(string collectionName, Item item)
        {
            CRUD.UpSertDocument<Item>(collectionName, item.Id, item);
        }

        public void UpdateItem(string collectionName, Guid id, Item item)
        {
            CRUD.UpSertDocument<Item>(collectionName, item.Id, item);
        }

        public void InsertItem(string collectionName, Item item)
        {
            CRUD.InsertOneDocument<Item>(collectionName, item);
        }

        public void DeleteItemById(string collectionName, Guid id)
        {
            CRUD.DeleteDocumentById<Item>(collectionName, id);
        }

        public void DeleteItemByType(string collectionName, string type)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Type", type);
            CRUD.DeleteDocumentByFilter<Item>(collectionName, filter);
        }


    }
}

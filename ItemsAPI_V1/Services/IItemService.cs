using System;
using System.Collections.Generic;

namespace ItemsAPI_V1
{
    public interface IItemService
    {
        MongoCRUD CRUD { get; set; }

        void DeleteItem(string collectionName, Item item);
        void DeleteItemById(string collectionName, Guid id);
        void DeleteItemByType(string collectionName, string type);
        List<Item> GetAllItems(string collectionName);
        Item GetItemById(string collectionName, Guid id);
        Item GetItemByNewURL(string collectionName, string newUrl);
        Item GetItemByType(string collectionName, string type);
        List<Item> GetItemsByType(string collectionName, string type);
        void InsertItem(string collectionName, Item item);
        void UpdateItem(string collectionName, Guid id, Item item);
        void UpdateItem(string collectionName, Item item);
    }
}
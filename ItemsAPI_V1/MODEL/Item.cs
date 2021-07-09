using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI_V1
{
    [BsonIgnoreExtraElements]
    public class Item
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Type { get; set; }
        public bool HasToChange { get; set; }
        public string NewURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Item()
        {
            Type = "";
            HasToChange = false;
            NewURL = "";
            Id = Guid.NewGuid();
            StartDate = DateTime.Now;
            EndDate = new DateTime(2021, 12, 31);
        }
    }
}

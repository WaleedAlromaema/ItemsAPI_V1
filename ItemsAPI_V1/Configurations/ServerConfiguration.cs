using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI_V1
{
    public class ServerConfiguration
    {
        public MongoDBConfiguration MongoDB { get; set; } = new MongoDBConfiguration();
    }
}

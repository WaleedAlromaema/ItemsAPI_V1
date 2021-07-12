using System;

namespace ItemsAPI_V1
{
    public class MongoDBConfiguration
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
       
        public string ConnectionString
        {
            get
            {
                //ongodb://root:{PASSWORDHERE}@db:27017/?authSource=admin&readPreference=primary&appname=test&ssl=false
                string UserEnvVar = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME");
                string PasswordEnvVar = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD");
                if (string.IsNullOrEmpty(UserEnvVar) || string.IsNullOrEmpty(PasswordEnvVar))
                {

                    if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    {
                        return $@"mongodb://{Host}:{Port}";
                    }
                    else
                        return $@"mongodb://{User}:{Password}@{Host}:{Port}";
                }
                else
                {
                    return $@"mongodb://{UserEnvVar}:{PasswordEnvVar}@{Host}:{Port}";
                }


            }
        }
        public string ConnectionString1
        {
            get
            {
                //mongodb://root:{PASSWORDHERE}@db:27017/?authSource=admin&readPreference=primary&appname=test&ssl=false
                string UserEnvVar = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME");
                string PasswordEnvVar = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD");
                string MongoEnvHost= Environment.GetEnvironmentVariable("MongoDB__Host");
                string MongoEnvPort = Environment.GetEnvironmentVariable("MONGODB_PORT");
                
                if (string.IsNullOrEmpty(UserEnvVar) || string.IsNullOrEmpty(PasswordEnvVar))
                {

                    if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    {
                        return $@"mongodb://{Host}:{Port}";
                    }
                    else
                        return $@"mongodb://{User}:{Password}@{Host}:{Port}";
                }
                else
                {
                    return $@"mongodb://{UserEnvVar}:{PasswordEnvVar}@{MongoEnvHost}:{MongoEnvPort}/?authSource=admin&readPreference=primary&appname=test&ssl=false";
                }


            }
        }
    }
}

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace Version170
{
    public class MongoLogHelper
    {
        private Configuration loadConfig(string configName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + configName;
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = path;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            //string ddd=config.GetSection("appSettings");
            return config;
        }

        private string strconn = string.Empty;
        private string dbName = string.Empty;
        private string dbCollection = string.Empty;

        public MongoLogHelper()
        {
            Configuration config = loadConfig("mongolog.config");
            strconn = config.AppSettings.Settings["strConn"].Value;
            dbName = config.AppSettings.Settings["dbName"].Value;
            dbCollection = config.AppSettings.Settings["dbCollection"].Value;
        }
        private static MongoLogHelper _Instance;
        private readonly static object sysObj = new object();
        public static MongoLogHelper Instance()
        {
            if (_Instance == null)
            {
                lock (sysObj)
                {
                    if (_Instance == null)
                    {
                        _Instance = new MongoLogHelper();
                    }
                }
            }
            return _Instance;
        }

        #region Example Log
        /// <summary>
        /// 插入一个BsonDocument
        /// </summary>
        /// <param name="document"></param>
        public void LogForMongo(BsonDocument document)
        {

            MongoClient client = new MongoClient(strconn);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(dbName);
            MongoCollection col = db.GetCollection(dbCollection);
            document.Add("Author", "liuweiqin");
            document.Add("ts", new DateTime());
            col.Insert(document);
        }

        #endregion
    }
}

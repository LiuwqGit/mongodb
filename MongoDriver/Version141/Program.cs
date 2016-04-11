using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;

namespace Version141
{
    class Program
    {
        //数据库连接字符串
        private const string strconn = "mongodb://127.0.0.1:27017";
        //数据库名称
        private const string dbName = "cnblogs";
        //集合名称
        private const string dbCollection = "Users";

        static void Main(string[] args)
        {
            //Insert();
            //Update();
            //Delete();
            Query();
            Console.WriteLine("Query");
        }
        /// <summary>
        /// 初始化数据库文档
        /// </summary>
        /// <returns></returns>
        private static MongoCollection Init()
        {
            //创建数据库链接
            MongoServer server = new MongoClient(strconn).GetServer();
            //获得数据库cnblogs
            MongoDatabase db = server.GetDatabase(dbName);
            MongoCollection col = db.GetCollection(dbCollection);
            return col;
        }
        private static void Insert()
        {
            MongoCollection col = Init();

            for (int i = 0; i < 10; i++)
            {
                Users users = new Users();
                users.Name = "liusc" + i.ToString();
                users.Sex = "man";
                //获得Users集合，如果数据库中没有，系统会自动新建一个 
                //执行插入操作
                col.Insert<Users>(users);
            }
        }

        private static void Update()
        {
            MongoCollection col = Init();
            //定义获取"Name"值为"liusc"的查询条件
            var query = new QueryDocument { { "Name", "liusc" } };
            //定义更新文档
            var update = new UpdateDocument { { "$set", new QueryDocument { { "Sex", "wowen" } } } };
            //执行更新操作
            col.Update(query, update);
        }

        private static void Delete()
        {
            MongoCollection col = Init();
            //定义获取"Name"值为"liusc"的查询条件
            var query = new QueryDocument { { "Name", "liusc" } };
            col.Remove(query);//移除集合中满足条件query的数据
            //col.RemoveAll();//移除集合全部数据
        }

        private static void Query()
        {
            MongoCollection col = Init();

            //定义获取"Name"值为"liusc"的查询条件
            var query = new QueryDocument { { "Name", "liusc0" } };
            //查询全部集合里的数据
            var result1 = col.FindAllAs<Users>();
            //查询指定查询条件的第一条数据，查询条件可缺省。
            var result2 = col.FindOneAs<Users>();
            //查询指定查询条件的全部数据
            var result3 = col.FindAs<Users>(query);

        }

        public class Users
        {
            public ObjectId _id { get; set; }//此属性必须
            public string Name { get; set; }
            public string Sex { get; set; }
        }
    }
}

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SERedis.SE.BaseDemo
{
    public class SEOpeater
    {
        public static void OpeaterString()
        {
            var db = RedisManager.Instance.GetDatabase();
           
            string key = "keyTest1";
            //SET命令
            db.StringSet(key, "10");
            //GET命令
            string value = db.StringGet(key);
            Console.WriteLine(value);
            //APPEND命令
            db.StringAppend(key, "10");
            value = db.StringGet(key);
            Console.WriteLine(value);
            //有第二个参数（整数，参看StringIncrement方法）为DECRBY命令
            //没有第二个参数为DECR命令
            db.StringDecrement(key);
            value = db.StringGet(key);
            Console.WriteLine(value);
            //有第二个参数为INCRBY命令
            //没有第二个参数为INCR命令
            db.StringIncrement(key, 2);
            value = db.StringGet(key);
            Console.WriteLine(value);
            string key2 = "keyTest";
            //SETEX命令，带过期时间
            db.StringSet(key2, "keyTest2", new TimeSpan(0, 0, 5));
            string value2 = db.StringGet(key2);
            Console.WriteLine(value2);
            Thread.Sleep(5 * 1000);
            //超过5s后，查不到该值
            value2 = db.StringGet(key2);
            Console.WriteLine("5s later:" + value2);
            //GETSET命令，读出原来的值，并附新值
            //下面两个是测试
            value = db.StringGetSet(key, "2000");
            Console.WriteLine(value);
            value = db.StringGet(key);
            Console.WriteLine(value);
            //MSET命令
            db.StringSet(new KeyValuePair<RedisKey, RedisValue>[] {
                   new KeyValuePair<RedisKey, RedisValue>("key1", "value1"),
                   new KeyValuePair<RedisKey, RedisValue>("key2", "value2"), });
            //MGET命令
            RedisValue[] values = db.StringGet(new RedisKey[] { "key1", "key2" });
            Console.WriteLine(values[0] + "&&" + values[1]);

        }

        public static void OpeaterHashes()
        {

            var db = RedisManager.Instance.GetDatabase();
            #region Hash命令
            string key = "mykey";
            //避免key重复
            db.KeyDelete(key);
            //HSET命令
            db.HashSet(key, "a", "1");
            //HGET命令
            string value = db.HashGet(key, "a");
            Console.WriteLine(value);
            //HMSET
            db.HashSet(key, new HashEntry[] { new HashEntry("b", "2"), new HashEntry("c", "3") });
            //HMGET
            HashEntry[] values = db.HashGetAll(key);
            Console.WriteLine(values[0].Name + "///" + values[0].Value);
            //HDEL
            db.HashDelete(key, "c");
            string valuec = db.HashGet(key, "c");
            Console.WriteLine("c:" + valuec);
            //HEXISTS
            Console.WriteLine(db.HashExists(key, "a"));
            #endregion
        }

        public static void OpeaterList()
        {
            var db = RedisManager.Instance.GetDatabase();
            #region List命令
            string key = "mykey";
            db.KeyDelete(key);
            //LPUSH
            long index = db.ListLeftPush(key, "test");
            //LINDEX,index返回总的长度,index必须减一
            string value = db.ListGetByIndex(key, index - 1);
            Console.WriteLine(value);
            //LINSTER
            long index2 = db.ListInsertAfter(key, "test", "testright");
            string value2 = db.ListGetByIndex(key, index2 - 1);
            Console.WriteLine(value2);
            long index3 = db.ListInsertBefore(key, "test", "testleft");
            string value3 = db.ListGetByIndex(key, index - 1);
            //LRANGE
            RedisValue[] values = db.ListRange(key);
            Console.WriteLine("values:begin");
            values.ToList().ForEach((v) =>
            {
                Console.WriteLine(v);
            });
            Console.WriteLine("values:end");
            //LREM
            long index4 = db.ListRemove(key, "test");
            values = db.ListRange(key);
            Console.WriteLine("values2:begin");
            values.ToList().ForEach((v) =>
            {
                Console.WriteLine(v);
            });
            Console.WriteLine("values2:end");
            //LPOP
            string value5 = db.ListLeftPop(key);
            Console.WriteLine(value5);
            values = db.ListRange(key);
            Console.WriteLine("values3:begin");
            values.ToList().ForEach((v) =>
            {
                Console.WriteLine(v);
            });
            Console.WriteLine("values3:end");
            Console.WriteLine(value3);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"单 中企，双 越海 {new Random().Next(100)}");
            }
    
            Console.WriteLine(value3);

           
            #endregion
        }

        public static void TranSfer()
        {
            var db = RedisManager.Instance.GetDatabase();

            //设置值
            db.StringSet("name", "张三");
            db.StringSet("age", 90);
            string name = db.StringGet("name");
            string age = db.StringGet("age");
            Console.WriteLine("name:" + name);
            Console.WriteLine("age:" + age);

            //创建一个事物
            ITransaction trans = db.CreateTransaction();
            //锁定RedisKey=name RedisValue=张三的缓存
            trans.AddCondition(Condition.StringEqual("name", name));
            Console.WriteLine("begin trans");
            trans.StringSetAsync("name", "Tom");

            bool isExec = trans.Execute(); //提交事物，name才会修改成功 name = Tom

            Console.WriteLine("事物执行结果：" + isExec);

            string _name = db.StringGet("name");
            string _age = db.StringGet("age");
            Console.WriteLine("name:" + _name);
            Console.WriteLine("age:" + _age);

            Console.WriteLine("end trans");
        }
    }
}
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERedis.SE.BaseDemo
{
    /// <summary>
    /// 单列
    /// </summary>
    public class RedisManager
    {
        private RedisManager() { }
        private static ConnectionMultiplexer instance;
        private static readonly object locker = new object();

        private static ConfigurationOptions configurationOptions =ConfigurationOptions.Parse("127.0.0.1" + ":" + "6378");
        /// <summary>
        /// 单例模式获取redis连接实例
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                lock (locker)
                {

                    if (instance == null)
                    {
                        if (instance == null)
                            instance = ConnectionMultiplexer.Connect(configurationOptions); //这里应该配置文件，不过这里演示就没写
                           // instance = ConnectionMultiplexer.Connect("127.0.0.1"); //这里应该配置文件，不过这里演示就没写
                    }
                }
                return instance;
            }
        }
    }
}

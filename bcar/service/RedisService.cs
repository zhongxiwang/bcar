using log4net;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{
    /// <summary>
    /// redis 接口
    /// </summary>
    public class RedisService
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Confiration { get; set; }
        ConnectionMultiplexer redis { get; set; }
        IDatabase Db { get; set; }
        ILog log { get; set; }
        readonly int keyouttime;
        /// <summary>
        /// redis 默认库为1
        /// </summary>
        /// <param name="configuration"></param>
        public RedisService(IConfiguration configuration,ILog log)
        {
            this.Confiration = configuration;
            this.log = log;
            keyouttime = this.Confiration["redisKeyOutTime"] == null ? 5 : int.Parse(this.Confiration["redisKeyOutTime"]);
            redis = ConnectionMultiplexer.Connect(this.Confiration["redisConnection"]);
            ChanageDatabase(1);
            gq();
            var sub= this.redis.GetSubscriber();
        }
        
        /// <summary>
        ///切换数据库
        /// </summary>
        /// <param name="i"></param>
        public void ChanageDatabase(int i)
        {
            this.Db = redis.GetDatabase(i);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        public void Write(string key,RedisValue buffer)
        {
            var task= this.Db.StringSetAsync(key, buffer);
            task.ContinueWith(b =>
            {
                if(!b.Result) log.Error("redis写入失败");
                var res=  this.Db.KeyExpireAsync(key, DateTime.Now.AddMinutes(this.keyouttime)).Result;
                if(!res) log.Error("redis 过期时间写入失败");
            });
            if (task.Exception != null) log.Error(task.Exception.Message);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="buffer"></param>
        public void Write<T>(string key,T buffer)
        {
            var task = this.Db.StringSetAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(buffer));
            task.ContinueWith(b =>
            {
                if (!b.Result) log.Error("redis写入失败");
                var res = this.Db.KeyExpireAsync(key, DateTime.Now.AddMinutes(this.keyouttime)).Result;
                if (!res) log.Error("redis 过期时间写入失败");

            });
            if (task.Exception != null) log.Error(task.Exception.Message);
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String Read(string key)
        {
            String result;
            try
            {
                result = (String)this.Db.StringGet(key);

            }catch(Exception e)
            {
                return "";
            }
            return result;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Read<T>(string key)
        {
            try
            {
               return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Read(key));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<string> ReadALL()
        {
            var endpoints = redis.GetEndPoints();
            List<string> keyList = new List<string>();
            foreach (var ep in endpoints)
            {
                var server = redis.GetServer(ep);
                var keys = server.Keys(0, "*");
                foreach (var item in keys)
                {
                    keyList.Add((string)item);
                }
            }
            return keyList; 
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            redis.Close();
        }

        public void delete(string key)
        {
            this.Db.KeyDelete(key);
        }

        public void gq()
        {
            var subscriber = redis.GetSubscriber();
            subscriber.Subscribe("__keyspace@0__:*", (channel, notificationType) =>
            {
                log.Info(channel + "|" + notificationType);
            });

        }
    }
}

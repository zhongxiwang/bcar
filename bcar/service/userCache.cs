using bcar.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace bcar.service
{
    public class userCache
    {
        public RedisService redis { get; private set; }
        IDbConnection con { get; set; }
        public userCache(RedisService redis, IDbConnection db)
        {
            this.redis = redis;
            this.con = db;
        }

        public userinfo read(string wxcount)
        {
            var result= redis.Read<userinfo>(wxcount);
            try
            {
                if (result == null)
                {
                    result = this.con.QueryFirst<userinfo>("select * from userinfo where wxCount='" + wxcount + "'");
                    redis.Write<userinfo>(wxcount, result);
                }
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public userinfo readById(string id)
        {
            try
            {
                   var result = this.con.QueryFirst<userinfo>("select * from userinfo where id='" + id + "'");
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void update(string wxcount)
        {
            try
            {
                var result = this.con.QueryFirst<userinfo>("select * from userinfo where wxCount='" + wxcount + "'");
                redis.Write<userinfo>(wxcount, result);
            }
            catch
            {
                
            }
        }

        public void rmove(string wxcount)
        {
            this.redis.delete(wxcount);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using Dapper;

namespace bcar.service
{
    public class driverCache
    {
        public RedisService redis { get; private set; }

        IDbConnection con { get; set; }

        public driverCache(RedisService redis, IDbConnection db)
        {
            this.redis = redis;
            this.con = db;
        }

        public driverinfo read(int userid)
        {
            var result = redis.Read<driverinfo>(userid.ToString());
            try
            {
                if (result == null)
                {
                    result = this.con.QueryFirst<driverinfo>("select * from userinfo where userid='" + userid + "'");
                    redis.Write<driverinfo>(userid.ToString(), result);
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void update(int userid)
        {
            try
            {
                var result = this.con.QueryFirst<driverinfo>("select * from driverinfo where userid='" + userid + "'");
                redis.Write<driverinfo>(userid.ToString() , result);
            }
            catch
            {

            }
        }
    }
}

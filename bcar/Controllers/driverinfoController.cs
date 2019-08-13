using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using bcar.model;
using bcar.uilt;
using System.Data;
using System.Collections;
using Newtonsoft.Json.Linq;
using bcar.service;
using System.Net.Http;
using bcar.Socket;
using log4net;

namespace bcar.Controllers
{
    /// <summary>
    /// 司机信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    //[Authentication]
    public class driverinfoController : ControllerBase
    {
        IDbConnection db { get; set; }

        RedisService redis { get; set; }

        TokenService token { get; set; }
        userCache _uc { get; set; }
        ILog _log { get; set; }
        /// <summary>
        /// driver infomaction
        /// </summary>
        /// <param name="db"></param>
        public driverinfoController(IDbConnection db, userCache uc, RedisService redis,TokenService token,ILog log)
        {
            this.db = db;
            this.redis = redis;
            this.token = token;
            this._uc = uc;
            this._log = log;
        }
        /// <summary>
        /// 获取全部司机信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result Get(string where = "", int limit = 10, int page = 0)
        {
            page--;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from driverinfo " + where, page, limit);
                result.data = db.Query<driverinfo>(sql);
                result.count=(long) db.ExecuteScalar(uiltT.Count("select * from driverinfo " + where));
                result.code = 0;
            }
            catch (Exception e)
            {
                result.msg = e.Message;
                result.code = -1;
            }
            return result;
        }

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public driverinfo Get(int id) 
        {
            driverinfo result = null;
            try
            {
                result = this.redis.Read<driverinfo>(id.ToString());
                if (result != null) return result;
                result= db.QueryFirst<driverinfo>("select * from driverinfo where userid=" + id);
                this.redis.Write<driverinfo>(id.ToString(), result);
                return result;
            }
            catch
            {
                return null;
            }
        }
        [HttpGet]
        [Route("/currentDriverInfo")]
        public driverinfo currentDriverInfo()
        {
            driverinfo result = null;
            int id = this.HttpContext.Session.GetInt32("userid") ?? 0;
            if (id == 0) return null;
            try
            {
                result = this.redis.Read<driverinfo>(id.ToString());
                if (result != null) return result;
                result = db.QueryFirst<driverinfo>("select * from driverinfo where userid=" + id);
                this.redis.Write<driverinfo>(id.ToString(), result);
                return result;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] driverinfo value)
        {
            Result.Run(key =>
            {
                _log.Error(Newtonsoft.Json.JsonConvert.SerializeObject(value));
                value.userid = HttpContext.Session.GetInt32("userid") ?? 0;
                if (value.userid == 0) return;
                var list = new string[]
                {
                value.idcard,
                value.driverlicense,
                value.carimage,
                value.drivinglicense,
                value.operationcertificate
                };
                var result = downloadImage(list);
                value.idcard = result[0];
                value.driverlicense = result[1];
                value.carimage = result[2];
                value.drivinglicense = result[3];
                value.operationcertificate = result[4];
                _log.Error(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                var sql= value.Insert();
                _log.Error(Newtonsoft.Json.JsonConvert.SerializeObject(sql));
                var n = this.db.Execute(sql); 
            });

        }

        private string[] downloadImage(string[] serverid)
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            string[] result = new string[serverid.Length]; 
            int i = 0;
            HttpClient client = new HttpClient();
            if (this.db.State != ConnectionState.Open) this.db.Open();
            var trans = this.db.BeginTransaction();
            try
            {
                Task.Run(async () =>
               {
                   foreach (var key in serverid)
                   {
                       if (string.IsNullOrEmpty(key))
                       {
                           result[i++] = key;
                           continue;
                       }
                       model.upload tmp = new model.upload();
                       string url = "https://api.weixin.qq.com/cgi-bin/media/get?access_token=" + this.token + "&media_id=" + key;
                       var dataset = await client.GetByteArrayAsync(url);
                       System.IO.File.WriteAllBytes(path + "/data/image/" + tmp.id, dataset);
                       result[i++] = tmp.id;
                       this.db.Execute(tmp.Insert());
                   }

                   trans.Commit();
               }).Wait();
                return result;
            }
            catch (Exception e)
            {
                trans.Rollback();
                return new string[5];
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]JObject value)
        {
            var openid= HttpContext.Session.GetString("openid");
            if(openid!=null)this.redis.delete(openid);
            this.db.Execute(uiltT.Update(value, "driverinfo", "where userid=" + id));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        [Route("putDriverInfo")]
        [HttpGet]
        public int putDriverInfo(string key,string val)
        {
            var openid = HttpContext.Session.GetString("openid");
            var user = this._uc.read(openid);
            this.redis.delete(openid);
            
            return this.db.Execute("update driverinfo set " + key + " ='" + val + "' where userid='" + user.id + "'");
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.redis.delete(id.ToString());
            this.db.Execute(uiltT.Delete("driverinfo", " userid=" + id));
        }
    }
}


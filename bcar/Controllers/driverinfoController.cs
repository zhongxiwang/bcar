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
using bcar.Socket;

namespace bcar.Controllers
{
    /// <summary>
    /// 司机信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    [Authentication]
    public class driverinfoController : ControllerBase
    {
        IDbConnection db { get; set; }

        RedisService redis { get; set; }
        /// <summary>
        /// driver infomaction
        /// </summary>
        /// <param name="db"></param>
        public driverinfoController(IDbConnection db,RedisService redis)
        {
            this.db = db;
            this.redis = redis;
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

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] driverinfo value)
        {
            this.db.Execute(value.Insert());
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]JObject value)
        {
            this.redis.delete(id.ToString());
            this.db.Execute(uiltT.Update(value, "driverinfo", "where userid=" + id));
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

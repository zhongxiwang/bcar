using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.uilt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using bcar.model;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    public class userinfoController : ControllerBase
    {
        IDbConnection db { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public userinfoController(IDbConnection db)
        {
            this.db = db;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<userinfo> Get(string where="", int pageSize=10,int pageIndex=0)
        {
            return db.Query<userinfo>(uiltT.limit("select * from userinfo",pageIndex,pageSize));
        }

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public userinfo Get(int id)
        {
            try
            {
                return db.QueryFirst<userinfo>("select * from userinfo where id=" + id);
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
        public void Post([FromBody] userinfo value)
        {
            this.db.ExecuteScalar(value.Insert());
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JObject value)
        {
             this.db.ExecuteScalar(uiltT.Update(value, "userinfo", "where id=" + id));
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.db.ExecuteScalar(uiltT.Delete("userinfo", "where id=" + id));
        }
    }
}

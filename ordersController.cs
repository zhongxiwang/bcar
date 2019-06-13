using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using bcar.model;
using bcar.uilt;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    /// <summary>
    /// 账单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    public class ordersController : ControllerBase
    {
        IDbConnection db { get; set; }
        /// <summary>
        /// driver infomaction
        /// </summary>
        /// <param name="db"></param>
        public ordersController(IDbConnection db)
        {
            this.db = db;
        }
        /// <summary>
        /// 获取全部司机信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<orders> Get(string where = "", int pageSize = 10, int pageIndex = 0)
        {
            return db.Query<orders>(uiltT.limit("select * from orders", pageIndex, pageSize));
        }

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public orders Get(int id)
        {
            try
            {
                return db.QueryFirst<orders>("select * from orders where id=" + id);
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
        public void Post([FromBody] orders value)
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
            this.db.ExecuteScalar(uiltT.Update(value, "orders", "where id=" + id));
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.db.ExecuteScalar(uiltT.Delete("driverinfo", "where id=" + id));
        }
    }
}

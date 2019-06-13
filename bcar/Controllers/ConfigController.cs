using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using bcar.service;
using bcar.model;
using Newtonsoft.Json.Linq;
using bcar.uilt;
using Microsoft.AspNetCore.Cors;
using bcar.Socket;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    [Authentication]
    public class ConfigController : ControllerBase
    {
        public IDbConnection db { get; set; }
        public CostService Cost { get; set; }
        public ConfigController(IDbConnection db,CostService Cost)
        {
            this.db = db;
            this.Cost = Cost;
        }

        /// <summary>
        /// 获取全部配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ConigAll")]
        public Result Get(int page,int limit)
        {
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from configuration", page, limit);
                result.data = db.Query<Config>(sql);
                result.count = (long)db.ExecuteScalar(uiltT.Count("select * from configuration "));
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
        /// 获取配置数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return CostService.Config[id];
        }


        /// <summary>
        /// 更新配置数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] JObject value)
        {
            var sql= uilt.uiltT.Update(value, "configuration", "where keyv='"+id+"'");
            var result= this.db.Execute(sql) > 0 ? true : false;
            Task.Run(() =>
            {
                if (result) this.Cost.reloadConfig();
            });
            return result;
           
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using bcar.service;
using bcar.Socket;
using bcar.uilt;
using Dapper;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    [Authentication]
    public class LocationSiteController : ControllerBase
    {

        IDbConnection db { get; set; }
        ILog log { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public LocationSiteController(IDbConnection db,ILog log,CostService cost)
        {
            this.db = db;
            this.log = log;
        }


        // GET: api/LocationSite
        [HttpGet]
        public Result Get(string where = "", int limit = 10, int page = 0)
        {
            page -= 1;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from locationSite " + where, page, limit);
                result.data = db.Query<locationSite>(sql);
                result.count = (long)db.ExecuteScalar(uiltT.Count("select * from locationSite " + where));
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
        /// 模糊搜索
        /// </summary>
        /// <param name="where">搜索数值</param>
        /// <param name="slname">上次输入的地址</param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/likeSearch")]
        public IEnumerable<locationSite> likeSearch(string where= "",string slname="", int limit = 10, int page = 0)
        {
            page -= 1;
            if (string.IsNullOrWhiteSpace(where)) where = "";
            if (string.IsNullOrWhiteSpace(slname)) slname = "";
            if (page < 0) page = 0;
            try
            {
                if (slname == "")
                {
                    string tmp = "select * from locationSite where  sitename like '%" + where + "%'";
                    var sql = uiltT.limit(tmp, page, limit);
                    return db.Query<locationSite>(sql);
                }
                else
                {
                    string tmp = "select * from  locationSite where irouteid in( select irouteid from locationSite where  sitename= '"+ slname + "' ) and sitename<>'" + slname + "'";
                    var sql = "select * from ( " + tmp + ") a where a.sitename like '%"+where+"%'";
                    var result= db.Query<locationSite>(sql);
                    return result;
                }
            }
            catch
            {
            }
            return null;
        }

        // POST: api/LocationSite
        [HttpPost]
        public int Post([FromBody]locationSite[] value)
        {
            if (this.db.State != ConnectionState.Open) this.db.Open();
            var trn = this.db.BeginTransaction();
            try
            {
                foreach (var key in value) this.db.Execute(key.Insert());
                trn.Commit();
            }
            catch(Exception e)
            {
                this.log.Error(e.Message);
                trn.Rollback();
                return -1;
            }
            return 1;
        }

        // PUT: api/LocationSite/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JObject value)
        {
            this.db.Execute(uiltT.Update(value, "locationSite", "where id=" + id));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.db.Execute(uiltT.Delete("locationSite", " id=" + id));
        }


        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDistance")]
        public double getDistance(locationSite[] dataset)
        {
            try
            {
                var last = dataset[1];
                var result= this.db.Query<locationSite>("select * from locationSite where irouteid=" + last.irouteid+" and lindex>="+dataset[0].lindex + " and lindex<"+last.lindex);
                double distance = 0;
                foreach (var key in result)
                    distance += double.Parse(key.nxdistance);
                return distance;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="distance">距离</param>
        /// <param name="type">下单的类型</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getPrice")]
        public double getPrice(string distance,int type)
        {
            double distancen = 0;
            if (distance[distance.Length - 1] == '米') distancen = 1;//double.Parse(distance.Substring(0, distance.Length - 1));
            else if (distance[distance.Length - 1] == '里') distancen = double.Parse(distance.Substring(0, distance.Length - 2));
            else distancen = double.Parse(distance);
            if (type == 4) distancen = double.Parse(distance)/1000;
            try
            {
                Func<double, double,double,double> action = (StartingPrice, StartingDistance, StartOldPrice) =>
                 {
                     if (distancen <= StartingDistance)
                         return StartingPrice;
                     var s = distancen - StartingDistance;
                     return Math.Round(StartingPrice + StartOldPrice * s, 2);
                 };
                double sip = 0;
                double sid = 0;
                double sod = 0;
                switch (type)
                {
                    case 1:
                        {
                            sip = double.Parse(CostService.Config["StartingPriceZc"]);
                            sid = double.Parse(CostService.Config["StartingDistanceZc"]);
                            sod = double.Parse(CostService.Config["StartOldPriceZc"]);
                        }
                        break;
                    case 2:
                        {
                            sip = double.Parse(CostService.Config["StartingPriceSfc"]);
                            sid = double.Parse(CostService.Config["StartingDistanceSfc"]);
                            sod = double.Parse(CostService.Config["StartOldPriceSfc"]);
                        }
                        break;
                    case 3:
                        {
                            sip = double.Parse(CostService.Config["StartingPriceSd"]);
                            sid = double.Parse(CostService.Config["StartingDistanceSd"]);
                            sod = double.Parse(CostService.Config["StartOldPriceSd"]);
                        }
                        break;
                    case 4:
                        {
                            sip = double.Parse(CostService.Config["StartingPriceKc"]);
                            sid = double.Parse(CostService.Config["StartingDistanceKc"]);
                            sod = double.Parse(CostService.Config["StartOldPriceKc"]);
                        }
                        break;
                }
                return action(sip,sid,sod);
            }
            catch
            {
                return -1;
            }
        }
        
    }
}

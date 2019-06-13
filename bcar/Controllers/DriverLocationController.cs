using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using bcar.service;
using bcar.Socket;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    [Authentication]
    public class DriverLocationController : ControllerBase
    {
        IDbConnection idb { get; set; }
        userCache uc { get; set; }
        public DriverLocationController(IDbConnection db, userCache uc)
        {
            this.idb = db;
            this.uc = uc;
        }
        const double radius = 3000;
        // GET: api/DriverLocation
        /// <summary>
        /// 获取司机信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<driverlocation> Get()
        {
            return driverService.driverinfo.Values;
        }
        [HttpGet]
        [Route("nearbydriver")]
        public IEnumerable<driverlocation> nearbydriver(double lat,double lng,string id)
        {
            var result = new List<driverlocation>();
            var userlist = new List<string>();
            
            var order= this.idb.QueryFirst<orders>("select * from orders where id="+id);
            var req= Hup.CreateMsg.CreateRequest(order, "orderMessage");
            driverService.isLock = true;
            double statusR = -1;
            double statusTR = -1;
            int ids = -1, idst = -1;
            foreach (var key in driverService.driverinfo)
            {
                var n = uilt.uiltT.getDistance(lat, lng, key.Value.pointy, key.Value.pointx);
                if (key.Value.status == 1)
                {
                    if (statusR > n || statusR == -1)
                    {
                        statusR = n;
                        ids = int.Parse(key.Value.content);
                    }
                    if (n <= radius)
                    {
                        result.Add(key.Value);
                        userlist.Add(key.Key);
                    }
                }
                else
                    if (statusTR > n || statusR == -1)
                {
                    idst = int.Parse(key.Value.content);
                    statusTR = n;
                }
            }
            driverService.isLock = false;
            if (userlist.Count == 0)
            {
                var systemList= UseSocket.CreateSocket().getSystemUser();
                req.RecUserlist.AddRange(systemList);
                req.SendUser = "system";
                req.Head["r"] = statusR.ToString();
                userinfo ui = null;
                if (ids != -1)
                {
                    ui = this.idb.QueryFirst<userinfo>("select * from userinfo where id='" + ids + "'");
                    req.Head.Add("ch", ui.username);
                    req.Head.Add("mobile", ui.mobile);

                }
                Hup.CreateMsg.Run(req);
            }
            else
            {
                req.RecUserlist = userlist;
                req.SendUser = "system";
                Hup.CreateMsg.Run(req);
            }
            return result;
        }


        // POST: api/DriverLocation
        [HttpPost]
        public void Post([FromBody] driverlocation value)
        {
            
        }

        // PUT: api/DriverLocation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// 地图距离计算
        /// </summary>
        /// <param name="lat_a"></param>
        /// <param name="lng_a"></param>
        /// <param name="lat_b"></param>
        /// <param name="lng_b"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("distance")]
        public  double getDistance(double lat_a, double lng_a, double lat_b, double lng_b)
        {
            double pk = 180 / 3.14169;
            double a1 = lat_a / pk;
            double a2 = lng_a / pk;
            double b1 = lat_b / pk;
            double b2 = lng_b / pk;
            double t1 = Math.Cos(a1) * Math.Cos(a2) * Math.Cos(b1) * Math.Cos(b2);
            double t2 = Math.Cos(a1) * Math.Sin(a2) * Math.Cos(b1) * Math.Sin(b2);
            double t3 = Math.Sin(a1) * Math.Sin(b1);
            double tt = Math.Acos(t1 + t2 + t3);
            return 6371000 * tt;
        }
    }
}

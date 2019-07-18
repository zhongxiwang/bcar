﻿using System;
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
using bcar.service;
using bcar.Socket;

namespace bcar.Controllers
{
    /// <summary>
    /// 账单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    //[Authentication]
    public class ordersController : ControllerBase
    {
        IDbConnection db { get; set; }
        userCache Uc { get; set; }
        TokenService token { get; set; }
        static DateTime upt = DateTime.Now;
        /// <summary>
        /// driver infomaction
        /// </summary>
        /// <param name="db"></param>
        public ordersController(IDbConnection db,userCache uc,TokenService token)
        {
            this.db = db;
            this.Uc = uc;
            this.token = token;
            if (upt.AddMinutes(30) <= DateTime.Now)
            {
                this.db.Execute("update orders set state=3 where StartTime<CURRENT_TIMESTAMP");
                upt = DateTime.Now;
            }
            
        }
        /// <summary>
        /// 获取全部订单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result Get(string where = "", int limit = 10, int page = 0)
        {
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from orders " + where+ " order by createtime desc", page, limit);
                result.data = db.Query<orders>(sql);
                result.count = (long)db.ExecuteScalar(uiltT.Count("select * from orders " + where));
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
        /// 获取订单初始信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("initial")]
        public Result initial(string where = "", int limit = 10, int page = 0)
        {
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from orders " + where+ " order by createtime desc", page, limit);
                var dt = db.Query<orders>(sql);
                result.data= dt.Select(key =>
                {
                    var tm= JObject.Parse(key.ordersInfo);
                     tm.Add("oid", key.id);
                    return tm;
                });
                result.count = (long)db.ExecuteScalar(uiltT.Count("select * from orders " + where));
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
        /// 获取订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("getOrder")]
        public Result getOrder(int page,int limit, int userid,int type)
        {
            string sql = "select * from orders where (routeid = (select route from driverinfo where userid = " + userid + ") or routeid=0) and state = 0 and orderType=" + type+ " order by createtime desc";
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var tmp = uiltT.limit(sql, page, limit);
                result.data = db.Query<orders>(tmp);
                result.count = (long)db.ExecuteScalar(uiltT.Count(sql));
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
        /// 司机订单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("driverOrder")]
        public Result driverOrder(int page, int limit, int userid,string state="")
        {
            string sql = "select * from orders where driverid=" + userid;
            if(state!="") sql+=" and state=" + state;
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var tmp = uiltT.limit(sql, page, limit);
                result.data = db.Query<orders>(tmp);
                result.count = (long)db.ExecuteScalar(uiltT.Count(sql));
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
        /// 创建用户信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] orders value)
        {
            this.db.ExecuteScalar(value.Insert());
        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("submitorderinfo")]
        public Result Post([FromBody] orderinfo value)
        {
            lock (this)
            {
                 var str= HttpContext.Session.GetInt32("order");
                if (str != null&& DateTime.Now.Minute==str) return Result.Run(res => throw new Exception("不能频繁提交订单"));
            }
            return Result.Run(ress =>
            {
                var openid= HttpContext.Session.GetString("openid");
                HttpContext.Session.SetInt32("order",DateTime.Now.Minute);
                if (value.riderType != 4 && value.startDate.AddMinutes(2) <= DateTime.Now.AddHours(2)) throw new Exception("乘坐时间必须在2小时后") ;
                if (value.riderType == 4) value.startDate.AddMinutes(5);
                orders result = Hup.CreateMsg.Run(value).Content;
                var user = this.Uc.read(openid);
                if (user.bill < value.price)
                {
                    ress.code = 2;
                    ress.data = value.price;
                    throw new Exception("账户余额不足。");
                }
                result.userid = user.id;
                var sql = result.Insert();
                var cmd = this.db.CreateCommand();
                this.db.Open();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select LAST_INSERT_ID()";
                var n = this.db.Execute(sql);
                var id = (ulong)cmd.ExecuteScalar() ; //this.db.ExecuteScalar<long>("");
                value.startDate = value.startDate.AddMinutes(5);
                this.db.Close();
                string[] eumslist = new string[] { "", "顺风车", "专车", "速递", "快车" };
                Task.Run(async () =>
                {
                    await TaskManagerService.Factory().AutoRun(value.startDate, key =>
                    {
                        JObject obj = new JObject();
                        obj.Add("state", 3);
                        string upsql = uilt.uiltT.Update(obj, "orders", " where id='" + id + "' ");
                        db.Execute(upsql);
                        uilt.uiltT.SendWxMessage(token, "您的订单从" + value.startingPoint + "到" + value.endingPoint + "的行程，由于长时间没有司机接单已经超时，请重新创建行程。", openid);
                        return Task.CompletedTask;
                    }, "task" + id);
                });

                Task.Run(() =>
                {
                    var list = this.db.Query<string>("select wxCount from userinfo join driverinfo on userinfo.id=driverinfo.userid where driverstate=1");
                    foreach (var key in list)
                    {
                        uilt.uiltT.SendWxMessage(token, "有新的从" + value.startingPoint + "到" + value.endingPoint + "的" + eumslist[value.riderType] + "订单。", key);
                    }
                }).Wait();
                ress.data = id;
            });


            //return id.ToString();// n > 0 ? "" : "订单创建失败";
        }

        /// <summary>
        /// 获取用户订单详细
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrderInfo")]
        public Result GetOrderInfo(int type, int page,int limit)
        {
            page--;
            if (page < 0) page = 0;
            Func<Dictionary<string,orderinfo>, Result> action = (key) =>
            {
                var result = new Result();
                var tmps= key.Values.Skip(page * limit);
                if(tmps!=null) result.data = tmps.Take(limit);
                result.count = key.Count;
                result.code = 0;
                return result;
            };
            Dictionary<string, orderinfo> tmp = null;
            switch (type)
            {
                case 1:tmp = orderService.riderTypeOne;break;
                case 2:tmp = orderService.riderTypeTwo;break;
                case 3:tmp = orderService.riderTypeThere;break;
            }
            return action(tmp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iid"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] JObject value)
        {
            if (value["state"] != null) TaskManagerService.Factory().deleteJob("task"+id);
            int n= this.db.Execute(uiltT.Update(value, "orders", "where id=" + id));
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 订单完成，扣除费用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/orderComplete")]
        public bool complete(int id, [FromBody] JObject value)
        {
            if (this.db.State != ConnectionState.Open) this.db.Open();
            var trn = this.db.BeginTransaction();
            try
            {
                var order= this.db.QueryFirst<orders>("select * from orders where id=" + id);
                var users= this.Uc.readById(order.userid.ToString());
                users.bill = Math.Round(users.bill - order.userprice, 2);
                
                this.db.Execute("	UPDATE  userinfo set bill=" + users.bill + " where id=" + order.userid);

                userinfo drivers = null;
                if (order.driverid == order.userid.ToString()) drivers = users;
                else drivers=this.Uc.readById(order.driverid);
                Task.Run(() =>
                {
                    this.Uc.rmove(users.wxCount);
                    this.Uc.rmove(drivers.wxCount);
                });
                drivers.bill = Math.Round( drivers.bill + order.driverprice);
                this.db.Execute("	UPDATE  userinfo set bill=" + drivers.bill + " where id=" + order.driverid);
                int n = this.db.Execute(uiltT.Update(value, "orders", "where id=" + id));
                trn.Commit();
                SendTz(order);
                return n > 0 ? true : false;
            }
            catch(Exception e)
            {
                var t = e.Message;
                trn.Rollback();
            }
            return false;
        }

        private void SendTz(orders order)
        {
            uilt.uiltT.SendWxMessage(this.token, "你从" + order.startlocation + "到" + order.endlocation + "的订单有一笔" + order.userprice + "元的消费\r\n时间:" + order.createTime.ToString("yyyy-MM-dd hh:mm"), this.db.ExecuteScalar<string>("select wxCount from userinfo where id=" + order.userid));
            var driverWxCount = this.db.ExecuteScalar<string>("select wxCount from userinfo where id=" + order.driverid);
            uilt.uiltT.SendWxMessage(this.token, "你从" + order.startlocation + "到" + order.endlocation + "的订单有一笔" + order.driverprice + "元的收入\r\n时间:" + order.createTime.ToString("yyyy-MM-dd hh:mm"), driverWxCount);
              if (driverService.driverinfo.ContainsKey(driverWxCount)) driverService.driverinfo[driverWxCount].status = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iid"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        [HttpPut]
        [Route("meetOrder")]
        public bool meetOrder(int id, [FromBody] JObject value)
        {
            lock (this)
            {
                long t = (long)this.db.ExecuteScalar("select count(1) from orders where id=" + id + " and state =0");
                if (t == 0) return false;
                int n = this.db.Execute(uiltT.Update(value, "orders", "where id=" + id));
                TaskManagerService.Factory().deleteJob("task" + id);
                return n > 0 ? true : false;
            }
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

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
using bcar.service;
using bcar.Socket;
using System.Text;
using NETCore.Encrypt.Extensions;
using NETCore.Encrypt;

namespace bcar.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    [Authentication]
    public class userinfoController : ControllerBase
    {
        IDbConnection db { get; set; }
        userCache userc { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public userinfoController(IDbConnection db, userCache uc)
        {
            this.db = db;
            this.userc = uc;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result Get(string where="", int limit = 10,int page = 0)
        {
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from userinfo " + where, page, limit);
                result.data = db.Query<userinfo>(sql);
                result.count = (long)db.ExecuteScalar(uiltT.Count("select * from userinfo " + where));
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
        /// 获取用户信息
        /// </summary>
        /// <param name="wxcount">微信的openid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/userwxgetuserinfo")]
        public userinfo Gest()
        {
            var openid = HttpContext.Session.GetString("openid");
            var ui= this.userc.read(openid);
            ui.bill= Math.Round(ui.bill, 2);
            return ui;
        }

        [HttpGet]
        [Route("/abill")]
        public bool add(int t)
        {
            if (t == 0)
            {
                this.HttpContext.Session.Remove("fee");
                return true;
            }
            var fee= this.HttpContext.Session.GetInt32("fee");
            if (fee == null) return false;
            float pla = (float)fee / 100;
            var openid = this.HttpContext.Session.GetString("openid");
            if (openid == null)
                return false;
            var ui = this.userc.read(openid);
            ui.bill += pla;
            JObject obj = new JObject();
            obj.Add("bill", ui.bill.ToString());
            var res= this.db.Execute(uiltT.Update(obj, "userinfo", "where id=" + ui.id));
            this.HttpContext.Session.Remove("fee");
            return res == 1 ? true : false;
        }

        /// <summary>
        /// 获取用户粉丝数
        /// </summary>
        /// <param name="wxcount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/fans")]
        public IEnumerable<userinfo> fans()
        {
            var wxcount = HttpContext.Session.GetString("openid");
            return this.db.Query<userinfo>("select * from userinfo where recommender='" + wxcount + "'");
        }

        /// <summary>
        /// 用户是否注册过
        /// </summary>
        /// <param name="wxcount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/userisregister")]
        public object register()
        {
            var openid= HttpContext.Session.GetString("openid");
            if (openid == null) return 0;
            var t= this.db.ExecuteScalar("select driverstate from userinfo LEFT JOIN driverinfo  on userinfo.id=driverinfo.userid where wxCount='" + openid+"' ");
            var n= this.db.ExecuteScalar<long>("select count(1) from userinfo  where wxCount='" + openid + "' ");
            if (n == 1 && t != null) return 1;
            if (n == 1) return 2;
            return 0;
        }



        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] userinfo value)
        {
            if (value.recommender != null)
            {
                var result = this.userc.read(value.recommender);
                if (result != null) value.userid = result.id;
            }
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
            var us= this.userc.readById(id.ToString());
            //this.userc.read(us.wxCount);
            this.userc.rmove(us.wxCount);
             this.db.Execute(uiltT.Update(value, "userinfo", "where id=" + id));
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="mobile"></param>
        [HttpGet]
        [Route("/mobile")]
        public void mobile(string mobile)
        {
            var id= HttpContext.Session.GetString("openid");
            JObject obj = new JObject();
            obj.Add("mobile", mobile);
            this.db.Execute(uiltT.Update(obj, "userinfo", "where wxCount='" + id+"'"));
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.db.Execute(uiltT.Delete("userinfo", " id=" + id));
        }
    }
}

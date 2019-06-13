using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using bcar.Socket;
using bcar.uilt;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class adminController : ControllerBase
    {
        readonly IDbConnection _db;
        public adminController(IDbConnection con)
        {
            this._db = con;
        }
        
        [HttpGet]
        [Route("sigin")]
        public string Get(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return "0";
            password = password.MD5();
            var result = this._db.QueryFirstOrDefault<admin>("select * from adminTable where username='" + username + "' and pwd='" + password + "'");
            if (result != null)
            {
                HttpContext.Session.SetString("id", result.id);
                return "1";
            }
            else return "0";
        }


        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authentication]
        public Result Get(string where = "", int limit = 10, int page = 0)
        {
            page -= 1;
            if (page < 0) page = 0;
            Result result = new Result();
            try
            {
                var sql = uiltT.limit("select * from adminTable " + where, page, limit);
                result.data = _db.Query<admin>(sql);
                result.count = (long)_db.ExecuteScalar(uiltT.Count("select * from adminTable " + where));
                result.code = 0;
            }
            catch (Exception e)
            {
                result.msg = e.Message;
                result.code = -1;
            }
            return result;
        }
        [HttpPut]
        [Authentication]
        public string Put([FromBody]admin user)
        {
            var id = HttpContext.Session.GetString("id");

            if (!string.IsNullOrWhiteSpace(id))
            {
                return this._db.Execute("update admnTable set username=@username,emial=@emial where id='" + id + "'", user).ToString();
            }
            else
            {
                this.Redirect("/bm/index.html");
                return "";
            }
        }

        [HttpPut]
        [Route("/editPassword")]
        [Authentication]
        public string Put(string oldpwd, string newpwd)
        {
            oldpwd = oldpwd.MD5();
            newpwd = newpwd.MD5();
            var id = HttpContext.Session.GetString("id");
            var n = this._db.ExecuteScalar<long>("select count(1) from admin where id='" + id + "' and password='" + oldpwd + "'");
            if (!string.IsNullOrWhiteSpace(id) && n == 1)
            {
                return this._db.Execute("update admnTable set pwd='" + newpwd + "' where id='" + id + "'").ToString();
            }
            else
            {
                return "密码错误";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authentication]
        public int Post([FromBody]admin user)
        {
            try
            {
                user.pwd = user.pwd.MD5();
                var i = this._db.Execute("insert into adminTable(username,pwd,email)values(@username,@pwd,@email)", user);
                if (i > 0) return 1;
                else return -1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateData")]
        [Authentication]
        public long updateData(string id, [FromBody]JObject list)
        {
            var passwd= list["pwd"].ToString();
            if (!string.IsNullOrWhiteSpace(passwd))
                list["pwd"] = passwd.MD5();
            var sql= uilt.uiltT.Update(list, "adminTable", " where id='" + id + "' ");
            return this._db.Execute(sql);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [Authentication]
        public long delete(string id)
        {
             string sql=uilt.uiltT.Delete("adminTable", " id='" + id + "' ");
            return this._db.Execute(sql);
        }
    }
}
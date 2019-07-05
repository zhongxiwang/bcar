using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using bcar.model;
using bcar.service;
using Dapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using NETCore.Encrypt.Extensions;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class wxDataController : ControllerBase
    {
        TokenService token { get; set; }
        readonly ILog _log;
        readonly IDbConnection _db;
        readonly userCache usc;

        Dictionary<string, string> list = new Dictionary<string, string>()
        {
            {"share","/share/Share.html" },
            {"driver","/share/rect.html" },
            {"qrcode","/share/Ffens.html" },
            {"shagepage","/share/sharepage.html" },
            {"fm","/share/fearnMoney.html" },
            {"xc","/share/myTrip.html" },
            {"reg","/diriver/registerInfo.html" },
            {"sleep","/diriver/sleep.html" }
        };
        public wxDataController(TokenService token, ILog log, IDbConnection con, userCache uc)
        {
            this.token = token;
            this._db = con;
            this._log = log;
            this.usc = uc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public RedirectResult Setopenid(string id, string code)
        {
            webToken webtoken = new webToken(this.token.Secret, this.token.Coropid, code);
            string t = webtoken;
            HttpContext.Session.SetString("openid", webtoken.openid);
            var n = this._db.QueryFirstOrDefault<userinfo>("select * from userinfo  where wxCount='" + webtoken.openid + "' ");
            if (n == null)
            {
                var usedata = WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + this.token + "&openid=" + webtoken.openid + "&lang=zh_CN");
                register(webtoken.openid);
            }
            else if (id.Equals("driver"))
            {
                var driverstate = this._db.ExecuteScalar("select driverstate from userinfo LEFT JOIN driverinfo  on userinfo.id=driverinfo.userid where wxCount='" + webtoken.openid + "' ");
                if (driverstate == null) id = "reg";
                else if((int)driverstate!=1)id = "sleep";
            }
            else if(id.Equals("shagepage"))
            {
                string key = webtoken.openid+webtoken.openid.MD5();
                return Redirect(list[id] + "?key=" + key+"&iss="+n.proxy);
            }else if (id.Equals("share"))
            {
                if (n.mobile==null) n.mobile = "";
                return Redirect(list[id] + "?mo=" + n.mobile);
            }
            return Redirect(list[id]);
        }

        [HttpGet]
        [Route("openid")]
        public string openid()
        {
            return HttpContext.Session.GetString("openid");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getShareQrcode")]
        public string getShareQrcode(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return "";
            string openid = id.Substring(0, id.Length - 32);
            try
            {
                var ui = this.usc.read(openid);
                return ui.qrCode;
            }
            catch
            {
                return null;
            }
        }

        private void register( string wxcount)
        {
            var result = WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + this.token + "&openid=" + wxcount + "&lang=zh_CN").Result;
            var json = JObject.Parse(result);
            var userwxinfo = new userinfo();
            userwxinfo.headimgurl = json["headimgurl"].ToString();
            userwxinfo.username = json["nickname"].ToString();
            userwxinfo.wxCount = json["openid"].ToString();
            userwxinfo.recommender = json["qr_scene_str"].ToString();
            userwxinfo.qrCode = qrurl(wxcount);
            if (userwxinfo.recommender != null)
            {
                var res = this.usc.read(userwxinfo.recommender);
                if (res != null) userwxinfo.userid = res.id;
            }
            this._db.ExecuteScalar(userwxinfo.Insert());
        }


        private string qrurl(string wxcount)
        {
            string s = "{ \"action_name\": \"QR_LIMIT_STR_SCENE\",\"action_info\": { \"scene\": { \"scene_str\": \""   + wxcount + "\"} }";
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token;
            var result = WxUilt.Request.Post(url, s).Result;
            JObject json = JObject.Parse(result);
            var ticket = HttpUtility.UrlEncode(json["ticket"].ToString());
            return "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + ticket;
        }
    }
}
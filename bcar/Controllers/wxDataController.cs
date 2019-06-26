using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using bcar.model;
using bcar.service;
using Dapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            {"fm","/share/fearnMoney.html" },
            {"xc","/share/myTrip.html" },
            {"reg","/diriver/register.html" },
            {"sleep","/diriver/sleep.html" }
        };
        public wxDataController(TokenService token, ILog log, IDbConnection con, userCache uc)
        {
            this.token = token;
            this._db = con;
            this._log = log;
            this.usc = uc;
        }

        [HttpGet("{id}")]
        public RedirectResult Setopenid(string id, string code)
        {
            webToken webtoken = new webToken(this.token.Secret, this.token.Coropid, code);
            string t = webtoken;
            HttpContext.Session.SetString("openid", webtoken.openid);
            var n = this._db.ExecuteScalar<long>("select count(1) from userinfo  where wxCount='" + webtoken.openid + "' ");
            if (n != 1)
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
            return Redirect(list[id]);
        }

        [HttpGet]
        [Route("openid")]
        public string openid()
        {
            return HttpContext.Session.GetString("openid");
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
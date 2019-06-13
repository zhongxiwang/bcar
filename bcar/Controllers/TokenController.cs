using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using bcar.service;
using bcar.Socket;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    /// <summary>
    /// token开发
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class TokenController : ControllerBase
    {
        TokenService token { get; set; }

        static Dictionary<string, webToken> webtokenList = new Dictionary<string, webToken>();
        readonly ILog _log;
        public TokenController(TokenService token, ILog log)
        {
            this.token = token;
            this._log = log;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return token;
        }
        /// <summary>
        /// js api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("apiticket")]
        public string jsapi()
        {
            var result= WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + this.token + "&type=jsapi").Result;
            JObject obj = JObject.Parse(result);
            return obj["ticket"].ToString();
        }
        /// <summary>
        /// web Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("webtoken")]
        public Task<string> webToken(string code, string openid)
        {
             if (openid.Equals("undefined")|| openid.Equals("null")) openid = "";
            var tmp= HttpContext.Session.GetString("openid");
            if (tmp != null) openid = tmp;
            if (!string.IsNullOrEmpty(openid))
            {
                HttpContext.Session.SetString("openid", openid);
                var ts= WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + this.token + "&openid=" + openid + "&lang=zh_CN");
                this._log.Info(ts.Result);

                return ts;
            }
            webToken webtoken = new webToken(this.token.Secret, this.token.Coropid, code);
            string t= webtoken;
            HttpContext.Session.SetString("openid", webtoken.openid);
            if (webtokenList.ContainsKey(webtoken.openid))
                webtokenList[webtoken.openid] = webtoken;
            return WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + this.token + "&openid=" + webtoken.openid + "&lang=zh_CN");
        }

        /// <summary>
        /// 获取用户共享二维码
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShareQrCode")]
        public string ShareQrCode(string openid)
        {
            string s = "{ \"action_name\": \"QR_LIMIT_STR_SCENE\",\"action_info\": { \"scene\": { \"scene_str\": \"" + openid + "\"} }";
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token;
            var result = WxUilt.Request.Post(url, s).Result;
            JObject json = JObject.Parse(result);
            var ticket= HttpUtility.UrlEncode(json["ticket"].ToString());
            return "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + ticket;
        }
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="message"></param>
        [HttpPost]
        [Route("SendMessage")]
        [Authentication]
        public void SendMessage(string openid)
        {
            var message= this.Request.Form["message"];
            uilt.uiltT.SendWxMessage(this.token, message, openid);
        }



    }
}
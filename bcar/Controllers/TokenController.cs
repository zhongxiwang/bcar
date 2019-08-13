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
        jsapiTokencs _jsapi { get; set; }
        static Dictionary<string, webToken> webtokenList = new Dictionary<string, webToken>();
        readonly ILog _log;
        public TokenController(TokenService token, ILog log,jsapiTokencs jsapi)
        {
            this.token = token;
            this._log = log;
            this._jsapi = jsapi;
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
        [Route("/apiticket")]
        public string jsapi(string url)
        {
            return this._jsapi.signature(url);
        }
        /// <summary>
        /// web Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("webtoken")]
        public Task<string> webToken()
        {
            var wxcount = HttpContext.Session.GetString("openid");
            return WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + this.token + "&openid=" + wxcount + "&lang=zh_CN");
        }

        /// <summary>
        /// 获取用户共享二维码
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShareQrCode")]
        public string ShareQrCode()
        {
            var wxcount = HttpContext.Session.GetString("openid");
            string s = "{ \"action_name\": \"QR_LIMIT_STR_SCENE\",\"action_info\": { \"scene\": { \"scene_str\": \"" + wxcount + "\"} }";
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
        public void SendMessage(string openid, [FromBody]JObject obj)
        {
            var message = "";
            try
            {
                message=this.Request.Form["message"];
            }
            catch
            {
                message = obj["message"].ToString();
            }
            uilt.uiltT.SendWxMessage(this.token, message, openid);
        }

    }
}
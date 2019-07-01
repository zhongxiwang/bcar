using bcar.model;
using NETCore.Encrypt.Extensions;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{
    public class jsapiTokencs
    {
        TokenService _token;
        DateTime createTime = new DateTime();
        string token = null;
        public string JsApiToken { get { if (createTime.AddSeconds(Scoend) < DateTime.Now||token == null) getTicket(); return token; } }
        int Scoend;
        public jsapiTokencs(TokenService token)
        {
            this._token = token;
        }
        private void getTicket()
        {

            var tasks = RetryPolicy.Handle<IOException>().WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
             {
             });
            tasks.Execute(() =>
            {
                string result = WxUilt.Request.Get("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + this._token + "&type=jsapi").Result;
                JObject obj = JObject.Parse(result);
                token=obj["ticket"].ToString();
                createTime = DateTime.Now;
                Scoend = int.Parse(obj["expires_in"].ToString());
            });
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string noncestr { get { return "Wm3WZYTPz0wzccnW"; } }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get { return "1414587457"; } }
        /// <summary>
        /// 获取 signature
        /// </summary>
        /// <returns></returns>
                public string signature(string url)
        {
            string str = "jsapi_ticket=" + JsApiToken + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
            string s = "jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg&noncestr=Wm3WZYTPz0wzccnW&timestamp=1414587457&url=http://mp.weixin.qq.com?params=value";
            return str.SHA1().ToLower();
        }
    }
}

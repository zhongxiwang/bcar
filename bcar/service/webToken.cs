using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WxUilt;

namespace bcar.service
{
    public class webToken
    {
        public webToken(string Secret, string appid,string code)
        {
            this.Secret = Secret;
            this.Coropid = appid;
            this.code = code;
        }
        public string code { get; set; }

        public String Coropid { get; set; }
        /// <summary>
        /// token 时长
        /// </summary>
        public int TimeLong { get; set; }
        /// <summary>
        /// 上次获取
        /// </summary>
        public DateTime LastTime { get; set; }

        public string Access_token { get; set; }

        public string Secret { get; set; }

        public string refresh_token { get; set; }
        public string openid { get; set; }

        private void GetTokenAsync()
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid="+this.Coropid+"&secret="+this.Secret+"&code="+this.code+"&grant_type=authorization_code";
            var policy = RetryPolicy.Handle<Exception>().WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {

                ///错误日志
            });
            policy.Execute(() =>
            {
                var res = Request.Get(url).Result;
                if (res == null) throw new Exception("token 获取失败");
                JObject obj = JObject.Parse(res);

                if (obj["errcode"] == null)
                {
                    LastTime = DateTime.Now;
                    Access_token = obj["access_token"].ToString();
                    TimeLong = int.Parse(obj["expires_in"].ToString());
                    openid = obj["openid"].ToString();
                    refresh_token = obj["refresh_token"].ToString();
                }
                else throw new Exception(obj["errcode"].ToString());
            });
        }
        /// <summary>
        /// 获取用户openid
        /// </summary>
        /// <returns></returns>
        public string getOpenid()
        {
            if (this.openid == null) GetTokenAsync();
            return openid;
        }

        public static implicit operator string(webToken tokens)
        {
            if (tokens.LastTime.Add(TimeSpan.FromSeconds(tokens.TimeLong)) <DateTime.Now)
            {
                tokens.GetTokenAsync();
            }
            //var result= Request.Get("https://api.weixin.qq.com/sns/userinfo?access_token=" + tokens.Access_token + "&openid=" + tokens.openid + "&lang=zh_CN").Result;
            return tokens.Access_token;
        }
    }
}

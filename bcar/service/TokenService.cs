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
    public class TokenService
    {
        public TokenService(string Secret, string appid)
        {
            this.Secret = Secret;
            this.Coropid = appid;
        }
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

        private void GetTokenAsync()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&secret="+ Secret+"&appid=" + this.Coropid;
            var policy = RetryPolicy.Handle<Exception>().WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {

                ///错误日志
            });
            policy.Execute(() =>
            {
                var res = Request.Get(url).Result;
                if (res == null) throw new Exception("token 获取失败");
                JObject obj = JObject.Parse(res);

                if (obj["errcode"]==null)
                {
                    LastTime = DateTime.Now;
                    Access_token = obj["access_token"].ToString();
                    TimeLong = int.Parse(obj["expires_in"].ToString());
                }
                else throw new Exception(obj["errcode"].ToString());
            });
        }

        public static implicit operator string(TokenService tokens)
        {
            if (tokens.LastTime.Add(TimeSpan.FromSeconds(tokens.TimeLong)) < DateTime.Now)
            {
                tokens.GetTokenAsync();
            }
            return tokens.Access_token;
        }
    }
}

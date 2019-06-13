using MySql.Data.MySqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using bcar.model;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace bcar.service
{
    public class DataJob : IJob
    {
        
        static TokenService token = new TokenService(Startup.GlobConfiguration["appsecret"], Startup.GlobConfiguration["AppID"]);
        public Task Execute(IJobExecutionContext context)
        {
            IConfiguration dic = (IConfiguration)context.JobDetail.JobDataMap.Get("Config");
            Dictionary<string, orderinfo> list = (Dictionary<string, orderinfo>)context.JobDetail.JobDataMap.Get("datalist");
            var t= Startup.GlobConfiguration["mysql"];
            
            JObject obj = new JObject();
            obj.Add("state", 3);
            var lists= list.ToArray();
            if (list.Count>0)
            foreach (var key in lists)
            {
                if (key.Value.startDate <= DateTime.Now)
                {
                    string sql = uilt.uiltT.Update(obj, "orders", " where id='" + key.Key + "' ");
                        //db.Execute(sql);
                    uilt.uiltT.SendWxMessage(token, "您的订单从"+key.Value.startingPoint+"到"+key.Value.endingPoint+"的行程，由于长时间没有司机接单已经超时，请重新创建行程。", key.Value.openid);
                        list.Remove(key.Key);
                }
            }
            return Task.CompletedTask;
        }
    }
}

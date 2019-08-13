using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using bcar.model;
using bcar.service;
using bcar.Socket;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NETCore.Encrypt.Extensions;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using WxPayAPI;

namespace bcar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Task.Run(async () =>
            //{
            //    var str = await CorpPayAsync(100);
            //});
            var host= CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                            .UseKestrel(options =>//设置Kestrel服务器
                            {
                                options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                                {
                                    //填入之前iis中生成的pfx文件路径和指定的密码　　　　　　　　　　　　
                                    listenOptions.UseHttps(@"E:\bcar\bcar\2351339_www.pbaike.top.pfx", "Cfyqp3Rr");
                                });
                            });
    

    public static async Task<string> CorpPayAsync(int num)
        {
            var openid = "oO8Kd1bx8pPV6dGkOI7KILsrmPLY";
            var f = (double)num / 100;
            if (openid != null)
            {
                JsApiPay jsApiPay = new JsApiPay(null);
                jsApiPay.openid = openid;
                jsApiPay.total_fee = num;
                //JSAPI支付预处理
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetCompanycParameters("提现");

                    //在页面上显示订单信息
                    var info = unifiedOrderResult.ToPrintStr();
                    //System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
                    var xml= unifiedOrderResult.ToXml();
 
                    //HttpContent hc = new StringContent(unifiedOrderResult.ToXml());
                    var str = HttpService.Post(unifiedOrderResult.ToXml(), "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers", true, 3);
                    //var messageTask =await http.PostAsync("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers", hc);
                    JObject obj = new JObject();
                    //obj.Add("bill", uc.bill.ToString());
                    return str;// await messageTask.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return "下单失败";
                }
            }
            return null;
        }
    }
}

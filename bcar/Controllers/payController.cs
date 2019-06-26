using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using bcar.service;
using bcar.Socket;
using bcar.uilt;
using bcar.wxpay;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WxPayAPI;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authentication]
    [EnableCors("any")]
    public class payController : ControllerBase
    {
        IDbConnection db { get; set; }
        userCache userc { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public payController(IDbConnection db, userCache uc)
        {
            this.db = db;
            this.userc = uc;
        }

        [HttpGet]
        public string Pays(int fee)
        {
            this.HttpContext.Session.SetInt32("fee",fee);
            var openid= HttpContext.Session.GetString("openid");
            if (openid != null)
            {
                //string url = "http://paysdk.weixin.qq.com/example/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee;
                //Response.Redirect(url);
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.openid = openid;
                jsApiPay.total_fee = fee;// int.Parse(total_fee);

                //JSAPI支付预处理
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    var wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                    //在页面上显示订单信息
                    var info = unifiedOrderResult.ToPrintStr();
                    return wxJsApiParam;
                }
                catch (Exception ex)
                {
                    return "下单失败";
                }
            }
            return "-1";
        }

        [HttpGet]
        [Route("callback")]
        public object backCallMessageGet()
        {
            return backCall();
        }


        [HttpPost]
        [Route("callbacks")]
        public object backCallMessagePost() 
        {
            return backCall();
        }

        /// <summary>
        /// 企业付款(请求需要双向证书)
        /// 企业付款业务是基于微信支付商户平台的资金管理能力，为了协助商户方便地实现企业向个人付款，
        /// 针对部分有开发能力的商户，提供通过API完成企业付款的功能。 比如目前的保险行业向客户退保、给付、理赔。
        /// 企业付款将使用商户的可用余额，需确保可用余额充足。查看可用余额、充值、提现请登录商户平台“资金管理”进行操作。https://pay.weixin.qq.com/ 
        /// 注意：与商户微信支付收款资金并非同一账户，需要单独充值。
        /// </summary>
        /// <param name="json">企业支付数据</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/CorpPay")]
        public async Task<string> CorpPayAsync(int num)
        {
            var openid =  HttpContext.Session.GetString("openid");
            var uc= this.userc.read(openid);
            var f = (double)num / 100;
            if (uc.bill < f) return "输入金额过大";
            uc.bill -= f;
            if (openid != null)
            {
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.openid = openid;
                jsApiPay.total_fee = num;
                //JSAPI支付预处理
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetCompanycParameters("提现");
                    
                    //在页面上显示订单信息
                    var info = unifiedOrderResult.ToPrintStr();
                    System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
                    HttpContent hc =new StringContent(unifiedOrderResult.ToXml());
                    var str= HttpService.Post(unifiedOrderResult.ToXml(), "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers", true, 3000);
                    //var messageTask =await http.PostAsync("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers", hc);
                    JObject obj = new JObject();
                    obj.Add("bill", uc.bill.ToString());
                    var res = this.db.Execute(uiltT.Update(obj, "userinfo", "where id=" + uc.id));
                    return str;// await messageTask.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return "下单失败";
                }
            }
            return null;
        }

        private string backCall()
        {
            var t= this.HttpContext;
            return "";
        }
    }
}
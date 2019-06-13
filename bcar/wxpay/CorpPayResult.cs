using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.wxpay
{
    /// <summary>
    /// 
    /// </summary>
    public class CorpPayResult : PageResult
    {
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string mch_appid { get; set; }

        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 商户使用查询API填写的单号的原路返回. 
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 企业付款成功，返回的微信订单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 企业付款成功时间
        /// </summary>
        public string payment_time { get; set; }
    }

}

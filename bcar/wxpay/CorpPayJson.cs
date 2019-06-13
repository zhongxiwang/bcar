using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.wxpay
{
    public class CorpPayJson
    {
        public CorpPayJson()
        {
            this.check_name = "NO_CHECK";
        }

        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 校验用户姓名选项,可以使用 PayCheckName枚举对象获取名称
        /// NO_CHECK：不校验真实姓名 
        /// FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） 
        /// OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
        /// </summary>
        public string check_name { get; set; }

        /// <summary>
        /// 收款用户真实姓名。 
        /// 如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名
        /// </summary>
        public string re_user_name { get; set; }

        /// <summary>
        /// 企业付款金额，单位为分
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 企业付款操作说明信息。必填。
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 调用接口的机器Ip地址
        /// </summary>
        public string spbill_create_ip { get; set; }
    }
}

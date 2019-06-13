using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class orderinfo
    {
        public orderinfo() { createTime = DateTime.Now; }

        /// <summary>
        /// 联系号码
        /// </summary>
        public string rider { get; set; }

        /// <summary>
        /// 上车点
        /// </summary>
        public string startingPoint { get; set; }

        /// <summary>
        /// 目标地
        /// </summary>
        public string endingPoint { get; set; }

        /// <summary>
        /// 乘坐时间
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// 乘坐人数
        /// </summary>
        public int personNum { get; set; }

        /// <summary>
        /// 同意协议
        /// </summary>
        public bool xy { get; set; }

        /// <summary>
        /// 预算价格
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// 司机收益
        /// </summary>
        public double driverprice { get; set; }

        /// <summary>
        /// 专车类型，舒适5做
        /// </summary>
        public string carT { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public int riderType { get; set; }

        /// <summary>
        /// 收货人称呼
        /// </summary>
        public string addresseeCall { get; set; }

        /// <summary>
        /// 收获地址手机号码
        /// </summary>
        public string addresseeMobile { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string headimgurl { get; set; }

        /// <summary>
        /// 微信用户名
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 微信id
        /// </summary>
        public string openid { get; set; }

        public string id { get; set; }

        public string routeid { get; set; }

        public DateTime createTime { get;private set; }

        public static implicit operator Hup.MessageBus.Request(orderinfo ts)
        {
            return Hup.CreateMsg.CreateRequest(ts, "riderType" + ts.riderType);
        }
    }
}

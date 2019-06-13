using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 用户下单
    /// </summary>
    public class userOrder
    {
        /// <summary>
        /// 乘车人电话
        /// </summary>
        public string rider { get; set; }
        /// <summary>
        /// 开始点
        /// </summary>
        public string startingPoint { get; set; }
        /// <summary>
        /// 结束点
        /// </summary>
        public string endingPoint { get; set; }
        /// <summary>
        /// 乘车时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public string personNum { get; set; }
        /// <summary>
        /// 接收协议
        /// </summary>
        public bool xy { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string carT { get; set; }
        /// <summary>
        /// 乘车类型
        /// </summary>
        public string riderType { get; set; }
        /// <summary>
        /// 守护人名称
        /// </summary>
        public string addresseeCall { get; set; }
        /// <summary>
        /// 收货人手机号码
        /// </summary>
        public string addresseeMobile { get; set; }
    }
}

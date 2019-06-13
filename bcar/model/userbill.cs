using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 用户账单
    /// </summary>
    public class userbill
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 账单id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 司机id
        /// </summary>
        public string driverid { get; set; }
        /// <summary>
        /// 司机当时信息
        /// </summary>
        public string driverInfo { get; set; }
        /// <summary>
        /// 上车地点
        /// </summary>
        public string startlocation { get; set; }
        /// <summary>
        /// 下车地点
        /// </summary>
        public string endlocation { get; set; }
        /// <summary>
        /// 实际收费
        /// </summary>
        public string actualprice { get; set; }
        /// <summary>
        /// 状态  0 表示为待出行，-1 表示取消，1 表示完成交易， 2 表示正在进行，3表示待付费
        /// </summary>
        public string state { get; set; }
    }
}

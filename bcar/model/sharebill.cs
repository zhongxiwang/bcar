using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 流水记录
    /// </summary>
    public class sharebill
    {
        /// <summary>
        /// 流水id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 微信用户名
        /// </summary>
        public string wxcount { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime { get; set; }

        public string formatTime { get { return createtime.ToString("yyyy年MM月dd日"); } }
        /// <summary>
        /// 费用
        /// </summary>
        public int price { get; set; }
        
        /// <summary>
        /// 订单id
        /// </summary>
        public int billid { get; set; }
        /// <summary>
        /// 分享数据表,0 用户用车提取收益，1 司机收益提取，2 代理商收益提取，3 提现
        /// </summary>
        public int opertype { get; set; }
    }
}

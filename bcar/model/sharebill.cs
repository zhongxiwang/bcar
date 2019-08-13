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
        public string id { get; set; }
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
        public double price { get { return pr; } set { pr = Math.Round(value, 2, MidpointRounding.AwayFromZero); } }
        double pr;
        /// <summary>
        /// 订单id
        /// </summary>
        public string billid { get; set; }
        /// <summary>
        /// 分享数据表,0 用户用车提取收益，1 司机收益提取，2 代理商收益提取，3 提现
        /// </summary>
        public int opertype { get; set; }

        public string Insert()
        {
            string str = "insert into sharebill(userid,wxcount,username,price,billid,opertype)values(@userid,@wxcount,@username,@price,@billid,@opertype)";
            return str;
        }
    }
}

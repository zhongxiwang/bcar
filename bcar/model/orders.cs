using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 账单
    /// </summary>
    public class orders
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 账单id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 司机id
        /// </summary>
        public string driverid { get; set; }
        /// <summary>
        /// 司机信息
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
        /// 状态 0 表示为待出行，-1 表示取消，1 表示完成交易， 2 表示正在进行，3 表示待付费 4 表示司机等待
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 用户支付费用
        /// </summary>
        public float userprice { get; set; }
        /// <summary>
        /// 司机收到费用
        /// </summary>
        public float driverprice { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 司机到达接送地点到达时间
        /// </summary>
        public DateTime arrivalTime { get; set; }

        /// <summary>
        /// 订单初始信息
        /// </summary>
        public string ordersInfo { get; set; }

        /// <summary>
        /// 1专车 2 顺风车 3速递 4 快车
        /// </summary>
        public int orderType { get; set; }

        /// <summary>
        /// 路线id
        /// </summary>
        public int routeid { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            string key = "";
            string value = "";
            var properties = this.GetType().GetProperties();
            foreach (var pro in properties)
            {
                var tmpvalue= pro.GetValue(this);
                if (tmpvalue == null) continue;
                var res = "";
                var dt = new DateTime();
                if (tmpvalue.GetType() == typeof(DateTime))
                    if (((DateTime)tmpvalue) == dt) continue;
                    else
                    {
                        res = ((DateTime)tmpvalue).ToString("yyyy-MM-dd hh:mm");
                    }
                else res = tmpvalue.ToString();
                key += pro.Name + ",";
                value += "'" + res + "',";
            }
            return "insert into orders(" + key.TrimEnd(',') + ") values (" + value.TrimEnd(',') + ")";
        }
        
    }
}

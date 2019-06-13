using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 创建司机信息
    /// </summary>
    public class driverinfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 称呼
        /// </summary>
        public string usercall { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string carcard { get; set; }
        /// <summary>
        /// 路线
        /// </summary>
        public string route { get; set; }
        /// <summary>
        /// 车类型，舒适5座位
        /// </summary>
        public string carType { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public string seatNum { get; set; }
        /// <summary>
        /// 车颜色
        /// </summary>
        public string carColor { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string moblie { get; set; }

        /// <summary>
        /// 身份证附件id
        /// </summary>
        public string idcard { get; set; }

        /// <summary>
        /// 驾驶证附件id
        /// </summary>
        public string driverlicense { get; set; }
        /// <summary>
        /// 行使证附件id
        /// </summary>
        public string drivinglicense { get; set; }
        /// <summary>
        /// 车辆侧面照证附件id
        /// </summary>
        public string carimage { get; set; }
        /// <summary>
        /// 司机验证状态
        /// </summary>
        public int driverstate { get; set; }

        /// <summary>
        /// 行驶证
        /// </summary>
        public string operationcertificate{get;set;}
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            string key = "";
            string value = "";
            var properties= this.GetType().GetProperties();
            foreach(var pro in properties)
            {
                key += pro.Name + ",";
                value += "'" + pro.GetValue(this) + "',";
            }
            return "insert into driverinfo(" + key.TrimEnd(',') + ") values (" + value.TrimEnd(',') + ")";
        }
    }
}

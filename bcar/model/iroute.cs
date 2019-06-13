using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 路线
    /// </summary>
    public class iroute
    {
        /// <summary>
        /// 开始地址
        /// </summary>
        public string startLocation { get; set; }
        /// <summary>
        /// 终点站
        /// </summary>
        public string endLocation { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 路线
        /// </summary>
        public string routeName { get { return startLocation + "到" + endLocation; } }


        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            return "insert into iroute(startLocation,endLocation)values('" + startLocation+"','"+ endLocation+"')";
        }
    }
}

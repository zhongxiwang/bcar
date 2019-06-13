using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    public class locationSite
    {
        /// <summary>
        /// 路线id
        /// </summary>
        public int irouteid { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string sitename { get; set; }
        /// <summary>
        /// 到下一站的距离
        /// </summary>
        public string nxdistance { get; set; }
        /// <summary>
        /// 站点位置序号
        /// </summary>
        public int lindex { get; set; }

        /// <summary>
        /// 插入语句
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            return "insert into locationSite(irouteid,sitename,nxdistance,lindex)values('" + this.irouteid + "','" + this.sitename + "','" + this.nxdistance + "','" + this.lindex + "')";
        }
    }
}

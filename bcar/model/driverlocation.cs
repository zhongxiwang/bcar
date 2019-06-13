using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 司机位置信息
    /// </summary>
    public class driverlocation
    {
        /// <summary>
        /// 搜索标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 司机信息正文
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// x轴
        /// </summary>
        public double pointx { get; set; }
        /// <summary>
        /// y轴
        /// </summary>
        public double pointy { get; set; }

        /// <summary>
        /// 1 表示能够接单，0表示单还没完成
        /// </summary>
        public int status { get; set; }
    }
}

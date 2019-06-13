using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class upload
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string uptime { get; set; }

        public upload()
        {
            Random r = new Random();
            var n= r.Next(99);
            id= DateTime.Now.ToString("yyyyMMddhhmmss") + n;
        }
        /// <summary>
        /// 执行插入
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            return "insert into upload(id,filename)values('"+id+"','"+filename+"')";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.model
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public class messages
    {
        /// <summary>
        /// 接收用户id
        /// </summary>
        public int recuser { get; set; }
        /// <summary>
        /// 发送用户
        /// </summary>
        public int senduser { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 消息类型 消息类型默认是分享收益消息
        /// </summary>
        public int messageType { get; set; }
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            return "insert into message(recuser,senduser,message,messageType)values("+recuser+ "," + senduser + ",'" + message + "'," + messageType + ")";
        }
    }
}

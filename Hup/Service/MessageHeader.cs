using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hup.MessageBus.Service
{
    /// <summary>
    /// 消息头
    /// </summary>
    class MessageHeader
    {
        /// <summary>
        /// 消息key
        /// </summary>
        public string MessageKey { get; set; }

        /// <summary>
        /// 请求key，也可以称为请求服务的方法名
        /// </summary>
        public string RequestKey { get; set; }
    }
}

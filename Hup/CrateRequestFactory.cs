using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hup.MessageBus
{
    public class CrateRequestFactory
    {
        /// <summary>
        /// 创建一个Message客户端的请求
        /// </summary>
        /// <returns>RequestClientType</returns>
        public static RequestType CreateRequestClientTypeForMessage()
        {
            return new RequestType { type = RequestType.Message };
        }
        /// <summary>
        /// 创建一个File客户端的请求
        /// </summary>
        /// <returns>RequestClientType</returns>
        public static RequestType CreateRequestClientTypeForFile()
        {
            return new RequestType { type = RequestType.File };
        }
    }
}
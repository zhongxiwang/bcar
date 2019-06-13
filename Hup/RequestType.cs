using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hup.MessageBus
{
    public class RequestType
    {
        /// <summary>
        /// Message
        /// </summary>
        public const string Message = "Msg";
        /// <summary>
        /// File
        /// </summary>
        public const string File = "File";
        /// <summary>
        /// 其他方式
        /// </summary>
        public string Other { get; set; }
        /// <summary>
        /// 目前请求类型
        /// </summary>
        public string type { get; set; }
        public static implicit operator string(RequestType t)
        {
            if (t.Other != null) return t.Other;
            else return t.type;
        }
        public static implicit operator RequestType(string s)
        {
            return new RequestType() { Other = s };
        }

    }
}
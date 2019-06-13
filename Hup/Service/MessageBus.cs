using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hup.MessageBus.Service
{
    /// <summary>
    /// 消息总线对象
    /// </summary>
    public class MessageBus
    {
        /// <summary>
        /// 发送到Bus并且同步执行该亲求
        /// </summary>
        /// <typeparam name="TResult">返回参数类型</typeparam>
        /// <param name="parmeter">参数实例</param>
        /// <returns></returns>
        public static Request SendBusAndAction(Request parmeter)
        {

                //首先根据请求key所获取该请求对应的服务处理句柄
                ServiceOperationHandler handler = ServiceRouting.GlobalRouting.GetRequestOperationHandle(parmeter.Head["RequestKey"]);
                //使用参数带出返回值
                Request result = new Request();
            //parmeter.AddService?.Invoke();
            if (handler == null) return null;
                handler(parmeter, ref result);
                if (result == null) return null;
            return result;
        }

    }
}

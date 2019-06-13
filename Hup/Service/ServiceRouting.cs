using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hup.MessageBus.Service
{
    /// <summary>
    /// 服务路由
    /// </summary>
    class ServiceRouting
    {
        /// <summary>
        /// 获取全局路由
        /// </summary>
        public static ServiceRouting GlobalRouting = new ServiceRouting();
        /// <summary>
        /// 获取请求处理句柄，也就是对应的亲求处理服务
        /// </summary>
        /// <param name="requestkey">服务名称</param>
        /// <returns></returns>
        public ServiceOperationHandler GetRequestOperationHandle(string requestkey)
        {
            return ServiceRegister.GlobalReister.GetOperationHandler(requestkey);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hup.MessageBus.Service
{
    /// <summary>
    /// 服务处理解绑
    /// </summary>
    /// <param name="parameter">参数</param>
    /// <param name="result">返回值</param>
    public delegate void ServiceOperationHandler(Request parameter, ref Request result);
   
    /// <summary>
    /// 服务注册器，用来在总线中注册并且保存了所有已经注册的服务
    /// </summary>
    public class ServiceRegister:Dictionary<string,ServiceOperationHandler>
    {
        /// <summary>
        /// 全局服务注册器
        /// </summary>
        public static ServiceRegister GlobalReister = new ServiceRegister();
        /// <summary>
        /// 根据请求key获取一个请求处理服务句柄
        /// </summary>
        /// <param name="requestkey">服务名</param>
        /// <param name="requestOperation">delegates</param>
        public void Register(string requestkey,ServiceOperationHandler requestOperation)
        {
            if (this.ContainsKey(requestkey))
                this[requestkey] = requestOperation;
            else this.Add(requestkey, requestOperation);
        }
        /// <summary>
        /// 删除注册的服务
        /// </summary>
        /// <param name="requestKey">服务名</param>
        public void RemoveSerice(string requestKey)
        {
            if (this.ContainsKey(requestKey)) Remove(requestKey);
        }

        /// <summary>
        /// 查看服务列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetService()
        {
            return ServiceRegister.GlobalReister.Keys.ToList();
        }
        /// <summary>
        /// 根据请求获取一个处理服务器句柄
        /// </summary>
        /// <param name="requestkey">返回object</param>
        /// <returns></returns>
        public ServiceOperationHandler GetOperationHandler(string requestkey)
        {
            if (this.ContainsKey(requestkey)) return this[requestkey];
            return null;
        }
    }
}

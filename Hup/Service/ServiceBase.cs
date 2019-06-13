using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hup.MessageBus.Service
{
    /// <summary>
    /// 所有服务的基类
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// 开始注册服务
        /// </summary>
        public void Register()
        {
            this.RegisterServieceRequest(ServiceRegister.GlobalReister);
        }
        /// <summary>
        /// 删除注册
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            ServiceRegister.GlobalReister.RemoveSerice(key);
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
        /// 所有的服务都必须实现此方法用来添加服务中所能处理的请求
        /// </summary>
        /// <param name="Register"></param>
        public abstract void RegisterServieceRequest(ServiceRegister Register);
    }
}

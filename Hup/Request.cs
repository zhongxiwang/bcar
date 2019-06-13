using System;
using System.Collections.Generic;

namespace Hup.MessageBus
{
    public class Request 
    {
        private Dictionary<string, string> _head = new Dictionary<string, string>();
        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string,string> Head { get { return _head; } }

        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestType ClientType { get; set; }

        /// <summary>
        /// 请求正文
        /// </summary>
        public dynamic Content{ get; set; }

        //List<string> qs = new List<string>();
        /// <summary>
        /// 接受用户列表
        /// </summary>
        //public List<string> RecUserlist { get { return qs; } set { qs = value; } }
        public List<string> RecUserlist { get; set; }

        /// <summary>
        /// 发送用户
        /// </summary>
        public string SendUser { get; set; }

        /// <summary>
        /// 添加自定义服务类型,再管道中添加，返回路由类型
        /// </summary>
        public Func<string> AddService { get; set; }

        /// <summary>
        /// 设置请求头,存在相同的覆盖
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetHead(string key,string value)
        {
            object o = new object();
            lock (o)
            {
                if (this._head.ContainsKey(key)) this._head[key] = value;
                else this._head.Add(key, value);
            }
        }
        /// <summary>
        /// 获取请求头信息，没有返回null
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public string GetHead(string key)
        {
            if (this._head.ContainsKey(key)) return this._head[key];
            return null;
        }
        public Request()
        {
            RecUserlist = new List<string>();
        }
    }
}
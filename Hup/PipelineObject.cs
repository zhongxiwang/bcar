using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hup.MessageBus
{
    public delegate void ClientPipelingObjectMoudel(Request request);
    public class PipelineObject
    {
        /// <summary>
        /// 管道模块引用
        /// </summary>
        private ClientPipelingObjectMoudel modules;
        /// <summary>
        /// 添加管道中的模块
        /// </summary>
        /// <param name="module"></param>
        public void AddModule(ClientPipelingObjectMoudel module)
        {
            modules += module;
        }
        /// <summary>
        /// 开始管道处理
        /// </summary>
        /// <param name="request"></param>
        public void Runpipeline(Request request)
        {
            modules(request);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hup.MessageBus
{
    /// <summary>
    /// 管道中断的moudle引用
    /// </summary>
    /// <param name="request">request</param>
    public delegate void OperationLogicPipelineObjectMoudles(Request request);

    /// <summary>
    /// 处理管道逻辑对象
    /// </summary>
    public class OperationLogicPipelineObject
    {
        /// <summary>
        /// 管道模块
        /// </summary>
        private OperationLogicPipelineObjectMoudles moudles;
        /// <summary>
        /// 添加模块到管道中
        /// </summary>
        /// <param name="moudle"></param>
        public void Add(OperationLogicPipelineObjectMoudles moudle)
        {
            this.moudles += moudle;
        }
        /// <summary>
        /// 启动管道
        /// </summary>
        /// <param name="request"></param>
        public void Runpipeline(Request request)
        {
            this.moudles(request);
        }
    }
}

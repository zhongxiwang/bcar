using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hup.MessageBus
{
    public class IbuildOperationObject
    {
        /// <summary>
        /// 生成逻辑处理管道接口
        /// </summary>
        public interface IBuildOperationLogicPipelineObject
        {
            /// <summary>
            /// 生成一个复合当前客户端类型的处理管道
            /// </summary>
            /// <param name="request"> request</param>
            /// <returns></returns>
            OperationLogicPipelineObject BuildOperationPipeline(Request request);
        }


        /// <summary>
        /// 
        /// </summary>
        public class BusForMessageType : IBuildOperationLogicPipelineObject
        {
            public OperationLogicPipelineObject BuildOperationPipeline(Request request)
            {
                var result = new OperationLogicPipelineObject();
                result.Add(requestObject =>
                {
                    
                });
                result.Add(requestObject =>
                {
                    
                });
                return result;
            }
        }
        /// <summary>
        /// 文件类型的总线
        /// </summary>
        public class BusForFileType : IBuildOperationLogicPipelineObject
        {
            public OperationLogicPipelineObject BuildOperationPipeline(Request request)
            {
                var result = new OperationLogicPipelineObject();
                result.Add(requestObject =>
                {
                    ///记录.Net请求log
                });
                result.Add(requestObject =>
                {
                    ///执行.Net请求
                });
                return result;
            }
        }

        /// <summary>
        /// 请求客户端类型创建启动模块
        /// </summary>
        public class OperationLogicPipelineObjectFactory
        {
            public static IBuildOperationLogicPipelineObject Create(RequestType clienttype)
            {
                if (clienttype.type == RequestType.Message) return new BusForMessageType();
                if (clienttype.type == RequestType.File) return new BusForFileType();
                return null;
            }
        }
    }
}
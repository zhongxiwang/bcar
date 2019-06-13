using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hup.MessageBus
{
    public class PipelineModules
    {
        /// <summary>
        /// 验证请求正文
        /// </summary>
        /// <param name="request"></param>
        public static void CheckRequestContent(Request request)
        {
            if (request == null || request.Content == null )
                throw new InvalidOperationException("无效请求");
        }
        /// <summary>
        /// 添加请求头
        /// </summary>
        /// <param name="request"></param>
        public static void AddRequestHead(Request request)
        {
            request.SetHead("KeyID",Guid.NewGuid().ToString());
            request.SetHead("RequestKey", request.ClientType.type);
            if (request.ClientType.type == RequestType.File)
                request.SetHead("HeadReadonly", "true");
            if(request.ClientType.Other!=null)
                request.SetHead("RequestKey", request.ClientType.Other);
            if (request.AddService!=null)
                request.SetHead("AddService", request.AddService());
            request.SetHead("ContentType", request.Content.GetType().FullName);
        }
        /// <summary>
        /// 转换请求格式
        /// </summary>
        /// <param name="request"></param>
        public static void TransferRequestFormat(Request request)
        {
            ///request.Content.Content = TransferrequestForRest.Transfer(request.Content.Content);
        }
        /// <summary>
        /// 压缩亲求
        /// </summary>
        /// <param name="request"></param>
        public static void ReduceRequest(Request request)
        {
            // ReduceRequestBody.reduce(request);
        }

    }
}
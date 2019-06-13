using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hup.MessageBus;


namespace Hup
{
    public class CreateMsg
    {
        public static Request CreateMsgRequest(string messages)
        {
            var req= CreateRequest(messages, new RequestType() { type = RequestType.Message });
            return req;
        }

        public static Request CreateRequest(object Content,RequestType reqtype)
        {
            Request request = new Request()
            {
                Content = Content,
                ClientType = reqtype
            };
            PipelineObject pipe = new PipelineObject();
            pipe.AddModule(PipelineModules.CheckRequestContent);
            pipe.AddModule(PipelineModules.AddRequestHead);
            pipe.AddModule(PipelineModules.TransferRequestFormat);
            pipe.AddModule(PipelineModules.ReduceRequest);
            pipe.Runpipeline(request);
            //var v = MessageBus.IbuildOperationObject.OperationLogicPipelineObjectFactory.Create(new RequestType() { type = RequestType.Message });
            //var result = v.BuildOperationPipeline(request);//.Runpipeline(request);
            //result.Add(PipelineModules.CheckRequestContent);
            return request;
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Request Run(Request req)
        {
            return Hup.MessageBus.Service.MessageBus.SendBusAndAction(req);
        }
    }
}

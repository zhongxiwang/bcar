using Hup.MessageBus;
using Hup.MessageBus.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcar.service
{
    /// <summary>
    /// 微信消息服务服务
    /// </summary>
    public class MessgeService : Hup.MessageBus.Service.ServiceBase
    {
        public override void RegisterServieceRequest(ServiceRegister Register)
        {
            Register.Register("text", (Request parameter, ref Request result) =>
             {
                 var req= (Request)parameter;
                 req.Head["CreateTime"] = GetCreateTime().ToString();
                 result.Content = CreateMsg(req);
             });
            Register.Register("image", (Request parameter, ref Request result) =>
            {
                var req = (Request)parameter;
                req.Head["CreateTime"] = GetCreateTime().ToString();
                result.Content = CreateMsg(req);
            });
            Register.Register("voice", (Request parameter, ref Request result) =>
            {
                var req = (Request)parameter;
                req.Head["CreateTime"] = GetCreateTime().ToString();
                result.Content = CreateMsg(req);
            });
            Register.Register("video", (Request parameter, ref Request result) =>
            {
                var req = (Request)parameter;
                req.Head["CreateTime"] = GetCreateTime().ToString();
                result.Content = CreateMsg(req);
            });
            Register.Register("location", (Request parameter, ref Request result) =>
            {
                var req = (Request)parameter;
                req.Head["CreateTime"] = GetCreateTime().ToString();
                result.Content = CreateMsg(req);

            });
            Register.Register("link", (Request parameter, ref Request result) =>
            {
                var req = (Request)parameter;
                req.Head["CreateTime"] = GetCreateTime().ToString();
                result.Content = CreateMsg(req);
            });
            Register.Register("event", (Request parameter, ref Request result) =>
            {
                var req = (Request)parameter;
                req.Head["CreateTime"] = GetCreateTime().ToString();
                result.Content = CreateMsg(req);
            });

        }

        private string ToXml()
        {
            return "";
        }
        private string CreateMsg(Request request)
        {
            string msg = "";
            msg = CreateNode("ToUserName", request.SendUser);
            msg += CreateNode("FromUserName", request.RecUserlist[0]);
            msg += CreateNode("MsgType", request.ClientType.Other);
            foreach(var key in request.Head)
            {
                msg += CreateNode(key.Key, key.Value);
            }
            return "<xml>" + msg + "</xml>";

        }
        private string CreateNode(string NodeName,string content)
        {
            return "<" + NodeName + "><![CDATA[" + content + "]]></" + NodeName + ">";
        }
        private int GetCreateTime()//创建时间戳
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);//格林威治时间1970，1，1，0，0，0
            return (int)(DateTime.Now - dateStart).TotalSeconds;
        }
    }
}

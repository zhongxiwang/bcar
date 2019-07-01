using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using bcar.service;
using bcar.Socket;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class VerifyController : ControllerBase
    {
        IConfiguration Conf { get; set; }
        TokenService ts { get; set; }
        public VerifyController(IConfiguration conf, TokenService token)
        {
            this.Conf = conf;
            ts = token;
        }

        /// <summary>
        /// 验证消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("verify")]
        public string Virify()
        {
            var nonce= this.Request.Query["nonce"];
            var timestamp = this.Request.Query["timestamp"];
            var signature = this.Request.Query["signature"];
            var echostr = this.Request.Query["echostr"];
            var token= this.Conf["Token"];
            var AppID = this.Conf["AppID"];
            var EncodingAESKey = this.Conf["EncodingAESKey"];
            string[] temp1 = { token, timestamp, nonce };
            //1. 将token、timestamp、nonce三个参数进行字典序排序
            Array.Sort(temp1);//排序
            string temp2 = string.Join("", temp1);
            var result= NETCore.Encrypt.EncryptProvider.Sha1(temp2);
            return echostr;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("verify")]
        public string VirifyPost()
        {
            bool sisubscribe = false;
            var nonce = this.Request.Query["nonce"];
            var timestamp = this.Request.Query["timestamp"];
            var signature = this.Request.Query["signature"];
            var echostr = this.Request.Query["echostr"];
            var token = this.Conf["Token"];
            var AppID = this.Conf["AppID"];
            var EncodingAESKey = this.Conf["EncodingAESKey"];
            byte[] buffer = new byte[(int)this.Request.ContentLength];
            this.Request.Body.Read(buffer, 0, buffer.Length);
            string result = Encoding.UTF8.GetString(buffer);
            //Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(token, EncodingAESKey, AppID);
            //string sMsg = "";  //解析之后的明文
            //int ret = 0;
            //ret = wxcpt.DecryptMsg(signature, timestamp, nonce, result, ref sMsg);
            //if (ret != 0)
            //{
            //    System.Console.WriteLine("ERR: Decrypt fail, ret: " + ret);
            //}
            //System.Console.WriteLine(sMsg);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            var root= xml.FirstChild;
            Dictionary<string, string> List = new Dictionary<string, string>();
            var req = Hup.CreateMsg.CreateRequest("", "t");
            foreach (XmlNode key in root)
            {
                if (key.Name == "MsgType")
                {
                    req.Head["RequestKey"] = key.InnerText;
                    req.ClientType.Other = key.InnerText;
                }
                else if (key.Name == "ToUserName") req.RecUserlist.Add(key.InnerText);
                else if (key.Name == "FromUserName") req.SendUser = key.InnerText;
                else if(key.Name== "toUser")
                {

                }else if(key.Name== "Event")
                {
                    if (key.InnerText.Equals("subscribe"))
                    {
                        sisubscribe = true;
                    }
                }
                else req.SetHead(key.Name, key.InnerText);
            }
            if (sisubscribe)
            {
                uilt.uiltT.SendWxMessage(this.ts, "出行用百变，打车更方便，，加入全员代理坐享亿万收益！", req.SendUser);
            }
            var x= Hup.CreateMsg.Run(req);
            return x.Content;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using bcar.service;
using bcar.Socket;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("any")]
    [Authentication]
    public class shareBillController : ControllerBase
    {
        public IDbConnection db { get; set; }
        public CostService cost { get; set; }
        public string openid { get; set; }
        public shareBillController(IDbConnection db, CostService cost)
        {
            this.db = db;
            this.cost = cost;
        }

        /// <summary>
        /// 获取用户收益
        /// </summary>
        /// <param name="wxcount">微信openid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/getsy")]
        public IEnumerable<sharebill> getsy( )
        {
            var wxcount = HttpContext.Session.GetString("openid");
            return this.db.Query<sharebill>("select * from sharebill where opertype <> '3' and username='"+wxcount+"'");
            
        }

        /// <summary>
        /// 获取用户收益
        /// </summary>
        /// <param name="wxcount">微信openid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/gettx")]
        public IEnumerable<sharebill> gettx()
        {
            var wxcount = HttpContext.Session.GetString("openid");
            return this.db.Query<sharebill>("select * from sharebill where opertype ='3' and username='" + wxcount + "'");
        }

        /// <summary>
        /// 成为代理商
        /// </summary>
        /// <param name="wxcount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/proxy")]
        public string proxy()
        {
            var wxcount = HttpContext.Session.GetString("openid");
            bool result= this.cost.proxy(wxcount);
            if (result)
            {
                return "成功申请为代理商";
            }
            else
            {
                return "申请失败，请确定账号金额充足";
            }
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/proxycost")]
        public object proxycost()
        {
            return CostService.Config["proxycost"];
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/config")]
        public object Config(string key)
        {
            return CostService.Config[key];
        }
    }
}
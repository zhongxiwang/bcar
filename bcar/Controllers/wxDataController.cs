using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcar.service;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace bcar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class wxDataController : ControllerBase
    {
        TokenService token { get; set; }
        readonly ILog _log;

        Dictionary<string, string> list = new Dictionary<string, string>()
        {
            {"share","/share/Share.html" },
            {"driver","/share/rect.html" },
            {"qrcode","/share/Ffens.html" },
            {"fm","/share/fearnMoney.html" },
            {"xc","/share/myTrip.html" }
        };
        public wxDataController(TokenService token, ILog log)
        {
            this.token = token;
            this._log = log;
        }

        [HttpGet]
        public RedirectResult Setopenid(string code,string type)
        {
            webToken webtoken = new webToken(this.token.Secret, this.token.Coropid, code);
            string t = webtoken;
            HttpContext.Session.SetString("openid", webtoken.openid);
            return Redirect(list[type]);
        }


    }
}
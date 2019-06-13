using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace bcar.Socket
{
    /// <summary>
    /// 验证
    /// </summary>
    public class AuthenticationAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.HttpContext.Session.GetString("id");
            if (string.IsNullOrWhiteSpace(id))
            {
                var openid = filterContext.HttpContext.Session.GetString("openid");
                if (string.IsNullOrWhiteSpace(openid))
                {
                    filterContext.Result = new RedirectToRouteResult("/bm/signin.html");
                    //结果转为自定义消息格式
                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                    // 重新封装回传格式
                    filterContext.HttpContext.Response.StatusCode = 401;
                    return;
                }
            }
        }
    }
}

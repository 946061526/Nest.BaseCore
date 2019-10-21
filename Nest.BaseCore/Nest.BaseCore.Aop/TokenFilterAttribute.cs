
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nest.BaseCore.Cache;
using Nest.BaseCore.Common;
using Nest.BaseCore.Log;
using System;
using System.Linq;

namespace Nest.BaseCore.Aop
{
    /// <summary>
    /// 用户登录Token认证
    /// </summary>
    public class TokenAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 判断是否忽略验证
            if (context.ActionDescriptor is ControllerActionDescriptor cad)
            {
                var controleIgnor = cad.ControllerTypeInfo.GetCustomAttributes(inherit: true).Any(x => x is IgnorTokenAttribute || x is AllowAnonymousAttribute);
                if (controleIgnor)
                    return;
                var actionIgnor = cad.MethodInfo.GetCustomAttributes(inherit: true).Any(x => x is IgnorTokenAttribute || x is AllowAnonymousAttribute);
                if (actionIgnor)
                    return;
            }

            ApiResultModel<string> apiResult = null;
            var path = context.HttpContext.Request.Path;
            var source = context.HttpContext.Request.Headers["source"];//请求来源为微信时，不做token验证
            if (string.IsNullOrEmpty(source) || source != "wx")
            {
                var token = context.HttpContext.Request.Headers["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    //不存在该缓存键
                    if (!RedisClient.Exists(RedisDatabase.DB_UserService, RedisCommon.GetTokenKey(token)))
                    {
                        apiResult = new ApiResultModel<string>() { Code = ApiResultCode.UserInvalid };
                        context.Result = new JsonResult(apiResult);
                        return;
                    }
                }
                else
                {
                    apiResult = new ApiResultModel<string>() { Code = ApiResultCode.NoToken };
                    context.Result = new JsonResult(apiResult);

                    //日志
                    Net4Logger.Error(path, "非法请求(无token)");
                }
            }
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 控制器中加了该属性的方法执行完成后才会来执行该方法。
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }

    /// <summary>
    /// 忽略Token
    /// </summary>
    public class IgnorTokenAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //}
    }
}

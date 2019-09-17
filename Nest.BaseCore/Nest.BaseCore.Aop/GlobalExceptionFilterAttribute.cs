using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nest.BaseCore.Common;
using Nest.BaseCore.Log;
using System;
using System.Net;

namespace Nest.BaseCore.Aop
{
    ///// <summary>
    ///// 统一异常处理
    ///// </summary>
    //public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    //{
    //    private readonly IExceptionlessLogger _exceptionlessLogger;
    //    public GlobalExceptionFilterAttribute(IExceptionlessLogger exceptionlessLogger)
    //    {
    //        _exceptionlessLogger = exceptionlessLogger;
    //    }

    //    public override void OnException(ExceptionContext context)
    //    {
    //        ApiResultModel<string> apiResult = null;
    //        var ex = context.Exception;
    //        if (ex != null)
    //        {
    //            apiResult = new ApiResultModel<string>() { Code = ApiResultCode.Exception, Msg = ex.Message };
    //            context.Result = new JsonResult(apiResult);
    //            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //            context.ExceptionHandled = true;

    //            //日志
    //            Net4Logger.Error(context.HttpContext.Request.Path, ex.Message, ex);
    //            //_exceptionlessLogger.Error(context.HttpContext.Request.Path, ex.Message, "");
    //        }
    //        base.OnException(context);
    //    }
    //}

    /// <summary>
    /// 统一异常处理
    /// </summary>
    public class GlobalExceptionFilterAttribute : IExceptionFilter
    {
        private readonly IExceptionlessLogger _exceptionlessLogger;
        public GlobalExceptionFilterAttribute(IExceptionlessLogger exceptionlessLogger)
        {
            _exceptionlessLogger = exceptionlessLogger;
        }

        public void OnException(ExceptionContext context)
        {
            ApiResultModel<string> apiResult = null;
            var ex = context.Exception;
            if (ex != null)
            {
                apiResult = new ApiResultModel<string>() { Code = ApiResultCode.Exception, Msg = ex.Message };
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;

                //日志
                //Net4Logger.Error(context.HttpContext.Request.Path, ex.Message, ex);
                _exceptionlessLogger.Error(context.HttpContext.Request.Path, ex.Message, "");
            }
        }
    }
}

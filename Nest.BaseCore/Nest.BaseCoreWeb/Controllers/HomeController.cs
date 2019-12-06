using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Models;

namespace Nest.BaseCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 默认错误页面
        /// </summary>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public ActionResult Error(string msg)
        {
            //msg = string.IsNullOrEmpty(msg) ? "" : UrlEncoder.Default.UrlDecode(msg) HttpContext.Server.UrlDecode(msg);
            ViewBag.Message = msg;
            return View();
        }
    }
}

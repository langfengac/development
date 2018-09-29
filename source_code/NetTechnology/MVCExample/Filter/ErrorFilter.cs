using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCExample.Filter
{
    public class ErrorFilterAttribute:HandleErrorAttribute
    {
        //日 志注入
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controllerName = filterContext.RouteData.Values["controller"].ToString();
                string actionName = filterContext.RouteData.Values["action"].ToString();
                //string error = filterContext.RouteData.Values["controller"].ToString();
                if (filterContext.HttpContext.Request.IsAjaxRequest())//检查请求头
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                         
                            PromptMsg = "系统出现异常，请联系管理员",
                            DebugMessage = filterContext.Exception.Message
                        }//这个就是返回的结果
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                }
                filterContext.ExceptionHandled = true;


            }
        }

    }
}
using System;
using System.Web;

namespace MVCExample.Controllers
{
    public class MyModule : IHttpModule
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参阅以下链接: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //此处放置清除代码。
        }

        public void Init(HttpApplication context)
        {
            // 下面是如何处理 LogRequest 事件并为其 
            // 提供自定义日志记录实现的示例
            context.LogRequest += new EventHandler(OnLogRequest);


            context.BeginRequest += new EventHandler(Context_BeginRequest);

            //context.AuthenticateRequest;
            //context.PostAuthenticateRequest;
            //context.AuthorizeRequest;
            //context.PostAuthorizeRequest;


            ////恰好在 ASP.NET 向客户端发送 HTTP 标头之前发生。
            //context.PreSendRequestHeaders;
            ////在选择了用来响应请求的处理程序时发生。
            //MapRequestHandler;
            ////在释放应用程序时发生。
            //Disposed;
            ////在 ASP.NET 响应请求时作为 HTTP 执行管线链中的第一个事件发生。
            //BeginRequest;
            ////当安全模块已建立用户标识时发生。
            //AuthenticateRequest;
            ////当安全模块已建立用户标识时发生。
            //PostAuthenticateRequest;
            ////当安全模块已验证用户授权时发生。
            //AuthorizeRequest;
            ////在当前请求的用户已获授权时发生。
            //PostAuthorizeRequest;
            ////在 ASP.NET 完成授权事件以使缓存模块从缓存中为请求提供服务后发生，从而绕过事件处理程序（例如某个页或 XML Web services）的执行。
            //ResolveRequestCache;
            ////在 ASP.NET 跳过当前事件处理程序的执行并允许缓存模块满足来自缓存的请求时发生。
            //PostResolveRequestCache;
            ////恰好在 ASP.NET 向客户端发送内容之前发生。
            //PreSendRequestContent;
            ////在 ASP.NET 已将当前请求映射到相应的事件处理程序时发生。
            //PostMapRequestHandler;
            ////在 ASP.NET 处理完 System.Web.HttpApplication.LogRequest 事件的所有事件处理程序后发生。
            //PostLogRequest;
            ////当托管对象与已经释放的请求相关联时发生。
            //RequestCompleted;
            ////在已获得与当前请求关联的请求状态（例如会话状态）时发生。
            //PostAcquireRequestState;
            ////恰好在 ASP.NET 开始执行事件处理程序（例如，某页或某个 XML Web services）前发生。
            //PreRequestHandlerExecute;
            ////在 ASP.NET 事件处理程序（例如，某页或某个 XML Web service）执行完毕时发生。
            //PostRequestHandlerExecute;
            ////在 ASP.NET 执行完所有请求事件处理程序后发生。 该事件将使状态模块保存当前状态数据。
            //ReleaseRequestState;
            ////在 ASP.NET 已完成所有请求事件处理程序的执行并且请求状态数据已存储时发生。
            //PostReleaseRequestState;
            ////当 ASP.NET 执行完事件处理程序以使缓存模块存储将用于从缓存为后续请求提供服务的响应时发生。
            //UpdateRequestCache;
            ////在 ASP.NET 完成缓存模块的更新并存储了用于从缓存中为后续请求提供服务的响应后，发生此事件。
            //PostUpdateRequestCache;
            ////恰好在 ASP.NET 为当前请求执行任何记录之前发生。
            //LogRequest;
            ////当 ASP.NET 获取与当前请求关联的当前状态（如会话状态）时发生。
            //AcquireRequestState;
            ////在 ASP.NET 响应请求时作为 HTTP 执行管线链中的最后一个事件发生。
            //EndRequest;
            ////当引发未经处理的异常时发生。
            //Error;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //可以在此处放置自定义日志记录逻辑
        }
    }
}

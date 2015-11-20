using System;
using System.Web;

namespace WINConnect.Web.Attributes
{
    /// <summary>
    /// Implementation Custom Header Module
    /// </summary>
    public class CustomHeaderModule : IHttpModule
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        /// <summary>
        /// OnPreSendRequestHeaders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpResponse response = null;
            if (HttpContext.Current == null)
            {
                HttpApplication context = (HttpApplication)sender;
                response = context.Response;
            }
            else
            {
                response = HttpContext.Current.Response;
            }

            RemoveHeader(response, "Server");
            RemoveHeader(response, "X-AspNetMvc-Version");
            // Remove in web.config
            //RemoveHeader(response, "X-Powered-By");
            //RemoveHeader(response, "X-AspNet-Version");
        }

        /// <summary>
        /// Remove Header
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        private void RemoveHeader(HttpResponse response, string key)
        {
            if (response == null)
            {
                return;
            }

            if (response.Headers == null)
            {
                return;
            }

            if (response.Headers[key] == null)
            {
                return;
            }

            response.Headers.Remove(key);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        { }
    }
}

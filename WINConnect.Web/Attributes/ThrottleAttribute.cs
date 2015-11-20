using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

/*
reference System.Runtime.Caching;
reference System.ServiceModel;
 */
namespace WINConnect.Web.Attributes
{
    public class ThrottleAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// A unique name for this Throttle.
        /// </summary>
        /// <remarks>
        /// We'll be inserting a Cache record based on this name and client IP, e.g. "Name-192.168.0.1"
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// The number of seconds clients must wait before executing this decorated route again.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// A text message that will be sent to the client upon throttling.  You can include the token {n} to
        /// show this.Seconds in the message, e.g. "Wait {n} seconds before trying again".
        /// </summary>
        public string Message { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var key = string.Concat(Name, "-", GetClientIp(actionContext.Request));
            var allowExecute = false;

            if (MemoryCache.Default[key] == null)
            {
                MemoryCache.Default.Set(key, true, DateTime.Now.AddSeconds(Seconds));
                allowExecute = true;
            }

            //if (HttpRuntime.Cache[key] == null)
            //{
            //    HttpRuntime.Cache.Add(key,
            //        true, // is this the smallest data we can have?
            //        null, // no dependencies
            //        DateTime.Now.AddSeconds(Seconds), // absolute expiration
            //        Cache.NoSlidingExpiration,
            //        System.Web.Caching.CacheItemPriority.Low,
            //        null); // no callback

            //    allowExecute = true;
            //}

            if (!allowExecute)
            {
                if (string.IsNullOrEmpty(Message))
                {
                    Message = "You may only perform this action every {n} seconds.";
                }

                actionContext.Response = actionContext.Request.CreateResponse(
                    // HttpStatusCode.BadRequest,
                    (HttpStatusCode)429,
                    Message.Replace("{n}", Seconds.ToString())
                );
            }
        }

        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        private string GetClientIp(HttpRequestMessage request)
        {
            // Web-hosting
            if (request.Properties.ContainsKey(HttpContext))
            {
                HttpContextWrapper ctx =
                    (HttpContextWrapper)request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                RemoteEndpointMessageProperty remoteEndpoint =
                    (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin
            //if (request.Properties.ContainsKey(OwinContext))
            //{
            //    OwinContext owinContext = (OwinContext)request.Properties[OwinContext];
            //    if (owinContext != null)
            //    {
            //        return owinContext.Request.RemoteIpAddress;
            //    }
            //}

            return null;
        }
    }
}
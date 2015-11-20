using System.Web.Mvc;

namespace WINConnect.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
#if !DEBUG
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
#endif
        }
    }
}
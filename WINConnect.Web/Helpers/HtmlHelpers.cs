using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

/// <summary>
/// Html Helpers
/// </summary>
public static class HtmlHelpers
{
    /// <summary>
    /// Create menu link with customs css class
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="linkText"></param>
    /// <param name="actionName"></param>
    /// <param name="controllerName"></param>
    /// <returns></returns>
    public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
    {
        var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action").ToLower();
        var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller").ToLower();

        var builder = new TagBuilder("li")
        {
            InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
        };

        actionName = actionName.ToLower();
        controllerName = controllerName.ToLower();

        if (actionName == currentAction && controllerName == currentController)
        {
            builder.AddCssClass("active");
        }
        return new MvcHtmlString(builder.ToString());
    }

    public static MvcHtmlString Span(this HtmlHelper helper, object htmlAttributes)
    {
        // Create tag builder
        var builder = new TagBuilder("span");

        //
        builder.MergeAttribute("aria-hidden", "true");

        // Add attributes
        builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

        // Render tag
        return new MvcHtmlString(builder.ToString());
    }

}

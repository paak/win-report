namespace WINConnect.Libs.Extensions
{
    public static class BooleanExtensions
    {
        //public static MvcHtmlString ToIcon(this bool isTrue)
        //{
        //    TagBuilder span = new TagBuilder("span");

        //    span.MergeAttribute("class", isTrue ? "fa fa-check-square-o" : "fa fa-square-o");

        //    return MvcHtmlString.Create(span.ToString());
        //}
        public static string ToIcon(this bool value)
        {
            return string.Format("<i class=\"fa{0}\"></i>", value ? " fa-check-square-o" : "");
        }
    }
}
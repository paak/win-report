using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WINConnect.Web
{
    public class JsonConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            // Uses the Json.NET library to perform serialization
            json.UseDataContractJsonSerializer = false;

            // Convert all dates to UTC
            json.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

#if DEBUG
            // Write indented JSON
            json.SerializerSettings.Formatting = Formatting.Indented;
#endif

            json.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;

            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            //json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            //json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            json.SerializerSettings.ContractResolver = new JsonContractResolver(json)
            {
                IgnoreSerializableAttribute = true
            };
        }
    }
}
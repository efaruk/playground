using Newtonsoft.Json;

namespace log4net.Appender.SplunkAppenders
{
    public static class Utility
    {

        //private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        //public static JsonSerializerSettings JsonSerializerSettings
        //{
        //    get { return _jsonSerializerSettings; }
        //    set { _jsonSerializerSettings = value; }
        //}

        public static T Deserialize<T>(string data) where T : class
        {
            var t = JsonConvert.DeserializeObject<T>(data);
            return t;
        }

        public static string Serialize<T>(T data) where T: class 
        {
            var rc = JsonConvert.SerializeObject(data);
            return rc;
        }
    }
}

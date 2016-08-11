using Newtonsoft.Json;

namespace WebAutoLogin.Runtime.Serialization
{
    /// <summary>
    ///     Default JSON serializer
    /// </summary>
    public class JsonNetSerializer : ISerializer
    {
        public object Serialize<TInput>(TInput instance)
        {
            var data = JsonConvert.SerializeObject(instance);
            return data;
        }

        public TOutput Deserialize<TOutput>(object data)
        {
            var t = default(TOutput);
            t = JsonConvert.DeserializeObject<TOutput>((string)data);
            return t;
        }
    }
}

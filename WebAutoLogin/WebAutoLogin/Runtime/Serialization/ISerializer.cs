namespace WebAutoLogin.Runtime.Serialization
{
    /// <summary>
    ///     Generic Serializer Interface
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        ///     Serialize method
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TInput"></typeparam>
        /// <returns></returns>
        object Serialize<TInput>(TInput instance);

        /// <summary>
        ///     Deserialize method
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="TOutput"></typeparam>
        /// <returns></returns>
        TOutput Deserialize<TOutput>(object data);
    }
}
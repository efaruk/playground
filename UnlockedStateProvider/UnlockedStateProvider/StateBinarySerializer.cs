using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnlockedStateProvider
{
    public static class StateBinarySerializer
    {
        //// Old Code #Protobuf-net
        //internal static byte[] Serialize(object value)
        //{

        //	byte[] result = null;
        //	using (var ms = new MemoryStream())
        //	{
        //		Serializer.Serialize(ms, value);
        //		result = ms.ToArray();
        //	}
        //	return result;
        //}


        //internal static T Deserialize<T>(byte[] value)
        //{

        //	var result = default(T);
        //	using (var ms = new MemoryStream(value))
        //	{
        //		result = Serializer.Deserialize<T>(ms);
        //	}
        //	return result;
        //}


        public static byte[] Serialize(object value)
        {
            if (value == null) return null;
            if (!value.GetType().IsSerializable) return null;
            byte[] result;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                result = stream.ToArray();
            }
            return result;
        }

        public static object Deserialize(byte[] value)
        {
            if (value == null) return null;
            object result;
            using (var stream = new MemoryStream(value))
            {
                var formatter = new BinaryFormatter();
                result = formatter.Deserialize(stream);
            }
            return result;
        }

        public static object Deserialize<T>(byte[] value)
        {
            if (value == null) return null;
            T result;
            using (var stream = new MemoryStream(value))
            {
                var formatter = new BinaryFormatter();
                result = (T) formatter.Deserialize(stream);
            }
            return result;
        }
    }
}
using System.Text;

namespace Multicasting
{
    public static class GlobalConstants
    {
        public const string MESSAGE_START = "|%$%|";
        public const string MESSAGE_END = "|%€%|";

        public const string MULTICAST_ADDRESS = "224.0.0.1";
        
        public static Encoding DefaultEncoding = Encoding.UTF8;

    }
}

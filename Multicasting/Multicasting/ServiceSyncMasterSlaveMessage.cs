using System;

namespace Multicasting
{
    public enum SyncMessageType
    {
        Unkown,
        Sync,
        Handshake
        
    }

    public interface IServiceSyncMessage
    {
        SyncMessageType MessageType { get; }

        DateTime SentOn { get; set; }

        DateTime ReceivedAt { get; set; }
    }

    public class ServiceSyncMasterSlaveMessage : IServiceSyncMessage
    {
        public string ServiceName { get; set; }

        public string MachineName { get; set; }

        public bool Master { get; set; }

        public SyncMessageType MessageType { get { return SyncMessageType.Sync; } }

        public DateTime SentOn { get; set; }

        public DateTime ReceivedAt { get; set; }
    }

    public class ServiceSyncMasterSlaveHandshakeMessage: IServiceSyncMessage
    {
        public string ServiceName { get; set; }

        public string MachineName { get; set; }

        public int Luck { get; set; }

        public SyncMessageType MessageType { get { return SyncMessageType.Handshake; } }

        public DateTime SentOn { get; set; }

        public DateTime ReceivedAt { get; set; }
    }
}